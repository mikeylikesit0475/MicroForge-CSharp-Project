using MicroForge.Application.Dtos;
using MicroForge.Domain.Analytics;
using MicroForge.Domain.Entities;
using MicroForge.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MicroForge.Application.Services;

/// <summary>
/// Application service for predictive analytics: yield prediction via
/// linear regression, yield trend charting with prediction overlays,
/// and dashboard chart data aggregation.
/// </summary>
public class PredictiveAnalyticsService : IPredictiveAnalyticsService
{
    private readonly IRepository<ProductionLine> _lineRepo;
    private readonly IRepository<ProductionRun> _runRepo;
    private readonly IRepository<SensorReading> _readingRepo;
    private readonly IRepository<DefectRecord> _defectRepo;

    public PredictiveAnalyticsService(
        IRepository<ProductionLine> lineRepo,
        IRepository<ProductionRun> runRepo,
        IRepository<SensorReading> readingRepo,
        IRepository<DefectRecord> defectRepo)
    {
        _lineRepo = lineRepo;
        _runRepo = runRepo;
        _readingRepo = readingRepo;
        _defectRepo = defectRepo;
    }

    public async Task<YieldPredictionDto> PredictYieldAsync(
        int productionLineId,
        CancellationToken ct = default)
    {
        var line = await _lineRepo.GetByIdAsync(productionLineId, ct);
        if (line == null)
            throw new InvalidOperationException($"Production line {productionLineId} not found");

        var input = await BuildPredictionInput(line, ct);
        var result = YieldPredictor.Predict(input);
        return MapPrediction(result);
    }

    public async Task<List<YieldTrendWithPredictionDto>> GetYieldTrendsAsync(
        int? productionLineId,
        int limit,
        CancellationToken ct = default)
    {
        if (limit > 50) limit = 50;

        var lineQuery = _lineRepo.Query().Where(l => l.IsActive);
        if (productionLineId.HasValue)
            lineQuery = lineQuery.Where(l => l.Id == productionLineId.Value);

        var lines = await lineQuery.OrderBy(l => l.Name).ToListAsync(ct);
        var results = new List<YieldTrendWithPredictionDto>();

        foreach (var line in lines)
        {
            var input = await BuildPredictionInput(line, ct);
            var prediction = YieldPredictor.Predict(input);

            var runs = await _runRepo.Query()
                .Where(r => r.ProductionLineId == line.Id
                    && r.Status == "Completed"
                    && r.ActualYield > 0)
                .OrderByDescending(r => r.StartTime)
                .Take(limit)
                .Select(r => new YieldTrendRunDto
                {
                    BatchNumber = r.BatchNumber,
                    TargetYield = r.TargetYield,
                    ActualYield = r.ActualYield,
                    StartTime = r.StartTime
                })
                .ToListAsync(ct);

            runs.Reverse();

            results.Add(new YieldTrendWithPredictionDto
            {
                LineId = line.Id,
                LineName = line.Name,
                LineType = line.LineType,
                HistoricalRuns = runs,
                Prediction = new YieldPredictionSummaryDto
                {
                    PredictedYield = prediction.PredictedYield,
                    Confidence = prediction.Confidence,
                    Basis = prediction.Basis,
                    RSquared = prediction.RSquared,
                    DataPoints = prediction.DataPoints
                }
            });
        }

        return results;
    }

    public async Task<DashboardChartDataDto> GetDashboardChartDataAsync(
        CancellationToken ct = default)
    {
        var activeLines = await _lineRepo.Query()
            .Where(l => l.IsActive)
            .OrderBy(l => l.Name)
            .ToListAsync(ct);

        var sensorGauges = new List<SensorGaugeDto>();
        foreach (var line in activeLines)
        {
            var sensorTypes = await _readingRepo.Query()
                .Where(r => r.ProductionLineId == line.Id)
                .Select(r => r.SensorType)
                .Distinct()
                .ToListAsync(ct);

            foreach (var sensorType in sensorTypes)
            {
                var latest = await _readingRepo.Query()
                    .Where(r => r.ProductionLineId == line.Id && r.SensorType == sensorType)
                    .OrderByDescending(r => r.Timestamp)
                    .FirstOrDefaultAsync(ct);

                if (latest != null)
                {
                    var status = "Normal";
                    if (latest.IsAnomaly)
                        status = latest.AnomalyType == "SPC_Violation" ? "SPC Violation" : "Anomaly";
                    else if (latest.Ucl.HasValue && latest.Value > latest.Ucl.Value)
                        status = "Above UCL";
                    else if (latest.Lcl.HasValue && latest.Value < latest.Lcl.Value)
                        status = "Below LCL";

                    sensorGauges.Add(new SensorGaugeDto
                    {
                        ProductionLineId = line.Id,
                        ProductionLineName = line.Name,
                        SensorType = latest.SensorType,
                        CurrentValue = latest.Value,
                        Unit = latest.Unit,
                        Mean = latest.Mean,
                        Ucl = latest.Ucl,
                        Lcl = latest.Lcl,
                        IsAnomaly = latest.IsAnomaly,
                        Status = status
                    });
                }
            }
        }

        var yieldTrends = await GetYieldTrendsAsync(null, 10, ct);

        var defectDistribution = await _defectRepo.Query()
            .GroupBy(d => d.DefectType)
            .Select(g => new DefectDistributionDto
            {
                DefectType = g.Key,
                Count = g.Count(),
                Severity = g.OrderByDescending(d => d.Severity).First().Severity
            })
            .OrderByDescending(d => d.Count)
            .Take(10)
            .ToListAsync(ct);

        return new DashboardChartDataDto
        {
            SensorGauges = sensorGauges,
            YieldTrends = yieldTrends,
            DefectDistribution = defectDistribution
        };
    }

    private async Task<YieldPredictionInput> BuildPredictionInput(
        ProductionLine line,
        CancellationToken ct)
    {
        var completedRuns = await _runRepo.Query()
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

        var recentReadings = await _readingRepo.Query()
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

    private static SensorFeatureData ComputeSensorFeatures(List<SensorReading> readings)
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

    private static YieldPredictionDto MapPrediction(YieldPredictionResult r) => new()
    {
        ProductionLineId = r.ProductionLineId,
        ProductionLineName = r.ProductionLineName,
        PredictedYield = r.PredictedYield,
        Confidence = r.Confidence,
        Basis = r.Basis,
        RSquared = r.RSquared,
        DataPoints = r.DataPoints,
        HistoricalPoints = r.HistoricalPoints.Select(p => new YieldTrendPointDto
        {
            BatchNumber = p.BatchNumber,
            ActualYield = p.ActualYield,
            StartTime = p.StartTime
        }).ToList(),
        FeatureWeights = r.FeatureWeights
    };
}