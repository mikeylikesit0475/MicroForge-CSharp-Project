using FluentAssertions;
using MicroForge.Application.Services;
using MicroForge.Domain.Entities;
using MicroForge.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MicroForge.UnitTests;

public class ClosedLoopTests : TestBase
{
    [Fact]
    public async Task move_lot_blocked_if_ewi_checklist_is_incomplete()
    {
        // Arrange
        var db = DbContext;
        var service = new WipService(
            new Repository<Lot>(db),
            new Repository<Wafer>(db),
            new Repository<RouteStep>(db),
            new Repository<LotMovementLog>(db),
            new Repository<ToolQualification>(db),
            new Repository<WorkInstruction>(db),
            new Repository<LotChecklistProgress>(db),
            new Repository<ProductionLine>(db)
        );

        var lotDto = await service.CreateLotAsync("LOT-CHECK-101", "POLY-ETCH-01", new List<string> { "W-1" }, CancellationToken.None);
        var steps = await service.GetRouteStepsAsync();
        var nextStep = steps.First(s => s.StepName == "LITHO");

        // Seed a mandatory work instruction for the current step (CLEAN)
        var instruction = new WorkInstruction
        {
            RouteStepId = lotDto.ActiveRouteStepId,
            TaskDescription = "Check pre-clean acid concentration",
            IsMandatory = true
        };
        db.WorkInstructions.Add(instruction);
        await db.SaveChangesAsync();

        // Act - Attempt move without completing checklist
        var success = await service.MoveLotToStepAsync(lotDto.Id, nextStep.Id, 1, CancellationToken.None);

        // Assert
        success.Should().BeFalse(); // Move should be blocked
        
        // Complete the checklist item
        await service.UpdateChecklistProgressAsync(lotDto.Id, instruction.Id, true, 1, CancellationToken.None);

        // Act again
        var successAfterChecklist = await service.MoveLotToStepAsync(lotDto.Id, nextStep.Id, 1, CancellationToken.None);

        // Assert
        successAfterChecklist.Should().BeTrue(); // Move should succeed now
    }

    [Fact]
    public async Task move_lot_blocked_if_target_tool_unqualified_for_recipe()
    {
        // Arrange
        var db = DbContext;
        var service = new WipService(
            new Repository<Lot>(db),
            new Repository<Wafer>(db),
            new Repository<RouteStep>(db),
            new Repository<LotMovementLog>(db),
            new Repository<ToolQualification>(db),
            new Repository<WorkInstruction>(db),
            new Repository<LotChecklistProgress>(db),
            new Repository<ProductionLine>(db)
        );

        var lotDto = await service.CreateLotAsync("LOT-QUAL-101", "UNQUALIFIED-RECIPE", new List<string> { "W-1" }, CancellationToken.None);
        var steps = await service.GetRouteStepsAsync();
        var nextStep = steps.First(s => s.StepName == "LITHO");

        // Seed a production line of type "LITHO"
        var line = new ProductionLine { Name = "Litho Scanner A", LineType = "LITHO", IsActive = true };
        db.ProductionLines.Add(line);
        await db.SaveChangesAsync();

        // Mark this recipe as unqualified for this line
        var qual = new ToolQualification
        {
            ProductionLineId = line.Id,
            RecipeName = "UNQUALIFIED-RECIPE",
            IsQualified = false
        };
        db.ToolQualifications.Add(qual);
        await db.SaveChangesAsync();

        // Act
        var success = await service.MoveLotToStepAsync(lotDto.Id, nextStep.Id, 1, CancellationToken.None);

        // Assert
        success.Should().BeFalse(); // Target chamber is not qualified for recipe
    }
}
