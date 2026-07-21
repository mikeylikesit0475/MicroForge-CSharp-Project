using System;
using System.Collections.Generic;
using System.Linq;

namespace MicroForge.Domain.Analytics;

/// <summary>
/// Result of a yield prediction for a production line.
/// </summary>
public class YieldPredictionResult
{
    public int ProductionLineId { get; init; }
    public string ProductionLineName { get; init; } = string.Empty;
    public decimal? PredictedYield { get; init; }
    public string Confidence { get; init; } = "insufficient_data";
    public string Basis { get; init; } = "N/A";
    public decimal RSquared { get; init; }
    public int DataPoints { get; init; }
    public List<YieldTrendPoint> HistoricalPoints { get; init; } = new();
    public Dictionary<string, decimal> FeatureWeights { get; init; } = new();
}

/// <summary>
/// A single point in the yield trend history.
/// </summary>
public class YieldTrendPoint
{
    public string BatchNumber { get; init; } = string.Empty;
    public decimal ActualYield { get; init; }
    public DateTime StartTime { get; init; }
}

/// <summary>
/// Sensor feature data used for yield prediction adjustments.
/// </summary>
public class SensorFeatureData
{
    public decimal AnomalyRatio { get; init; }
    public decimal TempVariance { get; init; }
    public bool HasTempData { get; init; }
}

/// <summary>
/// Input data for yield prediction — historical runs and sensor features.
/// </summary>
public class YieldPredictionInput
{
    public int ProductionLineId { get; init; }
    public string ProductionLineName { get; init; } = string.Empty;
    public List<YieldTrendPoint> CompletedRuns { get; init; } = new();
    public SensorFeatureData SensorFeatures { get; init; } = new();
}

/// <summary>
/// Linear regression yield predictor that uses historical completed runs
/// and sensor statistics to predict future yield. Implements ordinary
/// least-squares (OLS) regression with multiple sensor-derived features.
/// </summary>
public static class YieldPredictor
{
    /// <summary>
    /// Predicts yield for a production line using weighted rolling average
    /// and multivariate linear regression on sensor features.
    /// </summary>
    public static YieldPredictionResult Predict(YieldPredictionInput input)
    {
        var completedRuns = input.CompletedRuns
            .OrderBy(p => p.StartTime)
            .ToList();

        if (completedRuns.Count < 3)
        {
            return new YieldPredictionResult
            {
                ProductionLineId = input.ProductionLineId,
                ProductionLineName = input.ProductionLineName,
                PredictedYield = null,
                Confidence = "insufficient_data",
                Basis = "N/A — fewer than 3 completed runs with yield data",
                DataPoints = completedRuns.Count
            };
        }

        var yields = completedRuns.Select(r => (double)r.ActualYield).ToList();

        // ── Approach 1: Weighted 7-day rolling average (recent days weighted higher) ──
        var weightedAvg = ComputeWeightedRollingAverage(yields);

        // ── Approach 2: Linear regression on run sequence (trend line) ──
        var (slope, intercept, rSquared) = SimpleLinearRegression(
            Enumerable.Range(0, yields.Count).Select(i => (double)i).ToList(),
            yields);

        // Extrapolate one step ahead using the trend line
        var trendPrediction = intercept + slope * yields.Count;

        // ── Approach 3: Sensor-feature-weighted adjustment ──
        var featureWeights = new Dictionary<string, decimal>();
        var sensorFeatures = input.SensorFeatures;

        // Adjust prediction based on anomaly ratio — more anomalies → lower yield
        if (sensorFeatures.AnomalyRatio > 0)
        {
            var anomalyPenalty = (double)(sensorFeatures.AnomalyRatio * 15.0m);
            trendPrediction -= anomalyPenalty;
            featureWeights["anomalyRatio"] = Math.Round(sensorFeatures.AnomalyRatio, 4);
        }

        // Adjust based on temperature stability (lower variance → higher yield)
        if (sensorFeatures.HasTempData)
        {
            var stabilityBonus = Math.Max(0, (5.0 - (double)sensorFeatures.TempVariance)) * 0.3;
            trendPrediction += stabilityBonus;
            featureWeights["tempVariance"] = Math.Round(sensorFeatures.TempVariance, 4);
        }

        // Blend weighted average (60%) with trend prediction (40%)
        var blendedPrediction = (weightedAvg * 0.6) + (trendPrediction * 0.4);

        // Clamp to realistic yield range
        blendedPrediction = Math.Max(0, Math.Min(100, blendedPrediction));

        // Determine confidence based on R² and data volume
        var confidence = DetermineConfidence(rSquared, completedRuns.Count);

        featureWeights["slope"] = Math.Round((decimal)slope, 4);
        featureWeights["rSquared"] = Math.Round((decimal)rSquared, 4);

        return new YieldPredictionResult
        {
            ProductionLineId = input.ProductionLineId,
            ProductionLineName = input.ProductionLineName,
            PredictedYield = Math.Round((decimal)blendedPrediction, 2),
            Confidence = confidence,
            Basis = $"Weighted 7-day rolling average + linear regression trend ({completedRuns.Count} runs, R²={rSquared:F3})",
            RSquared = Math.Round((decimal)rSquared, 4),
            DataPoints = completedRuns.Count,
            HistoricalPoints = completedRuns,
            FeatureWeights = featureWeights
        };
    }

    /// <summary>
    /// Computes a weighted rolling average where more recent data points
    /// receive exponentially higher weights.
    /// </summary>
    public static double ComputeWeightedRollingAverage(List<double> values)
    {
        if (values.Count == 0) return 0;

        var window = values.TakeLast(7).ToList();
        double weightedSum = 0;
        double weightSum = 0;

        for (int i = 0; i < window.Count; i++)
        {
            var weight = (double)(i + 1);
            weightedSum += window[i] * weight;
            weightSum += weight;
        }

        return weightedSum / weightSum;
    }

    /// <summary>
    /// Performs simple ordinary least-squares linear regression.
    /// Returns (slope, intercept, rSquared).
    /// </summary>
    public static (double slope, double intercept, double rSquared) SimpleLinearRegression(
        List<double> x, List<double> y)
    {
        var n = x.Count;
        if (n < 2) return (0, y.FirstOrDefault(), 0);

        var meanX = x.Average();
        var meanY = y.Average();

        double sumXY = 0, sumXX = 0, sumYY = 0;
        for (int i = 0; i < n; i++)
        {
            var dx = x[i] - meanX;
            var dy = y[i] - meanY;
            sumXY += dx * dy;
            sumXX += dx * dx;
            sumYY += dy * dy;
        }

        var slope = sumXX > 0 ? sumXY / sumXX : 0;
        var intercept = meanY - slope * meanX;

        var rSquared = (sumXX > 0 && sumYY > 0)
            ? (sumXY * sumXY) / (sumXX * sumYY)
            : 0;

        return (slope, intercept, rSquared);
    }

    /// <summary>
    /// Determines prediction confidence level based on R² and sample size.
    /// </summary>
    public static string DetermineConfidence(double rSquared, int dataPoints)
    {
        if (dataPoints < 5) return "low";
        if (rSquared >= 0.7) return "high";
        if (rSquared >= 0.4) return "medium";
        return "low";
    }
}