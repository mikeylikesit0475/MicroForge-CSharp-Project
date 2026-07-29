using MicroForge.Domain.Entities;
using MicroForge.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MicroForge.Application.Services;

public class SelfHealingResultDto
{
    public int LotId { get; set; }
    public string LotNumber { get; set; } = string.Empty;
    public string OriginalStatus { get; set; } = string.Empty;
    public string NewStatus { get; set; } = string.Empty;
    public string AnomalyDescription { get; set; } = string.Empty;
    public string AssignedReworkStep { get; set; } = string.Empty;
    public int CapaId { get; set; }
    public int EcoId { get; set; }
    public string RemediationSummary { get; set; } = string.Empty;
    public DateTime RemediationTimestamp { get; set; } = DateTime.UtcNow;
}

public interface IFabSelfHealingService
{
    Task<SelfHealingResultDto?> TriggerSelfHealingAsync(int lotId, string anomalyDescription, CancellationToken ct = default);
    Task<List<SelfHealingResultDto>> GetRecentRemediationsAsync(CancellationToken ct = default);
}

public class FabSelfHealingService : IFabSelfHealingService
{
    private readonly IRepository<Lot> _lotRepo;
    private readonly IRepository<RouteStep> _routeStepRepo;
    private readonly IRepository<LotMovementLog> _movementRepo;
    private readonly IRepository<CapaAction> _capaRepo;
    private readonly IRepository<EcoRecord> _ecoRepo;

    private static readonly List<SelfHealingResultDto> _remediationHistory = new();

    public FabSelfHealingService(
        IRepository<Lot> lotRepo,
        IRepository<RouteStep> routeStepRepo,
        IRepository<LotMovementLog> movementRepo,
        IRepository<CapaAction> capaRepo,
        IRepository<EcoRecord> ecoRepo)
    {
        _lotRepo = lotRepo;
        _routeStepRepo = routeStepRepo;
        _movementRepo = movementRepo;
        _capaRepo = capaRepo;
        _ecoRepo = ecoRepo;
    }

    public async Task<SelfHealingResultDto?> TriggerSelfHealingAsync(int lotId, string anomalyDescription, CancellationToken ct = default)
    {
        var lot = await _lotRepo.GetByIdAsync(lotId, ct);
        if (lot == null) return null;

        var originalStatus = lot.Status.ToString();

        // 1. Quarantine Lot
        lot.Status = LotStatus.Hold;
        _lotRepo.Update(lot);
        await _lotRepo.SaveChangesAsync(ct);

        // 2. Find Rework Route Step (e.g. WAFER_PREP or CLEAN)
        var allSteps = (await _routeStepRepo.ListAsync(ct)).OrderBy(s => s.SequenceOrder).ToList();
        var reworkStep = allSteps.FirstOrDefault(s => s.StepName.Contains("PREP") || s.StepName.Contains("CLEAN")) ?? allSteps.FirstOrDefault();
        var reworkStepName = reworkStep?.StepName ?? "REWORK_CLEAN";

        if (reworkStep != null)
        {
            lot.ActiveRouteStepId = reworkStep.Id;
            _lotRepo.Update(lot);
            await _lotRepo.SaveChangesAsync(ct);

            // Log movement
            await _movementRepo.AddAsync(new LotMovementLog
            {
                LotId = lot.Id,
                RouteStepId = reworkStep.Id,
                EnteredAt = DateTime.UtcNow,
                OperatorId = 1
            }, ct);
            await _movementRepo.SaveChangesAsync(ct);
        }

        // 3. Create CAPA Action Item
        var capa = new CapaAction
        {
            LotId = lot.Id,
            ActionDescription = $"[Autonomous Swarm] Remediate {anomalyDescription} on {lot.LotNumber}",
            AssignedTo = "Swarm Auto-Remediation Agent",
            Status = "Pending Review",
            ApprovedBy = "Agent Swarm",
            CreatedAt = DateTime.UtcNow
        };
        await _capaRepo.AddAsync(capa, ct);
        await _capaRepo.SaveChangesAsync(ct);

        // 4. Create Draft ECO for Recipe Adjustment
        var eco = new EcoRecord
        {
            Title = $"[Autonomous Recalibration] {lot.RecipeName} Parameter Tuning",
            RecipeName = lot.RecipeName,
            ChangeDetail = $"Automated drift correction: Adjust gas flow and RF power compensation for {anomalyDescription}.",
            Status = "Submitted",
            ApprovedBy = "Agent Swarm",
            CreatedAt = DateTime.UtcNow
        };
        await _ecoRepo.AddAsync(eco, ct);
        await _ecoRepo.SaveChangesAsync(ct);

        var result = new SelfHealingResultDto
        {
            LotId = lot.Id,
            LotNumber = lot.LotNumber,
            OriginalStatus = originalStatus,
            NewStatus = lot.Status.ToString(),
            AnomalyDescription = anomalyDescription,
            AssignedReworkStep = reworkStepName,
            CapaId = capa.Id,
            EcoId = eco.Id,
            RemediationSummary = $"Autonomous Swarm quarantined {lot.LotNumber}, created CAPA #{capa.Id}, drafted ECO #{eco.Id}, and rerouted lot to {reworkStepName}.",
            RemediationTimestamp = DateTime.UtcNow
        };

        _remediationHistory.Add(result);
        return result;
    }

    public Task<List<SelfHealingResultDto>> GetRecentRemediationsAsync(CancellationToken ct = default)
    {
        return Task.FromResult(_remediationHistory.OrderByDescending(r => r.RemediationTimestamp).ToList());
    }
}
