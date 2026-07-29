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
    public decimal Usl { get; set; }
    public decimal Lsl { get; set; }
    public decimal Cpk { get; set; }
    public decimal Ppk { get; set; }
}

public static class SpcCalculator
{
    public static SpcEvaluationResult Evaluate(decimal currentValue, List<decimal> history, decimal? targetUsl = null, decimal? targetLsl = null)
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
        var variance = history.Select(v => Math.Pow((double)(v - mean), 2)).Average();
        var stdDev = (decimal)Math.Sqrt(variance);

        result.Mean = Math.Round(mean, 4);
        result.StdDev = Math.Round(stdDev, 4);
        result.Ucl = Math.Round(mean + 3 * stdDev, 4);
        result.Lcl = Math.Round(mean - 3 * stdDev, 4);

        // 2. Set USL and LSL (default to 3-sigma if not provided)
        result.Usl = targetUsl ?? result.Ucl;
        result.Lsl = targetLsl ?? result.Lcl;

        // 3. Calculate Cpk & Ppk Process Capability Indices
        if (stdDev > 0)
        {
            var cpu = (result.Usl - result.Mean) / (3 * stdDev);
            var cpl = (result.Mean - result.Lsl) / (3 * stdDev);
            result.Cpk = Math.Round(Math.Min(cpu, cpl), 4);

            // Sample standard deviation (s) with n-1 degrees of freedom for Ppk
            var sampleVariance = history.Count > 1
                ? history.Select(v => Math.Pow((double)(v - mean), 2)).Sum() / (history.Count - 1)
                : variance;
            var sampleStdDev = (decimal)Math.Sqrt(sampleVariance);

            if (sampleStdDev > 0)
            {
                var ppu = (result.Usl - result.Mean) / (3 * sampleStdDev);
                var ppl = (result.Mean - result.Lsl) / (3 * sampleStdDev);
                result.Ppk = Math.Round(Math.Min(ppu, ppl), 4);
            }
            else
            {
                result.Ppk = result.Cpk;
            }
        }
        else
        {
            result.Cpk = 1.33m;
            result.Ppk = 1.33m;
        }

        // 4. Rule 1: 1 point outside 3-sigma limits
        if (currentValue > result.Ucl || currentValue < result.Lcl)
        {
            result.IsViolation = true;
            result.Violations.Add("Rule 1: Point outside 3-sigma control limits");
        }

        // 5. Rule 2: 9 consecutive points on the same side of the mean
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

        // 6. Rule 3: 6 consecutive points steadily increasing or steadily decreasing
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
