using FluentAssertions;
using MicroForge.Domain.Entities;
using MicroForge.Domain.Interfaces;
using MicroForge.Infrastructure.AI.Tools;
using System.Text.Json.Nodes;

namespace MicroForge.UnitTests;

/// <summary>
/// Unit tests for the UpdateProductionRunStatusTool — run status transitions with authorization.
/// </summary>
public class UpdateProductionRunStatusToolTests : TestBase
{
    [Fact]
    public async Task invokeasync_updates_status_successfully()
    {
        await seedstandarddata();
        var run = DbContext.ProductionRuns.First(r => r.BatchNumber == "BATCH-001");
        var tool = new UpdateProductionRunStatusTool(DbContext);
        var args = new JsonObject { ["runId"] = run.Id, ["status"] = "Paused" };
        var context = new ToolExecutionContext { UserId = 1, Roles = new List<string> { "Admin" } };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeTrue();
        result.Output.Should().Contain("updated");
        var updated = DbContext.ProductionRuns.Find(run.Id);
        updated!.Status.Should().Be("Paused");
    }

    [Fact]
    public async Task invokeasync_completing_run_sets_endtime()
    {
        await seedstandarddata();
        var run = DbContext.ProductionRuns.First(r => r.BatchNumber == "BATCH-001");
        var tool = new UpdateProductionRunStatusTool(DbContext);
        var args = new JsonObject { ["runId"] = run.Id, ["status"] = "Completed", ["actualYield"] = 93.2 };
        var context = new ToolExecutionContext { UserId = 1, Roles = new List<string> { "Admin" } };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeTrue();
        var updated = DbContext.ProductionRuns.Find(run.Id);
        updated!.Status.Should().Be("Completed");
        updated.EndTime.Should().NotBeNull();
        updated.ActualYield.Should().Be(93.2m);
    }

    [Fact]
    public async Task invokeasync_with_nonexistent_runid_returns_failure()
    {
        await seedstandarddata();
        var tool = new UpdateProductionRunStatusTool(DbContext);
        var args = new JsonObject { ["runId"] = 9999, ["status"] = "Paused" };
        var context = new ToolExecutionContext { UserId = 1, Roles = new List<string> { "Admin" } };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeFalse();
        result.Error.Should().Contain("not found");
    }

    [Fact]
    public async Task invokeasync_operator_denied_for_unauthorized_line()
    {
        await seedstandarddata();
        var run = DbContext.ProductionRuns.First(r => r.BatchNumber == "BATCH-002");
        var tool = new UpdateProductionRunStatusTool(DbContext);
        var args = new JsonObject { ["runId"] = run.Id, ["status"] = "Paused" };
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
    public async Task invokeasync_viewer_denied()
    {
        await seedstandarddata();
        var run = DbContext.ProductionRuns.First();
        var tool = new UpdateProductionRunStatusTool(DbContext);
        var args = new JsonObject { ["runId"] = run.Id, ["status"] = "Paused" };
        var context = new ToolExecutionContext
        {
            UserId = 1,
            Roles = new List<string> { "Viewer" },
            ProductionLineIds = new List<int>()
        };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeFalse();
        result.Error.Should().Contain("Access denied").And.Contain("write");
    }

    [Fact]
    public async Task invokeasync_engineer_allowed_for_authorized_line()
    {
        await seedstandarddata();
        var run = DbContext.ProductionRuns.First(r => r.BatchNumber == "BATCH-001");
        var tool = new UpdateProductionRunStatusTool(DbContext);
        var args = new JsonObject { ["runId"] = run.Id, ["status"] = "Paused" };
        var context = new ToolExecutionContext
        {
            UserId = 1,
            Roles = new List<string> { "Engineer" },
            ProductionLineIds = new List<int> { 1 }
        };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeTrue();
    }

    [Fact]
    public void tool_name_is_update_production_run_status()
    {
        var tool = new UpdateProductionRunStatusTool(DbContext);
        tool.Name.Should().Be("update_production_run_status");
    }

    [Fact]
    public void tool_category_is_production()
    {
        var tool = new UpdateProductionRunStatusTool(DbContext);
        tool.Category.Should().Be("production");
    }
}