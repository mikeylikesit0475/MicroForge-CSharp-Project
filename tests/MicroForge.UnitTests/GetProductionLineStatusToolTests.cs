using FluentAssertions;
using MicroForge.Domain.Entities;
using MicroForge.Domain.Interfaces;
using MicroForge.Infrastructure.AI.Tools;
using System.Text.Json.Nodes;

namespace MicroForge.UnitTests;

/// <summary>
/// Unit tests for the GetProductionLineStatusTool — real-time line status with sensor summaries.
/// </summary>
public class GetProductionLineStatusToolTests : TestBase
{
    [Fact]
    public async Task invokeasync_returns_line_status_for_admin()
    {
        await seedstandarddata();
        var tool = new GetProductionLineStatusTool(DbContext);
        var args = new JsonObject();
        var context = new ToolExecutionContext { UserId = 1, Roles = new List<string> { "Admin" } };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeTrue();
        result.Output.Should().Contain("Line Alpha");
        result.Output.Should().Contain("Line Beta");
    }

    [Fact]
    public async Task invokeasync_filters_by_data_boundary_for_operator()
    {
        await seedstandarddata();
        var tool = new GetProductionLineStatusTool(DbContext);
        var args = new JsonObject();
        var context = new ToolExecutionContext
        {
            UserId = 2,
            Roles = new List<string> { "Operator" },
            ProductionLineIds = new List<int> { 1 }
        };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeTrue();
        result.Output.Should().Contain("Line Alpha");
        result.Output.Should().NotContain("Line Beta");
    }

    [Fact]
    public async Task invokeasync_with_specific_lineid_returns_single_line()
    {
        await seedstandarddata();
        var tool = new GetProductionLineStatusTool(DbContext);
        var args = new JsonObject { ["productionLineId"] = 1 };
        var context = new ToolExecutionContext { UserId = 1, Roles = new List<string> { "Admin" } };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeTrue();
        result.Output.Should().Contain("Line Alpha");
        result.Output.Should().NotContain("Line Beta");
    }

    [Fact]
    public async Task invokeasync_operator_denied_unauthorized_line()
    {
        await seedstandarddata();
        var tool = new GetProductionLineStatusTool(DbContext);
        var args = new JsonObject { ["productionLineId"] = 2 };
        var context = new ToolExecutionContext
        {
            UserId = 2,
            Roles = new List<string> { "Operator" },
            ProductionLineIds = new List<int> { 1 }
        };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeFalse();
        result.Error.Should().Contain("Access denied");
    }

    [Fact]
    public async Task invokeasync_includes_active_run_info()
    {
        await seedstandarddata();
        var tool = new GetProductionLineStatusTool(DbContext);
        var args = new JsonObject { ["productionLineId"] = 1 };
        var context = new ToolExecutionContext { UserId = 1, Roles = new List<string> { "Admin" } };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeTrue();
        result.Output.Should().Contain("BATCH-001");
        result.Output.Should().Contain("Running");
    }

    [Fact]
    public async Task invokeasync_includes_recent_sensor_summary()
    {
        await seedstandarddata();
        DbContext.SensorReadings.Add(new SensorReading
        {
            ProductionLineId = 1, SensorType = "Temperature",
            Value = 25.5m, Unit = "°C", Timestamp = DateTime.UtcNow
        });
        await DbContext.SaveChangesAsync();

        var tool = new GetProductionLineStatusTool(DbContext);
        var args = new JsonObject { ["productionLineId"] = 1 };
        var context = new ToolExecutionContext { UserId = 1, Roles = new List<string> { "Admin" } };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeTrue();
        result.Output.Should().Contain("Temperature");
    }

    [Fact]
    public void tool_name_is_get_production_line_status()
    {
        var tool = new GetProductionLineStatusTool(DbContext);
        tool.Name.Should().Be("get_production_line_status");
    }

    [Fact]
    public void tool_category_is_production()
    {
        var tool = new GetProductionLineStatusTool(DbContext);
        tool.Category.Should().Be("production");
    }
}