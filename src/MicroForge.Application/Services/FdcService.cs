using MicroForge.Domain.Entities;
using MicroForge.Domain.Interfaces;
using MicroForge.Domain.Analytics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MicroForge.Application.Services;

public class FdcService : IFdcService
{
    private readonly IRepository<ProductionLine> _lineRepo;
    private readonly IRepository<SensorReading> _readingRepo;
    private readonly IRepository<ProductionRun> _runRepo;

    public FdcService(
        IRepository<ProductionLine> lineRepo,
        IRepository<SensorReading> readingRepo,
        IRepository<ProductionRun> runRepo)
    {
        _lineRepo = lineRepo;
        _readingRepo = readingRepo;
        _runRepo = runRepo;
    }

    public async Task<List<ChamberMatchingSummaryDto>> GetChamberMatchingAsync(CancellationToken ct = default)
    {
        var activeLines = await _lineRepo.Query().Where(l => l.IsActive).ToListAsync(ct);
        var sensorTypes = new[] { "Temperature", "Pressure" };
        var summaries = new List<ChamberMatchingSummaryDto>();

        foreach (var type in sensorTypes)
        {
            var chamberMetrics = new List<ChamberMetricDto>();
            foreach (var line in activeLines)
            {
                var readings = await _readingRepo.Query()
                    .Where(r => r.ProductionLineId == line.Id && r.SensorType == type)
                    .OrderByDescending(r => r.Timestamp)
                    .Take(50)
                    .Select(r => r.Value)
                    .ToListAsync(ct);

                if (readings.Count == 0) continue;

                var avg = readings.Average();
                var variance = (decimal)readings.Select(v => Math.Pow((double)(v - avg), 2)).Average();

                chamberMetrics.Add(new ChamberMetricDto
                {
                    ProductionLineId = line.Id,
                    ProductionLineName = line.Name,
                    SensorType = type,
                    AverageValue = Math.Round(avg, 2),
                    Variance = Math.Round(variance, 4)
                });
            }

            if (chamberMetrics.Count == 0) continue;

            var groupAvg = chamberMetrics.Average(c => c.AverageValue);

            foreach (var metric in chamberMetrics)
            {
                // Flag as mismatched if average deviates by > 15% from group average
                var devPct = groupAvg > 0 ? Math.Abs(metric.AverageValue - groupAvg) / groupAvg : 0;
                if (devPct > 0.15m)
                {
                    metric.IsMismatched = true;
                    metric.Status = "Mismatched";
                }
            }

            summaries.Add(new ChamberMatchingSummaryDto
            {
                SensorType = type,
                GroupAverage = Math.Round(groupAvg, 2),
                Chambers = chamberMetrics
            });
        }

        return summaries;
    }

    public async Task<List<ToolMaintenanceStatusDto>> GetMaintenanceStatusAsync(CancellationToken ct = default)
    {
        var activeLines = await _lineRepo.Query().Where(l => l.IsActive).ToListAsync(ct);
        var results = new List<ToolMaintenanceStatusDto>();

        foreach (var line in activeLines)
        {
            // Seed a realistic baseline hours count depending on Line ID, plus any actual simulated run hours.
            // In a real fab, this would be computed by summing actual run durations.
            var completedRuns = await _runRepo.Query()
                .Where(r => r.ProductionLineId == line.Id && r.Status == "Completed")
                .ToListAsync(ct);

            var simulatedHours = completedRuns.Count * 2.0; // Assume 2 hours per completed run
            var baselineHours = (line.Id * 230) + 120; // 350 hrs for Line 1, 580 hrs for Line 2, etc.
            var totalHours = baselineHours + simulatedHours;

            var parts = new List<PartRulDto>
            {
                CalculatePartRul("Focus Ring", totalHours, 1000),
                CalculatePartRul("Showerhead Nozzle", totalHours, 800),
                CalculatePartRul("Electrostatic Chuck", totalHours, 1200)
            };

            results.Add(new ToolMaintenanceStatusDto
            {
                ProductionLineId = line.Id,
                ProductionLineName = line.Name,
                TotalRunHours = Math.Round(totalHours, 1),
                Parts = parts
            });
        }

        return results;
    }

    public async Task<List<string>> GetActiveFaultsAsync(CancellationToken ct = default)
    {
        var activeLines = await _lineRepo.Query().Where(l => l.IsActive).ToListAsync(ct);
        var sensorTypes = new[] { "Temperature", "Pressure", "Vibration" };
        var activeFaults = new List<string>();

        foreach (var line in activeLines)
        {
            foreach (var type in sensorTypes)
            {
                var values = await _readingRepo.Query()
                    .Where(r => r.ProductionLineId == line.Id && r.SensorType == type)
                    .OrderByDescending(r => r.Timestamp)
                    .Take(15)
                    .Select(r => r.Value)
                    .ToListAsync(ct);

                if (values.Count < 5) continue;

                // Chronological order is required by FdcAnalyzer
                values.Reverse();

                var fdcResult = FdcAnalyzer.Analyze(type, values);
                if (fdcResult.FaultDetected)
                {
                    activeFaults.Add($"[Line {line.Id} - {line.Name}] FDC {fdcResult.Severity} Alarm: {fdcResult.FaultName}. {fdcResult.Description}");
                }
            }
        }

        return activeFaults;
    }

    private static PartRulDto CalculatePartRul(string partName, double totalHours, double maxHours)
    {
        var currentCycleHours = totalHours % maxHours;
        var remainingHours = maxHours - currentCycleHours;
        var remainingPct = (decimal)(remainingHours / maxHours * 100);

        var status = "Good";
        if (remainingPct < 15) status = "Critical";
        else if (remainingPct < 35) status = "Warning";

        return new PartRulDto
        {
            PartName = partName,
            RemainingLifePercentage = Math.Round(remainingPct, 1),
            EstimatedHoursRemaining = Math.Round(remainingHours, 1),
            Status = status
        };
    }
}
