using FluentAssertions;
using MicroForge.Domain.Entities;
using MicroForge.Domain.Interfaces;
using MicroForge.Infrastructure.AI.Tools;
using System.Text.Json.Nodes;

namespace MicroForge.UnitTests;

/// <summary>
/// Unit tests for the GetProductionRunSummaryTool — run summary with yield and defect counts.
/// </summary>
public class GetProductionRunSummaryToolTests : TestBase
{
    [Fact]
    public async Task invokeasync_with_valid_runid_returns_summary()
    {
        await seedstandarddata();
        var run = DbContext.ProductionRuns.First(r => r.BatchNumber == "BATCH-001");
        var tool = new GetProductionRunSummaryTool(DbContext);
        var args = new JsonObject { ["runId"] = run.Id };
        var context = new ToolExecutionContext { UserId = 1, Roles = new List<string> { "Admin" } };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeTrue();
        result.Output.Should().Contain("BATCH-001");
        result.Output.Should().Contain("Running");
        result.Output.Should().Contain("95.5");
    }

    [Fact]
    public async Task invokeasync_with_nonexistent_runid_returns_failure()
    {
        await seedstandarddata();
        var tool = new GetProductionRunSummaryTool(DbContext);
        var args = new JsonObject { ["runId"] = 9999 };
        var context = new ToolExecutionContext { UserId = 1, Roles = new List<string> { "Admin" } };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeFalse();
        result.Error.Should().Contain("not found");
    }

    [Fact]
    public async Task invokeasync_includes_defect_count()
    {
        await seedstandarddata();
        var run = DbContext.ProductionRuns.First();
        DbContext.DefectRecords.Add(new DefectRecord
        {
            ProductionRunId = run.Id, DefectType = "Scratch", Severity = "Minor",
            Description = "Test defect", DetectedAt = DateTime.UtcNow
        });
        await DbContext.SaveChangesAsync();

        var tool = new GetProductionRunSummaryTool(DbContext);
        var args = new JsonObject { ["runId"] = run.Id };
        var context = new ToolExecutionContext { UserId = 1, Roles = new List<string> { "Admin" } };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeTrue();
        result.Output.Should().Contain("defectCount");
        result.Output.Should().Contain("1");
    }

    [Fact]
    public async Task invokeasync_operator_denied_unauthorized_line()
    {
        await seedstandarddata();
        var run = DbContext.ProductionRuns.First(r => r.BatchNumber == "BATCH-002");
        var tool = new GetProductionRunSummaryTool(DbContext);
        var args = new JsonObject { ["runId"] = run.Id };
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
    public async Task invokeasync_operator_allowed_for_authorized_line()
    {
        await seedstandarddata();
        var run = DbContext.ProductionRuns.First(r => r.BatchNumber == "BATCH-001");
        var tool = new GetProductionRunSummaryTool(DbContext);
        var args = new JsonObject { ["runId"] = run.Id };
        var context = new ToolExecutionContext
        {
            UserId = 2,
            Roles = new List<string> { "Operator" },
            ProductionLineIds = new List<int> { 1 }
        };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeTrue();
    }

    [Fact]
    public async Task invokeasync_completed_run_shows_actual_yield()
    {
        await seedstandarddata();
        var run = DbContext.ProductionRuns.First(r => r.BatchNumber == "BATCH-002");
        var tool = new GetProductionRunSummaryTool(DbContext);
        var args = new JsonObject { ["runId"] = run.Id };
        var context = new ToolExecutionContext { UserId = 1, Roles = new List<string> { "Admin" } };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeTrue();
        result.Output.Should().Contain("91.3");
        result.Output.Should().Contain("Completed");
    }

    [Fact]
    public void tool_name_is_get_production_run_summary()
    {
        var tool = new GetProductionRunSummaryTool(DbContext);
        tool.Name.Should().Be("get_production_run_summary");
    }

    [Fact]
    public void tool_category_is_production()
    {
        var tool = new GetProductionRunSummaryTool(DbContext);
        tool.Category.Should().Be("production");
    }
}