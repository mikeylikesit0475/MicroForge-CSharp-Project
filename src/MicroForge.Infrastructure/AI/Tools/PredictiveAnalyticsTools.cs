using MicroForge.Domain.Analytics;
using MicroForge.Domain.Entities;
using MicroForge.Domain.Interfaces;
using MicroForge.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Nodes;

namespace MicroForge.Infrastructure.AI.Tools;

/// <summary>
/// Predicts yield for a production line using weighted rolling average
/// and multivariate linear regression on sensor features.
/// </summary>
public class PredictYieldTool : BaseTool
{
    private readonly MicroForgeDbContext _db;
    public PredictYieldTool(MicroForgeDbContext db) => _db = db;

    public override string Name => "predict_yield";
    public override string Description => "Predict yield for a production line based on historical completed runs and sensor trends using linear regression. Returns predicted yield percentage, confidence level, R-squared, and contributing feature weights.";
    public override string Category => "analytics";
    public override JsonObject Schema => new()
    {
        ["type"] = "object",
        ["properties"] = new JsonObject
        {
            ["productionLineId"] = new JsonObject
            {
                ["type"] = "integer",
                ["description"] = "Production line ID to predict yield for"
            }
        },
        ["required"] = new JsonArray { "productionLineId" }
    };

    public override async Task<ToolResult> InvokeAsync(
        JsonObject? arguments,
        ToolExecutionContext context,
        CancellationToken ct = default)
    {
        var lineId = GetInt(arguments, "productionLineId");
        if (lineId <= 0)
            return ToolResult.Fail("productionLineId is required");

        var allowed = GetAllowedLineIds(context);
        if (allowed.Length > 0 && !allowed.Contains(lineId))
            return ToolResult.Fail("Access denied to production line");

        var line = await _db.ProductionLines.FirstOrDefaultAsync(l => l.Id == lineId, ct);
        if (line == null)
            return ToolResult.Fail($"Production line {lineId} not found");

        var input = await PredictionInputBuilder.BuildPredictionInput(_db, line, ct);
        var prediction = YieldPredictor.Predict(input);

        return ToolResult.Ok(Serialize(new
        {
            prediction.ProductionLineId,
            prediction.ProductionLineName,
            prediction.PredictedYield,
            prediction.Confidence,
            prediction.Basis,
            prediction.RSquared,
            prediction.DataPoints,
            featureWeights = prediction.FeatureWeights,
            historicalPoints = prediction.HistoricalPoints.TakeLast(10)
        }));
    }
}

/// <summary>
/// Gets yield trend data with optional prediction overlay for charting.
/// Combines historical completed-run yields with a forward prediction.
/// </summary>
public class GetYieldTrendWithPredictionTool : BaseTool
{
    private readonly MicroForgeDbContext _db;
    public GetYieldTrendWithPredictionTool(MicroForgeDbContext db) => _db = db;

    public override string Name => "get_yield_trend_with_prediction";
    public override string Description => "Get yield trend data for charting, including a forward-looking prediction based on linear regression. Returns historical yields and a predicted next-run yield with confidence metrics.";
    public override string Category => "analytics";
    public override JsonObject Schema => new()
    {
        ["type"] = "object",
        ["properties"] = new JsonObject
        {
            ["productionLineId"] = new JsonObject
            {
                ["type"] = "integer",
                ["description"] = "Production line ID (optional — if omitted, returns trends for all active lines)"
            },
            ["limit"] = new JsonObject
            {
                ["type"] = "integer",
                ["description"] = "Number of historical runs to include (default 10, max 50)"
            }
        }
    };

    public override async Task<ToolResult> InvokeAsync(
        JsonObject? arguments,
        ToolExecutionContext context,
        CancellationToken ct = default)
    {
        var lineId = GetInt(arguments, "productionLineId");
        var limit = GetInt(arguments, "limit", 10);
        if (limit > 50) limit = 50;

        var allowed = GetAllowedLineIds(context);

        var lineQuery = _db.ProductionLines.Where(l => l.IsActive);
        if (lineId > 0)
        {
            if (allowed.Length > 0 && !allowed.Contains(lineId))
                return ToolResult.Fail("Access denied to production line");
            lineQuery = lineQuery.Where(l => l.Id == lineId);
        }
        else if (allowed.Length > 0)
        {
            lineQuery = lineQuery.Where(l => allowed.Contains(l.Id));
        }

        var lines = await lineQuery.ToListAsync(ct);
        var results = new List<object>();

        foreach (var line in lines)
        {
            var input = await PredictionInputBuilder.BuildPredictionInput(_db, line, ct);
            var prediction = YieldPredictor.Predict(input);

            var runs = await _db.ProductionRuns
                .Where(r => r.ProductionLineId == line.Id && r.Status == "Completed" && r.ActualYield > 0)
                .OrderByDescending(r => r.StartTime)
                .Take(limit)
                .Select(r => new { r.BatchNumber, r.TargetYield, r.ActualYield, r.StartTime })
                .ToListAsync(ct);

            results.Add(new
            {
                lineId = line.Id,
                lineName = line.Name,
                lineType = line.LineType,
                historicalRuns = runs,
                prediction = new
                {
                    prediction.PredictedYield,
                    prediction.Confidence,
                    prediction.Basis,
                    prediction.RSquared,
                    prediction.DataPoints
                }
            });
        }

        return ToolResult.Ok(Serialize(new { count = results.Count, trends = results }));
    }
}

/// <summary>
/// Builds a YieldPredictionInput from database data for a given production line.
/// </summary>
internal static class PredictionInputBuilder
{
    public static async Task<YieldPredictionInput> BuildPredictionInput(
        Microsoft.EntityFrameworkCore.DbContext db,
        MicroForge.Domain.Entities.ProductionLine line,
        CancellationToken ct)
    {
        var completedRuns = await db.Set<MicroForge.Domain.Entities.ProductionRun>()
            .Where(r => r.ProductionLineId == line.Id
                && r.Status == "Completed"
                && r.ActualYield > 0)
            .OrderByDescending(r => r.StartTime)
            .Take(30)
            .Select(r => new YieldTrendPoint
            {
                BatchNumber = r.BatchNumber,
                ActualYield = r.ActualYield,
                StartTime = r.StartTime
            })
            .ToListAsync(ct);

        var recentReadings = await db.Set<MicroForge.Domain.Entities.SensorReading>()
            .Where(r => r.ProductionLineId == line.Id)
            .OrderByDescending(r => r.Timestamp)
            .Take(200)
            .ToListAsync(ct);

        var sensorFeatures = ComputeSensorFeatures(recentReadings);

        return new YieldPredictionInput
        {
            ProductionLineId = line.Id,
            ProductionLineName = line.Name,
            CompletedRuns = completedRuns,
            SensorFeatures = sensorFeatures
        };
    }

    private static SensorFeatureData ComputeSensorFeatures(List<MicroForge.Domain.Entities.SensorReading> readings)
    {
        if (readings.Count == 0)
            return new SensorFeatureData();

        var anomalyCount = readings.Count(r => r.IsAnomaly);
        var anomalyRatio = (decimal)anomalyCount / readings.Count;

        var tempReadings = readings.Where(r => r.SensorType == "Temperature").ToList();
        if (tempReadings.Count > 1)
        {
            var tempValues = tempReadings.Select(r => (double)r.Value).ToList();
            var tempMean = tempValues.Average();
            var tempVariance = (decimal)tempValues.Select(v => Math.Pow(v - tempMean, 2)).Average();
            return new SensorFeatureData
            {
                AnomalyRatio = anomalyRatio,
                TempVariance = tempVariance,
                HasTempData = true
            };
        }

        return new SensorFeatureData { AnomalyRatio = anomalyRatio };
    }
}