using FluentAssertions;
using MicroForge.Domain.Entities;
using MicroForge.Domain.Interfaces;
using MicroForge.Infrastructure.AI.Tools;
using System.Text.Json.Nodes;

namespace MicroForge.UnitTests;

/// <summary>
/// Unit tests for the CreateDefectRecordTool — defect creation with authorization.
/// </summary>
public class CreateDefectRecordToolTests : TestBase
{
    [Fact]
    public async Task invokeasync_with_valid_data_creates_defect()
    {
        await seedstandarddata();
        var run = DbContext.ProductionRuns.First();
        var tool = new CreateDefectRecordTool(DbContext);
        var args = new JsonObject
        {
            ["productionRunId"] = run.Id,
            ["defectType"] = "Scratch",
            ["severity"] = "Minor",
            ["description"] = "Surface scratch detected"
        };
        var context = new ToolExecutionContext { UserId = 1, Roles = new List<string> { "Admin" } };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeTrue();
        result.Output.Should().Contain("created");
        DbContext.DefectRecords.Should().HaveCount(1);
        var defect = DbContext.DefectRecords.First();
        defect.DefectType.Should().Be("Scratch");
        defect.Severity.Should().Be("Minor");
        defect.DetectedByUserId.Should().Be(1);
    }

    [Fact]
    public async Task invokeasync_with_nonexistent_runid_returns_failure()
    {
        await seedstandarddata();
        var tool = new CreateDefectRecordTool(DbContext);
        var args = new JsonObject
        {
            ["productionRunId"] = 9999,
            ["defectType"] = "Scratch",
            ["severity"] = "Minor",
            ["description"] = "Test"
        };
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
        var tool = new CreateDefectRecordTool(DbContext);
        var args = new JsonObject
        {
            ["productionRunId"] = run.Id,
            ["defectType"] = "Scratch",
            ["severity"] = "Minor",
            ["description"] = "Test"
        };
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
        var tool = new CreateDefectRecordTool(DbContext);
        var args = new JsonObject
        {
            ["productionRunId"] = run.Id,
            ["defectType"] = "Crack",
            ["severity"] = "Critical",
            ["description"] = "Major crack found"
        };
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
    public async Task invokeasync_viewer_role_denied()
    {
        await seedstandarddata();
        var run = DbContext.ProductionRuns.First();
        var tool = new CreateDefectRecordTool(DbContext);
        var args = new JsonObject
        {
            ["productionRunId"] = run.Id,
            ["defectType"] = "Scratch",
            ["severity"] = "Minor",
            ["description"] = "Test"
        };
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
    public async Task invokeasync_missing_required_fields_returns_failure()
    {
        await seedstandarddata();
        var tool = new CreateDefectRecordTool(DbContext);
        var args = new JsonObject
        {
            ["productionRunId"] = 1,
            ["defectType"] = "Scratch"
            // Missing severity and description
        };
        var context = new ToolExecutionContext { UserId = 1, Roles = new List<string> { "Admin" } };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeFalse();
    }

    [Fact]
    public void tool_name_is_create_defect_record()
    {
        var tool = new CreateDefectRecordTool(DbContext);
        tool.Name.Should().Be("create_defect_record");
    }

    [Fact]
    public void tool_category_is_quality()
    {
        var tool = new CreateDefectRecordTool(DbContext);
        tool.Category.Should().Be("quality");
    }
}