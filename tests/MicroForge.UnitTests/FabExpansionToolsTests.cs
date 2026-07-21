using FluentAssertions;
using MicroForge.Application.Services;
using MicroForge.Domain.Entities;
using MicroForge.Domain.Interfaces;
using MicroForge.Infrastructure.AI.Tools;
using MicroForge.Infrastructure.Data;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MicroForge.UnitTests;

public class FabExpansionToolsTests : TestBase
{
    [Fact]
    public async Task query_wip_lots_tool_returns_ok_result()
    {
        // Arrange
        var db = DbContext;
        var wipService = new WipService(
            new Repository<Lot>(db),
            new Repository<Wafer>(db),
            new Repository<RouteStep>(db),
            new Repository<LotMovementLog>(db),
            new Repository<ToolQualification>(db),
            new Repository<WorkInstruction>(db),
            new Repository<LotChecklistProgress>(db),
            new Repository<ProductionLine>(db)
        );
        var tool = new QueryWipLotsTool(wipService);

        // Act
        var result = await tool.InvokeAsync(null, new ToolExecutionContext { Username = "operator" }, CancellationToken.None);

        // Assert
        result.Success.Should().BeTrue();
        result.Output.Should().Contain("[]"); // Default empty list
    }

    [Fact]
    public async Task get_oee_metrics_tool_returns_ok_result()
    {
        // Arrange
        var db = DbContext;
        var fabService = new FabExpansionService(
            new Repository<ProductionLine>(db),
            new Repository<ProductionRun>(db),
            new Repository<SensorReading>(db),
            new Repository<DefectRecord>(db),
            new Repository<CapaAction>(db),
            new Repository<EcoRecord>(db),
            new Repository<ShiftLog>(db),
            new Repository<ToolMaintenanceLog>(db),
            new Repository<RecipeVersion>(db)
        );
        var tool = new GetOeeMetricsTool(fabService);

        // Act
        var result = await tool.InvokeAsync(null, new ToolExecutionContext { Username = "operator" }, CancellationToken.None);

        // Assert
        result.Success.Should().BeTrue();
        result.Output.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task query_fdc_diagnostics_tool_returns_ok_result()
    {
        // Arrange
        var db = DbContext;
        var fdcService = new FdcService(
            new Repository<ProductionLine>(db),
            new Repository<SensorReading>(db),
            new Repository<ProductionRun>(db)
        );
        var tool = new QueryFdcDiagnosticsTool(fdcService);

        // Act
        var result = await tool.InvokeAsync(null, new ToolExecutionContext { Username = "operator" }, CancellationToken.None);

        // Assert
        result.Success.Should().BeTrue();
        result.Output.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task manage_qualifications_tool_lists_qualifications()
    {
        // Arrange
        var db = DbContext;
        var wipService = new WipService(
            new Repository<Lot>(db),
            new Repository<Wafer>(db),
            new Repository<RouteStep>(db),
            new Repository<LotMovementLog>(db),
            new Repository<ToolQualification>(db),
            new Repository<WorkInstruction>(db),
            new Repository<LotChecklistProgress>(db),
            new Repository<ProductionLine>(db)
        );
        var tool = new ManageQualificationsTool(wipService);

        // Act
        var result = await tool.InvokeAsync(new JsonObject { ["action"] = "list" }, new ToolExecutionContext { Username = "operator" }, CancellationToken.None);

        // Assert
        result.Success.Should().BeTrue();
        result.Output.Should().NotBeNullOrEmpty();
    }
}
