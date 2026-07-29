using FluentAssertions;
using MicroForge.Application.Services;
using MicroForge.Domain.Entities;
using MicroForge.Infrastructure.AI.Tools;
using MicroForge.Infrastructure.Data;
using MicroForge.Infrastructure.Events;
using System.Linq;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MicroForge.UnitTests;

public class ShowcaseFeaturesTests : TestBase
{
    [Fact]
    public async Task self_healing_service_quarantines_lot_creates_capa_and_eco_and_reroutes()
    {
        // Arrange
        var db = DbContext;
        var lotRepo = new Repository<Lot>(db);
        var stepRepo = new Repository<RouteStep>(db);
        var moveRepo = new Repository<LotMovementLog>(db);
        var capaRepo = new Repository<CapaAction>(db);
        var ecoRepo = new Repository<EcoRecord>(db);

        // Seed step and lot
        var step = new RouteStep { StepName = "WAFER_PREP", SequenceOrder = 1 };
        await stepRepo.AddAsync(step, CancellationToken.None);
        await stepRepo.SaveChangesAsync(CancellationToken.None);

        var lot = new Lot { LotNumber = "LOT-TEST-999", RecipeName = "DRAM-16Gb", Status = LotStatus.Active, ActiveRouteStepId = step.Id };
        await lotRepo.AddAsync(lot, CancellationToken.None);
        await lotRepo.SaveChangesAsync(CancellationToken.None);

        var service = new FabSelfHealingService(lotRepo, stepRepo, moveRepo, capaRepo, ecoRepo);

        // Act
        var result = await service.TriggerSelfHealingAsync(lot.Id, "Thermal Runaway (+4.2σ Drift)", CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result!.NewStatus.Should().Be("Hold");
        result.CapaId.Should().BeGreaterThan(0);
        result.EcoId.Should().BeGreaterThan(0);
        result.AssignedReworkStep.Should().Be("WAFER_PREP");

        var updatedLot = await lotRepo.GetByIdAsync(lot.Id);
        updatedLot!.Status.Should().Be(LotStatus.Hold);
    }

    [Fact]
    public async Task wafer_die_map_service_generates_300mm_die_grid_with_defects()
    {
        // Arrange
        var service = new WaferDieMapService();

        // Act
        var result = await service.GenerateWaferDieMapAsync(lotId: 1, waferIdPattern: "LOT-001-W13", CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.TotalDies.Should().BeGreaterThan(300); // 300mm wafer has > 300 dies
        result.Dies.Should().NotBeEmpty();
        result.YieldPercentage.Should().BeInRange(0m, 100m);
        result.Dies.Any(d => d.DefectClass != "None").Should().BeTrue();
    }

    [Fact]
    public void secs_gem_simulator_handles_remote_commands()
    {
        // Arrange & Act
        SecsGemSimulator.SendRemoteCommand("REMOTE_START", "DRAM-16Gb-v3");

        // Assert
        var logs = SecsGemSimulator.TrafficLogs.ToList();
        logs.Any(l => l.MsgType == "S2F21" && l.Description.Contains("REMOTE_START")).Should().BeTrue();
        logs.Any(l => l.MsgType == "S2F22" && l.Description.Contains("Equipment ACK")).Should().BeTrue();
    }

    [Fact]
    public async Task trigger_self_healing_tool_invokes_service()
    {
        // Arrange
        var db = DbContext;
        var lotRepo = new Repository<Lot>(db);
        var stepRepo = new Repository<RouteStep>(db);
        var moveRepo = new Repository<LotMovementLog>(db);
        var capaRepo = new Repository<CapaAction>(db);
        var ecoRepo = new Repository<EcoRecord>(db);

        var step = new RouteStep { StepName = "WAFER_PREP", SequenceOrder = 1 };
        await stepRepo.AddAsync(step, CancellationToken.None);
        await stepRepo.SaveChangesAsync(CancellationToken.None);

        var lot = new Lot { LotNumber = "LOT-TOOL-777", RecipeName = "NAND-256Gb", Status = LotStatus.Active, ActiveRouteStepId = step.Id };
        await lotRepo.AddAsync(lot, CancellationToken.None);
        await lotRepo.SaveChangesAsync(CancellationToken.None);

        var service = new FabSelfHealingService(lotRepo, stepRepo, moveRepo, capaRepo, ecoRepo);
        var tool = new TriggerSelfHealingTool(service);

        // Act
        var result = await tool.InvokeAsync(new JsonObject { ["lotId"] = lot.Id, ["anomalyDescription"] = "Vacuum Pressure Leak" }, new MicroForge.Domain.Interfaces.ToolExecutionContext { Username = "engineer" }, CancellationToken.None);

        // Assert
        result.Success.Should().BeTrue();
        result.Output.Should().Contain("LOT-TOOL-777");
        result.Output.Should().Contain("Vacuum Pressure Leak");
    }
}
