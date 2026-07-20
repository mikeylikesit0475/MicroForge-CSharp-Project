using FluentAssertions;
using MicroForge.Domain.Entities;
using MicroForge.Domain.Interfaces;
using MicroForge.Infrastructure.AI.Tools;
using MicroForge.Infrastructure.Data;
using System.Text.Json.Nodes;

namespace MicroForge.UnitTests;

/// <summary>
/// Unit tests for the QueryProductionDefectsTool — defect querying with data boundary enforcement.
/// </summary>
public class QueryProductionDefectsToolTests : TestBase
{
    [Fact]
    public async Task invokeasync_with_no_defects_returns_zero_count()
    {
        await seedstandarddata();
        var tool = new QueryProductionDefectsTool(DbContext);
        var args = new JsonObject { ["limit"] = 20 };
        var context = new ToolExecutionContext { UserId = 1, Roles = new List<string> { "Admin" } };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeTrue();
        result.Output.Should().Contain("\"count\":0");
    }

    [Fact]
    public async Task invokeasync_with_defects_returns_them()
    {
        await seedstandarddata();
        var run = DbContext.ProductionRuns.First();
        DbContext.DefectRecords.Add(new DefectRecord
        {
            ProductionRunId = run.Id,
            DefectType = "Scratch",
            Severity = "Minor",
            Description = "Surface scratch",
            DetectedAt = DateTime.UtcNow
        });
        await DbContext.SaveChangesAsync();

        var tool = new QueryProductionDefectsTool(DbContext);
        var args = new JsonObject { ["limit"] = 20 };
        var context = new ToolExecutionContext { UserId = 1, Roles = new List<string> { "Admin" } };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeTrue();
        result.Output.Should().Contain("\"count\":1");
        result.Output.Should().Contain("Scratch");
    }

    [Fact]
    public async Task invokeasync_filters_by_severity()
    {
        await seedstandarddata();
        var run = DbContext.ProductionRuns.First();
        DbContext.DefectRecords.Add(new DefectRecord
        {
            ProductionRunId = run.Id, DefectType = "Scratch", Severity = "Minor",
            Description = "Minor scratch", DetectedAt = DateTime.UtcNow
        });
        DbContext.DefectRecords.Add(new DefectRecord
        {
            ProductionRunId = run.Id, DefectType = "Crack", Severity = "Critical",
            Description = "Critical crack", DetectedAt = DateTime.UtcNow
        });
        await DbContext.SaveChangesAsync();

        var tool = new QueryProductionDefectsTool(DbContext);
        var args = new JsonObject { ["severity"] = "Critical", ["limit"] = 20 };
        var context = new ToolExecutionContext { UserId = 1, Roles = new List<string> { "Admin" } };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeTrue();
        result.Output.Should().Contain("\"count\":1");
        result.Output.Should().Contain("Crack");
        result.Output.Should().NotContain("Scratch");
    }

    [Fact]
    public async Task invokeasync_enforces_data_boundary_for_operator()
    {
        await seedstandarddata();
        var run = DbContext.ProductionRuns.First(r => r.BatchNumber == "BATCH-001");
        DbContext.DefectRecords.Add(new DefectRecord
        {
            ProductionRunId = run.Id, DefectType = "Scratch", Severity = "Minor",
            Description = "On line alpha", DetectedAt = DateTime.UtcNow
        });
        var run2 = DbContext.ProductionRuns.First(r => r.BatchNumber == "BATCH-002");
        DbContext.DefectRecords.Add(new DefectRecord
        {
            ProductionRunId = run2.Id, DefectType = "Crack", Severity = "Critical",
            Description = "On line beta", DetectedAt = DateTime.UtcNow
        });
        await DbContext.SaveChangesAsync();

        var tool = new QueryProductionDefectsTool(DbContext);
        var args = new JsonObject { ["limit"] = 20 };
        // Operator only has access to line 1 (Line Alpha)
        var context = new ToolExecutionContext
        {
            UserId = 2,
            Roles = new List<string> { "Operator" },
            ProductionLineIds = new List<int> { 1 }
        };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeTrue();
        result.Output.Should().Contain("\"count\":1");
        result.Output.Should().Contain("On line alpha");
        result.Output.Should().NotContain("On line beta");
    }

    [Fact]
    public async Task invokeasync_operator_denied_access_to_unauthorized_line()
    {
        await seedstandarddata();
        var tool = new QueryProductionDefectsTool(DbContext);
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
    public async Task invokeasync_admin_can_access_all_lines()
    {
        await seedstandarddata();
        var tool = new QueryProductionDefectsTool(DbContext);
        var args = new JsonObject { ["productionLineId"] = 2, ["limit"] = 20 };
        var context = new ToolExecutionContext
        {
            UserId = 1,
            Roles = new List<string> { "Admin" },
            ProductionLineIds = new List<int>()
        };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeTrue();
    }

    [Fact]
    public async Task invokeasync_limit_capped_at_100()
    {
        await seedstandarddata();
        var tool = new QueryProductionDefectsTool(DbContext);
        var args = new JsonObject { ["limit"] = 500 };
        var context = new ToolExecutionContext { UserId = 1, Roles = new List<string> { "Admin" } };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeTrue();
        // Should not throw, just cap at 100
    }

    [Fact]
    public void tool_name_is_query_production_defects()
    {
        var tool = new QueryProductionDefectsTool(DbContext);
        tool.Name.Should().Be("query_production_defects");
    }

    [Fact]
    public void tool_category_is_quality()
    {
        var tool = new QueryProductionDefectsTool(DbContext);
        tool.Category.Should().Be("quality");
    }
}