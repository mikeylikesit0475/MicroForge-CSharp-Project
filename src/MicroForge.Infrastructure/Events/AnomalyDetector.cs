using MicroForge.Domain.Interfaces;

namespace MicroForge.Infrastructure.Events;

public class AnomalyDetector : IAnomalyDetector
{
    private static readonly Dictionary<string, (decimal Min, decimal Max, string Unit)> Thresholds = new()
    {
        ["Temperature"] = (15m, 85m, "°C"),
        ["Pressure"] = (0.5m, 5.0m, "bar"),
        ["Vibration"] = (0m, 10m, "mm/s"),
        ["Humidity"] = (20m, 80m, "%")
    };

    public AnomalyResult Detect(string sensorType, decimal value, string unit, List<decimal> recentValues)
    {
        if (Thresholds.TryGetValue(sensorType, out var threshold))
        {
            if (value > threshold.Max)
                return new AnomalyResult
                {
                    IsAnomaly = true,
                    AnomalyType = "HighThreshold",
                    Description = $"{sensorType} reading {value}{unit} exceeds maximum threshold {threshold.Max}{threshold.Unit}",
                    Threshold = threshold.Max
                };
            if (value < threshold.Min)
                return new AnomalyResult
                {
                    IsAnomaly = true,
                    AnomalyType = "LowThreshold",
                    Description = $"{sensorType} reading {value}{unit} below minimum threshold {threshold.Min}{threshold.Unit}",
                    Threshold = threshold.Min
                };
        }

        if (recentValues.Count >= 5)
        {
            var mean = recentValues.Average();
            var std = (decimal)Math.Sqrt((double)recentValues.Select(v => Math.Pow((double)(v - mean), 2)).Average());
            decimal zscore;
            if (std == 0)
            {
                // When there is no variance, any deviation from the mean is an infinite outlier
                zscore = value == mean ? 0m : value > mean ? decimal.MaxValue : decimal.MinValue;
            }
            else
            {
                zscore = (value - mean) / std;
            }
            if (Math.Abs(zscore) > 3m)
                return new AnomalyResult
                {
                    IsAnomaly = true,
                    AnomalyType = "StatisticalOutlier",
                    Description = $"{sensorType} reading {value}{unit} is a statistical outlier (z-score: {zscore:F2})",
                    ZScore = zscore
                };
        }

        return new AnomalyResult { IsAnomaly = false };
    }
}