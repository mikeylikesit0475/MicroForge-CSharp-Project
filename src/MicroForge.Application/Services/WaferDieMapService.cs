using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MicroForge.Application.Services;

public class DieDetailDto
{
    public string DieId { get; set; } = string.Empty;
    public decimal CoordX { get; set; }
    public decimal CoordY { get; set; }
    public string Status { get; set; } = "Pass"; // Pass, Fail, Marginal
    public string DefectClass { get; set; } = "None"; // None, RingDefect, EdgeFallout, CenterSpot, ScratchCluster
    public decimal CriticalDimensionNm { get; set; }
    public decimal BreakdownVoltageV { get; set; }
}

public class WaferDieMapResultDto
{
    public int LotId { get; set; }
    public string WaferId { get; set; } = string.Empty;
    public int TotalDies { get; set; }
    public int PassingDies { get; set; }
    public int FailingDies { get; set; }
    public decimal YieldPercentage { get; set; }
    public List<DieDetailDto> Dies { get; set; } = new();
}

public interface IWaferDieMapService
{
    Task<WaferDieMapResultDto> GenerateWaferDieMapAsync(int lotId, string waferIdPattern = "", CancellationToken ct = default);
}

public class WaferDieMapService : IWaferDieMapService
{
    public Task<WaferDieMapResultDto> GenerateWaferDieMapAsync(int lotId, string waferIdPattern = "", CancellationToken ct = default)
    {
        var radius = 140.0; // 300mm wafer radius (150mm radius with 10mm edge exclusion)
        var step = 14.0;   // 14mm die pitch size
        var rng = new Random(lotId * 37);

        var dies = new List<DieDetailDto>();
        var dieIndex = 1;

        // Select spatial defect pattern based on lotId
        var patternType = (lotId % 4) switch
        {
            0 => "RingDefect",
            1 => "EdgeFallout",
            2 => "CenterSpot",
            _ => "ScratchCluster"
        };

        for (var x = -radius; x <= radius; x += step)
        {
            for (var y = -radius; y <= radius; y += step)
            {
                var distanceFromCenter = Math.Sqrt(x * x + y * y);
                if (distanceFromCenter > radius) continue; // Outside 300mm wafer boundary

                var isMarginal = false;
                var isFail = false;
                var defectClass = "None";

                // Apply spatial defect models
                if (patternType == "RingDefect" && distanceFromCenter >= 70 && distanceFromCenter <= 100)
                {
                    if (rng.NextDouble() < 0.75) { isFail = true; defectClass = "RingDefect"; }
                }
                else if (patternType == "EdgeFallout" && distanceFromCenter >= 120)
                {
                    if (rng.NextDouble() < 0.85) { isFail = true; defectClass = "EdgeFallout"; }
                }
                else if (patternType == "CenterSpot" && distanceFromCenter <= 40)
                {
                    if (rng.NextDouble() < 0.80) { isFail = true; defectClass = "CenterSpot"; }
                }
                else if (patternType == "ScratchCluster" && Math.Abs(x - y) < 20 && x > -50 && x < 50)
                {
                    if (rng.NextDouble() < 0.70) { isFail = true; defectClass = "ScratchCluster"; }
                }

                // Random 2% background defect rate
                if (!isFail && rng.NextDouble() < 0.02)
                {
                    isFail = true;
                    defectClass = "Particulate";
                }
                else if (!isFail && rng.NextDouble() < 0.04)
                {
                    isMarginal = true;
                }

                var cdBase = 14.0m + (decimal)(rng.NextDouble() * 0.4 - 0.2);
                var vbdBase = 3.3m + (decimal)(rng.NextDouble() * 0.1 - 0.05);

                if (isFail) cdBase += 1.8m;

                dies.Add(new DieDetailDto
                {
                    DieId = $"DIE-{dieIndex++:D4}",
                    CoordX = Math.Round((decimal)x, 1),
                    CoordY = Math.Round((decimal)y, 1),
                    Status = isFail ? "Fail" : (isMarginal ? "Marginal" : "Pass"),
                    DefectClass = defectClass,
                    CriticalDimensionNm = Math.Round(cdBase, 2),
                    BreakdownVoltageV = Math.Round(vbdBase, 2)
                });
            }
        }

        var passing = dies.Count(d => d.Status == "Pass");
        var failing = dies.Count(d => d.Status == "Fail");
        var yieldPct = dies.Count > 0 ? Math.Round((decimal)passing / dies.Count * 100m, 2) : 100m;

        var result = new WaferDieMapResultDto
        {
            LotId = lotId,
            WaferId = string.IsNullOrEmpty(waferIdPattern) ? $"LOT-{lotId:D3}-W13" : waferIdPattern,
            TotalDies = dies.Count,
            PassingDies = passing,
            FailingDies = failing,
            YieldPercentage = yieldPct,
            Dies = dies
        };

        return Task.FromResult(result);
    }
}
