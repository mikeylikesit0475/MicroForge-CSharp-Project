using FluentAssertions;
using MicroForge.Application.Services;
using MicroForge.Domain.Entities;
using MicroForge.Domain.Interfaces;
using MicroForge.Infrastructure.AI.Tools;
using MicroForge.Infrastructure.Data;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MicroForge.UnitTests;

public class FabManagementToolsTests : TestBase
{
    [Fact]
    public async Task ewi_checklist_tools_fetch_and_complete_instructions_to_unblock_lot()
    {
        // Arrange
        var db = DbContext;
        var stepRepo = new Repository<RouteStep>(db);
        var lotRepo = new Repository<Lot>(db);
        var waferRepo = new Repository<Wafer>(db);
        var logRepo = new Repository<LotMovementLog>(db);
        var qualRepo = new Repository<ToolQualification>(db);
        var wiRepo = new Repository<WorkInstruction>(db);
        var progressRepo = new Repository<LotChecklistProgress>(db);
        var lineRepo = new Repository<ProductionLine>(db);

        var step = new RouteStep { StepName = "ETCH_1", SequenceOrder = 1, Description = "Plasma etch" };
        await stepRepo.AddAsync(step, CancellationToken.None);
        await stepRepo.SaveChangesAsync(CancellationToken.None);

        var lot = new Lot { LotNumber = "LOT-EWI-101", RecipeName = "DRAM-16Gb", Status = LotStatus.Active, ActiveRouteStepId = step.Id };
        await lotRepo.AddAsync(lot, CancellationToken.None);
        await lotRepo.SaveChangesAsync(CancellationToken.None);

        var wipService = new WipService(lotRepo, waferRepo, stepRepo, logRepo, qualRepo, wiRepo, progressRepo, lineRepo);

        // Get default work instructions
        var instructions = await wipService.GetWorkInstructionsAsync(step.Id, CancellationToken.None);
        instructions.Should().NotBeEmpty();
        var mandatoryInst = instructions.Find(i => i.IsMandatory);
        mandatoryInst.Should().NotBeNull();

        var getTool = new GetEwiChecklistTool(wipService);
        var completeTool = new CompleteEwiChecklistTool(wipService);
        var execContext = new ToolExecutionContext { Username = "operator_1", UserId = 1 };

        // Act 1: Fetch EWI checklist
        var getResult = await getTool.InvokeAsync(new JsonObject { ["lotId"] = lot.Id }, execContext, CancellationToken.None);

        // Assert 1
        getResult.Success.Should().BeTrue();
        getResult.Output.Should().Contain("LOT-EWI-101");
        getResult.Output.Should().Contain("checklist");

        // Act 2: Complete mandatory instruction
        var completeResult = await completeTool.InvokeAsync(new JsonObject
        {
            ["lotId"] = lot.Id,
            ["instructionId"] = mandatoryInst!.Id,
            ["completed"] = true
        }, execContext, CancellationToken.None);

        // Assert 2
        completeResult.Success.Should().BeTrue();
        completeResult.Output.Should().Contain("isCompleted\":true");
    }

    [Fact]
    public async Task manage_tool_maintenance_tool_lists_and_schedules_maintenance()
    {
        // Arrange
        var db = DbContext;
        var lineRepo = new Repository<ProductionLine>(db);
        var runRepo = new Repository<ProductionRun>(db);
        var readingRepo = new Repository<SensorReading>(db);
        var defectRepo = new Repository<DefectRecord>(db);
        var capaRepo = new Repository<CapaAction>(db);
        var ecoRepo = new Repository<EcoRecord>(db);
        var shiftRepo = new Repository<ShiftLog>(db);
        var maintRepo = new Repository<ToolMaintenanceLog>(db);
        var recipeRepo = new Repository<RecipeVersion>(db);

        var line = new ProductionLine { Name = "Line-PM-1", Location = "Fab-1", LineType = "Etch", IsActive = true };
        await lineRepo.AddAsync(line, CancellationToken.None);
        await lineRepo.SaveChangesAsync(CancellationToken.None);

        var fabService = new FabExpansionService(lineRepo, runRepo, readingRepo, defectRepo, capaRepo, ecoRepo, shiftRepo, maintRepo, recipeRepo);
        var tool = new ManageToolMaintenanceTool(fabService);
        var execContext = new ToolExecutionContext { Username = "maintenance_eng" };

        // Act 1: Schedule PM
        var scheduleResult = await tool.InvokeAsync(new JsonObject
        {
            ["action"] = "schedule",
            ["productionLineId"] = line.Id,
            ["status"] = "Scheduled PM",
            ["runtimeHours"] = 1500.5
        }, execContext, CancellationToken.None);

        // Assert 1
        scheduleResult.Success.Should().BeTrue();
        scheduleResult.Output.Should().Contain("Maintenance record created");

        // Act 2: List PM History
        var listResult = await tool.InvokeAsync(new JsonObject
        {
            ["action"] = "list",
            ["productionLineId"] = line.Id
        }, execContext, CancellationToken.None);

        // Assert 2
        listResult.Success.Should().BeTrue();
        listResult.Output.Should().Contain("Scheduled PM");
    }

    [Fact]
    public async Task calculate_spc_metrics_tool_computes_control_limits_and_cpk()
    {
        // Arrange
        var db = DbContext;
        var tool = new CalculateSpcMetricsTool(db);
        var execContext = new ToolExecutionContext { Username = "quality_eng" };

        // Act: Evaluate sample with drift
        var result = await tool.InvokeAsync(new JsonObject
        {
            ["currentValue"] = 412.5,
            ["history"] = new JsonArray { 398.2, 400.1, 399.5, 401.0, 399.8, 400.5, 402.1, 399.2, 400.8 },
            ["usl"] = 410.0,
            ["lsl"] = 390.0
        }, execContext, CancellationToken.None);

        // Assert
        result.Success.Should().BeTrue();
        result.Output.Should().Contain("cpk");
        result.Output.Should().Contain("ppk");
        result.Output.Should().Contain("ucl");
        result.Output.Should().Contain("lcl");
        result.Output.Should().Contain("isViolation\":true");
    }
}
