using FluentAssertions;
using MicroForge.Domain.Entities;
using MicroForge.Domain.Interfaces;
using MicroForge.Infrastructure.AI.Tools;
using System.Text.Json.Nodes;

namespace MicroForge.UnitTests;

/// <summary>
/// Unit tests for the QueryProductionRunsTool — production run querying with filters.
/// </summary>
public class QueryProductionRunsToolTests : TestBase
{
    [Fact]
    public async Task invokeasync_returns_runs_for_admin()
    {
        await seedstandarddata();
        var tool = new QueryProductionRunsTool(DbContext);
        var args = new JsonObject { ["limit"] = 20 };
        var context = new ToolExecutionContext { UserId = 1, Roles = new List<string> { "Admin" } };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeTrue();
        result.Output.Should().Contain("\"count\":2");
        result.Output.Should().Contain("BATCH-001");
        result.Output.Should().Contain("BATCH-002");
    }

    [Fact]
    public async Task invokeasync_filters_by_status()
    {
        await seedstandarddata();
        var tool = new QueryProductionRunsTool(DbContext);
        var args = new JsonObject { ["status"] = "Running", ["limit"] = 20 };
        var context = new ToolExecutionContext { UserId = 1, Roles = new List<string> { "Admin" } };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeTrue();
        result.Output.Should().Contain("\"count\":1");
        result.Output.Should().Contain("BATCH-001");
        result.Output.Should().NotContain("BATCH-002");
    }

    [Fact]
    public async Task invokeasync_filters_by_production_line()
    {
        await seedstandarddata();
        var tool = new QueryProductionRunsTool(DbContext);
        var args = new JsonObject { ["productionLineId"] = 2, ["limit"] = 20 };
        var context = new ToolExecutionContext { UserId = 1, Roles = new List<string> { "Admin" } };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeTrue();
        result.Output.Should().Contain("\"count\":1");
        result.Output.Should().Contain("BATCH-002");
        result.Output.Should().NotContain("BATCH-001");
    }

    [Fact]
    public async Task invokeasync_enforces_data_boundary_for_operator()
    {
        await seedstandarddata();
        var tool = new QueryProductionRunsTool(DbContext);
        var args = new JsonObject { ["limit"] = 20 };
        var context = new ToolExecutionContext
        {
            UserId = 2,
            Roles = new List<string> { "Operator" },
            ProductionLineIds = new List<int> { 1 }
        };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeTrue();
        result.Output.Should().Contain("\"count\":1");
        result.Output.Should().Contain("BATCH-001");
        result.Output.Should().NotContain("BATCH-002");
    }

    [Fact]
    public async Task invokeasync_operator_denied_unauthorized_line()
    {
        await seedstandarddata();
        var tool = new QueryProductionRunsTool(DbContext);
        var args = new JsonObject { ["productionLineId"] = 2, ["limit"] = 20 };
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
    public async Task invokeasync_with_no_runs_returns_zero_count()
    {
        await seedstandarddata();
        var tool = new QueryProductionRunsTool(DbContext);
        var args = new JsonObject { ["status"] = "Cancelled", ["limit"] = 20 };
        var context = new ToolExecutionContext { UserId = 1, Roles = new List<string> { "Admin" } };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeTrue();
        result.Output.Should().Contain("\"count\":0");
    }

    [Fact]
    public void tool_name_is_query_production_runs()
    {
        var tool = new QueryProductionRunsTool(DbContext);
        tool.Name.Should().Be("query_production_runs");
    }

    [Fact]
    public void tool_category_is_production()
    {
        var tool = new QueryProductionRunsTool(DbContext);
        tool.Category.Should().Be("production");
    }
}