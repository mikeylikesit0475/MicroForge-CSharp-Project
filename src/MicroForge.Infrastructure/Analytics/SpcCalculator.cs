using System;
using System.Collections.Generic;
using System.Linq;

namespace MicroForge.Infrastructure.Analytics;

public class SpcEvaluationResult
{
    public bool IsViolation { get; set; }
    public List<string> Violations { get; set; } = new();
    public decimal Mean { get; set; }
    public decimal StdDev { get; set; }
    public decimal Ucl { get; set; }
    public decimal Lcl { get; set; }
}

public static class SpcCalculator
{
    public static SpcEvaluationResult Evaluate(decimal currentValue, List<decimal> history)
    {
        // history must be chronological (oldest to newest)
        var allValues = history.ToList();
        allValues.Add(currentValue);

        var result = new SpcEvaluationResult();

        if (history.Count < 3)
        {
            // Not enough historical baseline data points to compute meaningful stats
            return result;
        }

        // 1. Calculate Mean & Standard Deviation of historical baseline
        var mean = history.Average();
        var stdDev = (decimal)Math.Sqrt((double)history.Select(v => Math.Pow((double)(v - mean), 2)).Average());

        result.Mean = Math.Round(mean, 4);
        result.StdDev = Math.Round(stdDev, 4);
        result.Ucl = Math.Round(mean + 3 * stdDev, 4);
        result.Lcl = Math.Round(mean - 3 * stdDev, 4);

        // 2. Rule 1: 1 point outside 3-sigma limits
        if (currentValue > result.Ucl || currentValue < result.Lcl)
        {
            result.IsViolation = true;
            result.Violations.Add("Rule 1: Point outside 3-sigma control limits");
        }

        // 3. Rule 2: 9 consecutive points on the same side of the mean
        if (allValues.Count >= 9)
        {
            var last9 = allValues.TakeLast(9).ToList();
            bool allAbove = last9.All(v => v > mean);
            bool allBelow = last9.All(v => v < mean);
            if (allAbove || allBelow)
            {
                result.IsViolation = true;
                result.Violations.Add("Rule 2: 9 consecutive points on the same side of the mean");
            }
        }

        // 4. Rule 3: 6 consecutive points steadily increasing or steadily decreasing
        if (allValues.Count >= 6)
        {
            var last6 = allValues.TakeLast(6).ToList();
            bool increasing = true;
            bool decreasing = true;
            for (int i = 1; i < last6.Count; i++)
            {
                if (last6[i] <= last6[i - 1])
                {
                    increasing = false;
                }
                if (last6[i] >= last6[i - 1])
                {
                    decreasing = false;
                }
            }

            if (increasing || decreasing)
            {
                result.IsViolation = true;
                result.Violations.Add(increasing 
                    ? "Rule 3: 6 consecutive points steadily increasing" 
                    : "Rule 3: 6 consecutive points steadily decreasing");
            }
        }

        return result;
    }
}
