using FluentAssertions;
using MicroForge.Application.Services;
using MicroForge.Domain.Entities;
using MicroForge.Domain.Interfaces;
using MicroForge.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MicroForge.UnitTests;

public class WipServiceTests : TestBase
{
    [Fact]
    public async Task create_lot_seeds_default_steps_and_initializes_wafers()
    {
        // Arrange
        var service = new WipService(
            new Repository<Lot>(DbContext),
            new Repository<Wafer>(DbContext),
            new Repository<RouteStep>(DbContext),
            new Repository<LotMovementLog>(DbContext),
            new Repository<ToolQualification>(DbContext),
            new Repository<WorkInstruction>(DbContext),
            new Repository<LotChecklistProgress>(DbContext),
            new Repository<ProductionLine>(DbContext));

        var waferIds = new List<string> { "W-01", "W-02", "W-03" };

        // Act
        var lotDto = await service.CreateLotAsync("LOT-999", "RECIPE-A", waferIds, CancellationToken.None);

        // Assert
        lotDto.LotNumber.Should().Be("LOT-999");
        lotDto.RecipeName.Should().Be("RECIPE-A");
        lotDto.WaferCount.Should().Be(3);
        lotDto.ActiveRouteStepName.Should().Be("CLEAN");

        var dbLot = await DbContext.Lots.Include(l => l.Wafers).Include(l => l.MovementLogs).FirstOrDefaultAsync(l => l.Id == lotDto.Id);
        dbLot.Should().NotBeNull();
        dbLot!.Wafers.Count.Should().Be(3);
        dbLot.MovementLogs.Count.Should().Be(1);
    }

    [Fact]
    public async Task move_lot_updates_step_and_closes_previous_log()
    {
        // Arrange
        var service = new WipService(
            new Repository<Lot>(DbContext),
            new Repository<Wafer>(DbContext),
            new Repository<RouteStep>(DbContext),
            new Repository<LotMovementLog>(DbContext),
            new Repository<ToolQualification>(DbContext),
            new Repository<WorkInstruction>(DbContext),
            new Repository<LotChecklistProgress>(DbContext),
            new Repository<ProductionLine>(DbContext));

        var lotDto = await service.CreateLotAsync("LOT-001", "RECIPE-A", new List<string> { "W-01" }, CancellationToken.None);
        var steps = await service.GetRouteStepsAsync();
        var nextStep = steps.First(s => s.StepName == "LITHO");

        // Act
        var success = await service.MoveLotToStepAsync(lotDto.Id, nextStep.Id, 42, CancellationToken.None);

        // Assert
        success.Should().BeTrue();
        var detail = await service.GetLotDetailAsync(lotDto.Id);
        detail!.ActiveRouteStep!.StepName.Should().Be("LITHO");
        detail.MovementLogs.Count.Should().Be(2);
        
        // Previous log must be closed
        var closedLog = DbContext.LotMovementLogs.First(m => m.RouteStepId == lotDto.ActiveRouteStepId);
        closedLog.ExitedAt.Should().NotBeNull();
    }

    [Fact]
    public async Task move_lot_blocked_when_status_is_hold()
    {
        // Arrange
        var service = new WipService(
            new Repository<Lot>(DbContext),
            new Repository<Wafer>(DbContext),
            new Repository<RouteStep>(DbContext),
            new Repository<LotMovementLog>(DbContext),
            new Repository<ToolQualification>(DbContext),
            new Repository<WorkInstruction>(DbContext),
            new Repository<LotChecklistProgress>(DbContext),
            new Repository<ProductionLine>(DbContext));

        var lotDto = await service.CreateLotAsync("LOT-002", "RECIPE-A", new List<string> { "W-01" }, CancellationToken.None);
        var steps = await service.GetRouteStepsAsync();
        var nextStep = steps.First(s => s.StepName == "LITHO");

        // Put on hold
        await service.HoldLotAsync(lotDto.Id, "SPC Failure", CancellationToken.None);

        // Act
        var success = await service.MoveLotToStepAsync(lotDto.Id, nextStep.Id, 42, CancellationToken.None);

        // Assert
        success.Should().BeFalse();
        var detail = await service.GetLotDetailAsync(lotDto.Id);
        detail!.Status.Should().Be("Hold");
        detail.ActiveRouteStep!.StepName.Should().Be("CLEAN"); // Unchanged
    }

    [Fact]
    public async Task release_lot_restores_active_status()
    {
        // Arrange
        var service = new WipService(
            new Repository<Lot>(DbContext),
            new Repository<Wafer>(DbContext),
            new Repository<RouteStep>(DbContext),
            new Repository<LotMovementLog>(DbContext),
            new Repository<ToolQualification>(DbContext),
            new Repository<WorkInstruction>(DbContext),
            new Repository<LotChecklistProgress>(DbContext),
            new Repository<ProductionLine>(DbContext));

        var lotDto = await service.CreateLotAsync("LOT-003", "RECIPE-A", new List<string> { "W-01" }, CancellationToken.None);
        
        await service.HoldLotAsync(lotDto.Id, "Metrology check needed", CancellationToken.None);
        var detailHeld = await service.GetLotDetailAsync(lotDto.Id);
        detailHeld!.Status.Should().Be("Hold");

        // Act
        var success = await service.ReleaseLotAsync(lotDto.Id, CancellationToken.None);

        // Assert
        success.Should().BeTrue();
        var detailReleased = await service.GetLotDetailAsync(lotDto.Id);
        detailReleased!.Status.Should().Be("Active");
    }
}
