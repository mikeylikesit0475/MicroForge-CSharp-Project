using MicroForge.Domain.Entities;
using MicroForge.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MicroForge.Application.Services;

public class WipService : IWipService
{
    private readonly IRepository<Lot> _lotRepo;
    private readonly IRepository<Wafer> _waferRepo;
    private readonly IRepository<RouteStep> _stepRepo;
    private readonly IRepository<LotMovementLog> _logRepo;
    private readonly IRepository<ToolQualification> _qualRepo;
    private readonly IRepository<WorkInstruction> _wiRepo;
    private readonly IRepository<LotChecklistProgress> _progressRepo;
    private readonly IRepository<ProductionLine> _lineRepo;

    public WipService(
        IRepository<Lot> lotRepo,
        IRepository<Wafer> waferRepo,
        IRepository<RouteStep> stepRepo,
        IRepository<LotMovementLog> logRepo,
        IRepository<ToolQualification> qualRepo,
        IRepository<WorkInstruction> wiRepo,
        IRepository<LotChecklistProgress> progressRepo,
        IRepository<ProductionLine> lineRepo)
    {
        _lotRepo = lotRepo;
        _waferRepo = waferRepo;
        _stepRepo = stepRepo;
        _logRepo = logRepo;
        _qualRepo = qualRepo;
        _wiRepo = wiRepo;
        _progressRepo = progressRepo;
        _lineRepo = lineRepo;
    }

    public async Task<List<LotDto>> GetActiveLotsAsync(CancellationToken ct = default)
    {
        var lots = await _lotRepo.Query()
            .Include(l => l.ActiveRouteStep)
            .Include(l => l.Wafers)
            .OrderByDescending(l => l.CreatedAt)
            .Select(l => new LotDto
            {
                Id = l.Id,
                LotNumber = l.LotNumber,
                Status = l.Status.ToString(),
                RecipeName = l.RecipeName,
                ActiveRouteStepId = l.ActiveRouteStepId,
                ActiveRouteStepName = l.ActiveRouteStep != null ? l.ActiveRouteStep.StepName : "Unknown",
                WaferCount = l.Wafers.Count,
                CreatedAt = l.CreatedAt
            })
            .ToListAsync(ct);
        return lots;
    }

    public async Task<LotDetailDto?> GetLotDetailAsync(int lotId, CancellationToken ct = default)
    {
        var lot = await _lotRepo.Query()
            .Include(l => l.ActiveRouteStep)
            .Include(l => l.Wafers)
            .Include(l => l.MovementLogs)
                .ThenInclude(m => m.RouteStep)
            .FirstOrDefaultAsync(l => l.Id == lotId, ct);

        if (lot == null) return null;

        return new LotDetailDto
        {
            Id = lot.Id,
            LotNumber = lot.LotNumber,
            Status = lot.Status.ToString(),
            RecipeName = lot.RecipeName,
            ActiveRouteStep = lot.ActiveRouteStep != null ? new RouteStepDto
            {
                Id = lot.ActiveRouteStep.Id,
                StepName = lot.ActiveRouteStep.StepName,
                SequenceOrder = lot.ActiveRouteStep.SequenceOrder,
                Description = lot.ActiveRouteStep.Description
            } : null,
            Wafers = lot.Wafers.Select(w => new WaferDto
            {
                Id = w.Id,
                WaferId = w.WaferId,
                LotId = w.LotId,
                SlotPosition = w.SlotPosition,
                Status = w.Status
            }).ToList(),
            MovementLogs = lot.MovementLogs.Select(m => new LotMovementLogDto
            {
                Id = m.Id,
                StepName = m.RouteStep != null ? m.RouteStep.StepName : "Unknown",
                EnteredAt = m.EnteredAt,
                ExitedAt = m.ExitedAt,
                OperatorId = m.OperatorId
            }).OrderByDescending(m => m.EnteredAt).ToList(),
            CreatedAt = lot.CreatedAt
        };
    }

    public async Task<List<RouteStepDto>> GetRouteStepsAsync(CancellationToken ct = default)
    {
        return await _stepRepo.Query()
            .OrderBy(r => r.SequenceOrder)
            .Select(r => new RouteStepDto
            {
                Id = r.Id,
                StepName = r.StepName,
                SequenceOrder = r.SequenceOrder,
                Description = r.Description
            })
            .ToListAsync(ct);
    }

    public async Task<bool> MoveLotToStepAsync(int lotId, int targetRouteStepId, int operatorId, CancellationToken ct = default)
    {
        var lot = await _lotRepo.Query()
            .Include(l => l.MovementLogs)
            .FirstOrDefaultAsync(l => l.Id == lotId, ct);

        if (lot == null || lot.Status == LotStatus.Hold || lot.Status == LotStatus.Completed)
        {
            // Block routing if the lot is held or completed
            return false;
        }

        var targetStep = await _stepRepo.GetByIdAsync(targetRouteStepId, ct);
        if (targetStep == null) return false;

        // 1. Validate Work Instruction checklist at current step
        var mandatoryInstructions = await _wiRepo.Query()
            .Where(w => w.RouteStepId == lot.ActiveRouteStepId && w.IsMandatory)
            .Select(w => w.Id)
            .ToListAsync(ct);

        if (mandatoryInstructions.Count > 0)
        {
            var completedCount = await _progressRepo.Query()
                .Where(p => p.LotId == lotId && mandatoryInstructions.Contains(p.WorkInstructionId) && p.IsCompleted)
                .CountAsync(ct);

            if (completedCount < mandatoryInstructions.Count)
            {
                // Block: Checklist not fully completed!
                return false;
            }
        }

        // 2. Validate Tool Qualification for target step
        var matchingLine = await _lineRepo.Query()
            .FirstOrDefaultAsync(l => l.LineType.ToLower().Contains(targetStep.StepName.ToLower()) || targetStep.StepName.ToLower().Contains(l.LineType.ToLower()), ct);

        if (matchingLine != null)
        {
            var qual = await _qualRepo.Query()
                .FirstOrDefaultAsync(q => q.ProductionLineId == matchingLine.Id && q.RecipeName == lot.RecipeName, ct);

            if (qual != null && !qual.IsQualified)
            {
                // Block: Target chamber is not qualified for recipe
                return false;
            }
        }

        // Close current movement log
        var currentLog = lot.MovementLogs.FirstOrDefault(m => m.ExitedAt == null);
        if (currentLog != null)
        {
            currentLog.ExitedAt = DateTime.UtcNow;
            _logRepo.Update(currentLog);
        }

        // Move to target step
        lot.ActiveRouteStepId = targetRouteStepId;
        
        // Add new movement log
        var newLog = new LotMovementLog
        {
            LotId = lotId,
            RouteStepId = targetRouteStepId,
            EnteredAt = DateTime.UtcNow,
            OperatorId = operatorId
        };
        await _logRepo.AddAsync(newLog, ct);
        
        // If the target step is the final step, mark the lot completed
        var maxOrder = await _stepRepo.Query().MaxAsync(s => s.SequenceOrder, ct);
        if (targetStep.SequenceOrder == maxOrder)
        {
            lot.Status = LotStatus.Completed;
            newLog.ExitedAt = DateTime.UtcNow;
        }

        _lotRepo.Update(lot);
        await _lotRepo.SaveChangesAsync(ct);
        await _logRepo.SaveChangesAsync(ct);

        return true;
    }

    public async Task<bool> HoldLotAsync(int lotId, string reason, CancellationToken ct = default)
    {
        var lot = await _lotRepo.GetByIdAsync(lotId, ct);
        if (lot == null || lot.Status == LotStatus.Completed) return false;

        lot.Status = LotStatus.Hold;
        _lotRepo.Update(lot);
        await _lotRepo.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> ReleaseLotAsync(int lotId, CancellationToken ct = default)
    {
        var lot = await _lotRepo.GetByIdAsync(lotId, ct);
        if (lot == null || lot.Status != LotStatus.Hold) return false;

        lot.Status = LotStatus.Active;
        _lotRepo.Update(lot);
        await _lotRepo.SaveChangesAsync(ct);
        return true;
    }

    public async Task<LotDto> CreateLotAsync(string lotNumber, string recipeName, List<string> waferIds, CancellationToken ct = default)
    {
        // Get initial route step
        var firstStep = await _stepRepo.Query()
            .OrderBy(r => r.SequenceOrder)
            .FirstOrDefaultAsync(ct);

        if (firstStep == null)
        {
            // Seed default steps if they don't exist yet
            var defaultSteps = new List<RouteStep>
            {
                new() { StepName = "CLEAN", SequenceOrder = 1, Description = "Chemical pre-clean step" },
                new() { StepName = "LITHO", SequenceOrder = 2, Description = "Photolithography pattern step" },
                new() { StepName = "ETCH", SequenceOrder = 3, Description = "Plasma etch process step" },
                new() { StepName = "METROLOGY", SequenceOrder = 4, Description = "Thickness and CD inspection" },
                new() { StepName = "COMPLETED", SequenceOrder = 5, Description = "Final inspection and pack out" }
            };
            foreach (var step in defaultSteps)
            {
                await _stepRepo.AddAsync(step, ct);
            }
            await _stepRepo.SaveChangesAsync(ct);
            firstStep = defaultSteps[0];
        }

        var lot = new Lot
        {
            LotNumber = lotNumber,
            RecipeName = recipeName,
            ActiveRouteStepId = firstStep.Id,
            Status = LotStatus.Active,
            CreatedAt = DateTime.UtcNow
        };

        await _lotRepo.AddAsync(lot, ct);
        await _lotRepo.SaveChangesAsync(ct);

        // Add wafers
        int slot = 1;
        foreach (var id in waferIds)
        {
            var wafer = new Wafer
            {
                WaferId = id,
                LotId = lot.Id,
                SlotPosition = slot++,
                Status = "Good"
            };
            await _waferRepo.AddAsync(wafer, ct);
        }
        await _waferRepo.SaveChangesAsync(ct);

        // Add initial movement log
        var log = new LotMovementLog
        {
            LotId = lot.Id,
            RouteStepId = firstStep.Id,
            EnteredAt = DateTime.UtcNow,
            OperatorId = 1
        };
        await _logRepo.AddAsync(log, ct);
        await _logRepo.SaveChangesAsync(ct);

        return new LotDto
        {
            Id = lot.Id,
            LotNumber = lot.LotNumber,
            Status = lot.Status.ToString(),
            RecipeName = lot.RecipeName,
            ActiveRouteStepId = lot.ActiveRouteStepId,
            ActiveRouteStepName = firstStep.StepName,
            WaferCount = waferIds.Count,
            CreatedAt = lot.CreatedAt
        };
    }

    public async Task<List<WorkInstruction>> GetWorkInstructionsAsync(int stepId, CancellationToken ct = default)
    {
        var instructions = await _wiRepo.Query()
            .Where(w => w.RouteStepId == stepId)
            .ToListAsync(ct);

        if (instructions.Count == 0)
        {
            var step = await _stepRepo.GetByIdAsync(stepId, ct);
            if (step != null)
            {
                var defaults = new List<WorkInstruction>
                {
                    new() { RouteStepId = stepId, TaskDescription = $"Verify {step.StepName} tool calibration parameters", IsMandatory = true },
                    new() { RouteStepId = stepId, TaskDescription = $"Check physical cassette slot alignment", IsMandatory = true },
                    new() { RouteStepId = stepId, TaskDescription = $"Log particle pre-inspection count", IsMandatory = false }
                };
                foreach (var d in defaults)
                {
                    await _wiRepo.AddAsync(d, ct);
                }
                await _wiRepo.SaveChangesAsync(ct);
                instructions.AddRange(defaults);
            }
        }

        return instructions;
    }

    public async Task<List<LotChecklistProgress>> GetChecklistProgressAsync(int lotId, CancellationToken ct = default)
    {
        return await _progressRepo.Query()
            .Where(p => p.LotId == lotId)
            .ToListAsync(ct);
    }

    public async Task<bool> UpdateChecklistProgressAsync(int lotId, int instructionId, bool completed, int operatorId, CancellationToken ct = default)
    {
        var progress = await _progressRepo.Query()
            .FirstOrDefaultAsync(p => p.LotId == lotId && p.WorkInstructionId == instructionId, ct);

        if (progress == null)
        {
            progress = new LotChecklistProgress
            {
                LotId = lotId,
                WorkInstructionId = instructionId,
                IsCompleted = completed,
                CompletedAt = completed ? DateTime.UtcNow : null,
                CompletedByOperatorId = operatorId
            };
            await _progressRepo.AddAsync(progress, ct);
        }
        else
        {
            progress.IsCompleted = completed;
            progress.CompletedAt = completed ? DateTime.UtcNow : null;
            progress.CompletedByOperatorId = operatorId;
            _progressRepo.Update(progress);
        }

        await _progressRepo.SaveChangesAsync(ct);
        return true;
    }

    public async Task<List<ToolQualification>> GetQualificationsAsync(CancellationToken ct = default)
    {
        var list = await _qualRepo.ListAsync(ct);
        if (list.Count == 0)
        {
            var lines = await _lineRepo.ListAsync(ct);
            foreach (var line in lines)
            {
                var seed = new ToolQualification
                {
                    ProductionLineId = line.Id,
                    RecipeName = "POLY-ETCH-01",
                    IsQualified = true
                };
                await _qualRepo.AddAsync(seed, ct);
                list.Add(seed);
            }
            await _qualRepo.SaveChangesAsync(ct);
        }
        return list;
    }

    public async Task<bool> SetQualificationAsync(int lineId, string recipeName, bool qualified, CancellationToken ct = default)
    {
        var qual = await _qualRepo.Query()
            .FirstOrDefaultAsync(q => q.ProductionLineId == lineId && q.RecipeName == recipeName, ct);

        if (qual == null)
        {
            qual = new ToolQualification
            {
                ProductionLineId = lineId,
                RecipeName = recipeName,
                IsQualified = qualified
            };
            await _qualRepo.AddAsync(qual, ct);
        }
        else
        {
            qual.IsQualified = qualified;
            qual.QualifiedAt = DateTime.UtcNow;
            _qualRepo.Update(qual);
        }

        await _qualRepo.SaveChangesAsync(ct);
        return true;
    }
}
