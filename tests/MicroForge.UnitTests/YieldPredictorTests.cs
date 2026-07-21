using FluentAssertions;
using MicroForge.Domain.Analytics;
using MicroForge.Domain.Entities;
using MicroForge.Domain.Interfaces;
using MicroForge.Infrastructure.AI.Tools;
using MicroForge.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Nodes;

namespace MicroForge.UnitTests;

/// <summary>
/// Unit tests for the YieldPredictor linear regression engine.
/// </summary>
public class YieldPredictorTests
{
    [Fact]
    public void Predict_WithFewerThanThreeRuns_ReturnsInsufficientData()
    {
        var input = new YieldPredictionInput
        {
            ProductionLineId = 1,
            ProductionLineName = "Line Alpha",
            CompletedRuns = new List<YieldTrendPoint>
            {
                new() { BatchNumber = "B001", ActualYield = 90m, StartTime = DateTime.UtcNow.AddDays(-2) },
                new() { BatchNumber = "B002", ActualYield = 91m, StartTime = DateTime.UtcNow.AddDays(-1) }
            },
            SensorFeatures = new SensorFeatureData()
        };

        var result = YieldPredictor.Predict(input);

        result.PredictedYield.Should().BeNull();
        result.Confidence.Should().Be("insufficient_data");
        result.DataPoints.Should().Be(2);
    }

    [Fact]
    public void Predict_WithThreeRuns_ReturnsPrediction()
    {
        var input = new YieldPredictionInput
        {
            ProductionLineId = 1,
            ProductionLineName = "Line Alpha",
            CompletedRuns = new List<YieldTrendPoint>
            {
                new() { BatchNumber = "B001", ActualYield = 90m, StartTime = DateTime.UtcNow.AddDays(-3) },
                new() { BatchNumber = "B002", ActualYield = 92m, StartTime = DateTime.UtcNow.AddDays(-2) },
                new() { BatchNumber = "B003", ActualYield = 94m, StartTime = DateTime.UtcNow.AddDays(-1) }
            },
            SensorFeatures = new SensorFeatureData { AnomalyRatio = 0.05m }
        };

        var result = YieldPredictor.Predict(input);

        result.PredictedYield.Should().NotBeNull();
        result.PredictedYield!.Value.Should().BeGreaterThan(0m);
        result.PredictedYield.Value.Should().BeLessThanOrEqualTo(100m);
        result.DataPoints.Should().Be(3);
        result.FeatureWeights.Should().ContainKey("slope");
        result.FeatureWeights.Should().ContainKey("rSquared");
    }

    [Fact]
    public void Predict_WithUpwardTrend_ReturnsHigherPrediction()
    {
        var input = new YieldPredictionInput
        {
            ProductionLineId = 1,
            ProductionLineName = "Line Alpha",
            CompletedRuns = Enumerable.Range(0, 10).Select(i => new YieldTrendPoint
            {
                BatchNumber = $"B{i:D3}",
                ActualYield = 80m + i * 1.5m,
                StartTime = DateTime.UtcNow.AddDays(-(10 - i))
            }).ToList(),
            SensorFeatures = new SensorFeatureData { AnomalyRatio = 0.02m }
        };

        var result = YieldPredictor.Predict(input);

        result.PredictedYield.Should().NotBeNull();
        result.PredictedYield!.Value.Should().BeGreaterThan(85m);
        result.FeatureWeights["slope"].Should().BeGreaterThan(0m);
    }

    [Fact]
    public void Predict_WithHighAnomalyRatio_ReducesPrediction()
    {
        var baseRuns = Enumerable.Range(0, 10).Select(i => new YieldTrendPoint
        {
            BatchNumber = $"B{i:D3}",
            ActualYield = 90m,
            StartTime = DateTime.UtcNow.AddDays(-(10 - i))
        }).ToList();

        var lowAnomalyInput = new YieldPredictionInput
        {
            ProductionLineId = 1,
            ProductionLineName = "Line Alpha",
            CompletedRuns = baseRuns,
            SensorFeatures = new SensorFeatureData { AnomalyRatio = 0.0m }
        };

        var highAnomalyInput = new YieldPredictionInput
        {
            ProductionLineId = 1,
            ProductionLineName = "Line Alpha",
            CompletedRuns = baseRuns,
            SensorFeatures = new SensorFeatureData { AnomalyRatio = 0.5m }
        };

        var lowResult = YieldPredictor.Predict(lowAnomalyInput);
        var highResult = YieldPredictor.Predict(highAnomalyInput);

        highResult.PredictedYield.Should().NotBeNull();
        lowResult.PredictedYield.Should().NotBeNull();
        highResult.PredictedYield!.Value.Should().BeLessThan(lowResult.PredictedYield!.Value);
    }

    [Fact]
    public void Predict_WithTempStability_BonusApplied()
    {
        var runs = Enumerable.Range(0, 10).Select(i => new YieldTrendPoint
        {
            BatchNumber = $"B{i:D3}",
            ActualYield = 90m,
            StartTime = DateTime.UtcNow.AddDays(-(10 - i))
        }).ToList();

        var stableInput = new YieldPredictionInput
        {
            ProductionLineId = 1,
            ProductionLineName = "Line Alpha",
            CompletedRuns = runs,
            SensorFeatures = new SensorFeatureData { AnomalyRatio = 0.0m, TempVariance = 1.0m, HasTempData = true }
        };

        var unstableInput = new YieldPredictionInput
        {
            ProductionLineId = 1,
            ProductionLineName = "Line Alpha",
            CompletedRuns = runs,
            SensorFeatures = new SensorFeatureData { AnomalyRatio = 0.0m, TempVariance = 10.0m, HasTempData = true }
        };

        var stableResult = YieldPredictor.Predict(stableInput);
        var unstableResult = YieldPredictor.Predict(unstableInput);

        stableResult.PredictedYield.Should().NotBeNull();
        unstableResult.PredictedYield.Should().NotBeNull();
        stableResult.PredictedYield!.Value.Should().BeGreaterThanOrEqualTo(unstableResult.PredictedYield!.Value);
    }

    [Fact]
    public void ComputeWeightedRollingAverage_WeightsRecentHigher()
    {
        var values = new List<double> { 80, 82, 84, 86, 88, 90, 92 };

        var avg = YieldPredictor.ComputeWeightedRollingAverage(values);

        // Weighted average should be closer to the most recent values
        avg.Should().BeGreaterThan(values.Average());
        avg.Should().BeGreaterThan(86.0);
    }

    [Fact]
    public void SimpleLinearRegression_PerfectLinearData_ReturnsHighRSquared()
    {
        var x = new List<double> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        var y = new List<double> { 10, 12, 14, 16, 18, 20, 22, 24, 26, 28 };

        var (slope, intercept, rSquared) = YieldPredictor.SimpleLinearRegression(x, y);

        slope.Should().BeApproximately(2.0, 0.001);
        intercept.Should().BeApproximately(10.0, 0.001);
        rSquared.Should().BeApproximately(1.0, 0.001);
    }

    [Fact]
    public void DetermineConfidence_LowDataPoints_ReturnsLow()
    {
        YieldPredictor.DetermineConfidence(0.9, 3).Should().Be("low");
    }

    [Fact]
    public void DetermineConfidence_HighRSquared_ReturnsHigh()
    {
        YieldPredictor.DetermineConfidence(0.8, 10).Should().Be("high");
    }

    [Fact]
    public void DetermineConfidence_MediumRSquared_ReturnsMedium()
    {
        YieldPredictor.DetermineConfidence(0.5, 10).Should().Be("medium");
    }
}

/// <summary>
/// Unit tests for the PredictYieldTool and GetYieldTrendWithPredictionTool.
/// </summary>
public class PredictYieldToolTests : TestBase
{
    [Fact]
    public async Task PredictYieldTool_WithValidLine_ReturnsPrediction()
    {
        await seedstandarddata();

        // Add completed runs with yield data for Line Alpha (Id=1)
        for (int i = 0; i < 5; i++)
        {
            DbContext.ProductionRuns.Add(new ProductionRun
            {
                ProductionLineId = 1,
                BatchNumber = $"YIELD-B{i:D3}",
                StartTime = DateTime.UtcNow.AddDays(-(5 - i)),
                EndTime = DateTime.UtcNow.AddDays(-(5 - i)).AddHours(2),
                Status = "Completed",
                TargetYield = 95m,
                ActualYield = 88m + i * 2m
            });
        }
        await DbContext.SaveChangesAsync();

        var tool = new PredictYieldTool(DbContext);
        var context = new ToolExecutionContext
        {
            UserId = 1,
            Username = "admin",
            Roles = new List<string> { "Admin" },
            ProductionLineIds = new List<int>()
        };

        var result = await tool.InvokeAsync(new JsonObject { ["productionLineId"] = 1 }, context);

        result.Success.Should().BeTrue();
        result.Output.Should().Contain("predictedYield");
        result.Output.Should().Contain("confidence");
    }

    [Fact]
    public async Task PredictYieldTool_WithInsufficientData_ReturnsNullPrediction()
    {
        await seedstandarddata();

        var tool = new PredictYieldTool(DbContext);
        var context = new ToolExecutionContext
        {
            UserId = 1,
            Username = "admin",
            Roles = new List<string> { "Admin" },
            ProductionLineIds = new List<int>()
        };

        var result = await tool.InvokeAsync(new JsonObject { ["productionLineId"] = 1 }, context);

        result.Success.Should().BeTrue();
        result.Output.Should().Contain("insufficient_data");
    }

    [Fact]
    public async Task PredictYieldTool_WithNonexistentLine_ReturnsError()
    {
        var tool = new PredictYieldTool(DbContext);
        var context = new ToolExecutionContext
        {
            UserId = 1,
            Username = "admin",
            Roles = new List<string> { "Admin" },
            ProductionLineIds = new List<int>()
        };

        var result = await tool.InvokeAsync(new JsonObject { ["productionLineId"] = 999 }, context);

        result.Success.Should().BeFalse();
        result.Error.Should().Contain("not found");
    }

    [Fact]
    public async Task PredictYieldTool_WithoutLineId_ReturnsError()
    {
        var tool = new PredictYieldTool(DbContext);
        var context = new ToolExecutionContext
        {
            UserId = 1,
            Username = "admin",
            Roles = new List<string> { "Admin" },
            ProductionLineIds = new List<int>()
        };

        var result = await tool.InvokeAsync(new JsonObject(), context);

        result.Success.Should().BeFalse();
        result.Error.Should().Contain("required");
    }

    [Fact]
    public async Task PredictYieldTool_OperatorWithoutAccess_ReturnsDenied()
    {
        await seedstandarddata();

        var tool = new PredictYieldTool(DbContext);
        var context = new ToolExecutionContext
        {
            UserId = 2,
            Username = "operator01",
            Roles = new List<string> { "Operator" },
            ProductionLineIds = new List<int> { 1 } // only line 1
        };

        var result = await tool.InvokeAsync(new JsonObject { ["productionLineId"] = 2 }, context);

        result.Success.Should().BeFalse();
        result.Error.Should().Contain("Access denied");
    }
}

/// <summary>
/// Unit tests for GetYieldTrendWithPredictionTool.
/// </summary>
public class GetYieldTrendWithPredictionToolTests : TestBase
{
    [Fact]
    public async Task GetYieldTrend_WithCompletedRuns_ReturnsTrends()
    {
        await seedstandarddata();

        // Line Beta (Id=2) already has one completed run from seed data
        var tool = new GetYieldTrendWithPredictionTool(DbContext);
        var context = new ToolExecutionContext
        {
            UserId = 1,
            Username = "admin",
            Roles = new List<string> { "Admin" },
            ProductionLineIds = new List<int>()
        };

        var result = await tool.InvokeAsync(new JsonObject { ["productionLineId"] = 2 }, context);

        result.Success.Should().BeTrue();
        result.Output.Should().Contain("trends");
    }

    [Fact]
    public async Task GetYieldTrend_WithoutLineId_ReturnsAllActiveLines()
    {
        await seedstandarddata();

        var tool = new GetYieldTrendWithPredictionTool(DbContext);
        var context = new ToolExecutionContext
        {
            UserId = 1,
            Username = "admin",
            Roles = new List<string> { "Admin" },
            ProductionLineIds = new List<int>()
        };

        var result = await tool.InvokeAsync(new JsonObject(), context);

        result.Success.Should().BeTrue();
        result.Output.Should().Contain("trends");
    }
}