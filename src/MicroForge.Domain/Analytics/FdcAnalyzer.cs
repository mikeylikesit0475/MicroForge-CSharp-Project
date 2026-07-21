using System;
using System.Collections.Generic;
using System.Linq;

namespace MicroForge.Domain.Analytics;

/// <summary>
/// Predefined fault profile catalog.
/// </summary>
public class FaultSignature
{
    public string Name { get; set; } = string.Empty;
    public string SensorType { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Severity { get; set; } = "Medium";
}

/// <summary>
/// Classification result from signal pattern analysis.
/// </summary>
public class FdcAnalysisResult
{
    public bool FaultDetected { get; set; }
    public string? FaultName { get; set; }
    public string? Description { get; set; }
    public string Severity { get; set; } = "Low";
    public decimal Confidence { get; set; }
}

/// <summary>
/// Monitors process sensors for dynamic anomalies matching signature profiles.
/// </summary>
public static class FdcAnalyzer
{
    private static readonly List<FaultSignature> Signatures = new()
    {
        new() { Name = "Thermal Runaway", SensorType = "Temperature", Description = "Rapid temperature increase exceeding process safety bounds.", Severity = "Critical" },
        new() { Name = "Chamber Vacuum Leak", SensorType = "Pressure", Description = "Abrupt pressure increase during high vacuum processing.", Severity = "Critical" },
        new() { Name = "RF Plasma Instability", SensorType = "Vibration", Description = "Excessive high-frequency physical oscillations in processing chamber.", Severity = "High" }
    };

    public static List<FaultSignature> GetSignatures() => Signatures;

    public static FdcAnalysisResult Analyze(string sensorType, List<decimal> values)
    {
        if (values == null || values.Count < 5)
        {
            return new FdcAnalysisResult { FaultDetected = false };
        }

        // 1. Analyze Temperature for "Thermal Runaway" (rising slope + high values)
        if (sensorType.Equals("Temperature", StringComparison.OrdinalIgnoreCase))
        {
            var last4 = values.TakeLast(4).ToList();
            var isRising = true;
            for (int i = 1; i < last4.Count; i++)
            {
                if (last4[i] <= last4[i - 1]) isRising = false;
            }

            var startVal = last4.First();
            var endVal = last4.Last();
            var risePct = startVal > 0 ? (endVal - startVal) / startVal : 0;

            if (isRising && risePct > 0.40m)
            {
                var sig = Signatures.First(s => s.Name == "Thermal Runaway");
                return new FdcAnalysisResult
                {
                    FaultDetected = true,
                    FaultName = sig.Name,
                    Description = $"{sig.Description} Temp jumped from {startVal:F1} to {endVal:F1} (Risen {risePct * 100:F0}%).",
                    Severity = sig.Severity,
                    Confidence = 0.95m
                };
            }
        }

        // 2. Analyze Pressure for "Chamber Vacuum Leak" (sudden spike)
        if (sensorType.Equals("Pressure", StringComparison.OrdinalIgnoreCase))
        {
            var average = values.SkipLast(1).Average();
            var last = values.Last();
            
            if (average > 0 && (last - average) / average > 1.0m)
            {
                var sig = Signatures.First(s => s.Name == "Chamber Vacuum Leak");
                return new FdcAnalysisResult
                {
                    FaultDetected = true,
                    FaultName = sig.Name,
                    Description = $"{sig.Description} Pressure spiked to {last:F2} (Baseline avg: {average:F2}).",
                    Severity = sig.Severity,
                    Confidence = 0.90m
                };
            }
        }

        // 3. Analyze Vibration for "RF Plasma Instability" (high variance/volatility)
        if (sensorType.Equals("Vibration", StringComparison.OrdinalIgnoreCase))
        {
            var average = values.Average();
            var stdDev = (decimal)Math.Sqrt((double)values.Select(v => Math.Pow((double)(v - average), 2)).Average());

            if (average > 0 && (stdDev / average) > 0.60m)
            {
                var sig = Signatures.First(s => s.Name == "RF Plasma Instability");
                return new FdcAnalysisResult
                {
                    FaultDetected = true,
                    FaultName = sig.Name,
                    Description = $"{sig.Description} Vibration std-dev of {stdDev:F2} is highly volatile (avg: {average:F2}).",
                    Severity = sig.Severity,
                    Confidence = 0.85m
                };
            }
        }

        return new FdcAnalysisResult { FaultDetected = false };
    }
}
