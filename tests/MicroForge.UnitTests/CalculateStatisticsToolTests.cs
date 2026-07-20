using FluentAssertions;
using MicroForge.Domain.Interfaces;
using MicroForge.Infrastructure.AI;
using MicroForge.Infrastructure.AI.Tools;
using MicroForge.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Nodes;

namespace MicroForge.UnitTests;

/// <summary>
/// Unit tests for the CalculateStatisticsTool — mean, median, std, min, max computation.
/// </summary>
public class CalculateStatisticsToolTests : TestBase
{
    [Fact]
    public async Task invokeasync_with_valid_data_returns_statistics()
    {
        var tool = new CalculateStatisticsTool();
        var args = new JsonObject { ["data"] = new JsonArray(1.0, 2.0, 3.0, 4.0, 5.0) };
        var context = new ToolExecutionContext { UserId = 1, Roles = new List<string> { "Admin" } };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeTrue();
        result.Output.Should().Contain("mean");
        result.Output.Should().Contain("median");
        result.Output.Should().Contain("stdDev");
        result.Output.Should().Contain("3"); // mean of 1-5
    }

    [Fact]
    public async Task invokeasync_with_empty_data_returns_failure()
    {
        var tool = new CalculateStatisticsTool();
        var args = new JsonObject { ["data"] = new JsonArray() };
        var context = new ToolExecutionContext { UserId = 1 };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeFalse();
        result.Error.Should().Contain("empty");
    }

    [Fact]
    public async Task invokeasync_with_missing_data_returns_failure()
    {
        var tool = new CalculateStatisticsTool();
        var args = new JsonObject();
        var context = new ToolExecutionContext { UserId = 1 };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeFalse();
    }

    [Fact]
    public async Task invokeasync_with_single_value_returns_valid_stats()
    {
        var tool = new CalculateStatisticsTool();
        var args = new JsonObject { ["data"] = new JsonArray(42.0) };
        var context = new ToolExecutionContext { UserId = 1 };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeTrue();
        result.Output.Should().Contain("42");
    }

    [Fact]
    public async Task invokeasync_with_even_count_computes_median_as_average_of_middle_two()
    {
        var tool = new CalculateStatisticsTool();
        var args = new JsonObject { ["data"] = new JsonArray(1.0, 2.0, 3.0, 4.0) };
        var context = new ToolExecutionContext { UserId = 1 };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeTrue();
        // Median of [1,2,3,4] = (2+3)/2 = 2.5
        result.Output.Should().Contain("2.5");
    }

    [Fact]
    public async Task invokeasync_with_odd_count_computes_median_as_middle_value()
    {
        var tool = new CalculateStatisticsTool();
        var args = new JsonObject { ["data"] = new JsonArray(1.0, 2.0, 3.0, 4.0, 5.0) };
        var context = new ToolExecutionContext { UserId = 1 };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeTrue();
        // Median of [1,2,3,4,5] = 3
        result.Output.Should().Contain("3");
    }

    [Fact]
    public async Task invokeasync_with_negative_values_computes_correct_stats()
    {
        var tool = new CalculateStatisticsTool();
        var args = new JsonObject { ["data"] = new JsonArray(-5.0, -3.0, -1.0, 1.0, 3.0) };
        var context = new ToolExecutionContext { UserId = 1 };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeTrue();
        // Mean = 0/5 = -0.2... actually (-5-3-1+1+3)/5 = -5/5 = -1
        result.Output.Should().Contain("-1");
    }

    [Fact]
    public void tool_name_is_calculate_statistics()
    {
        var tool = new CalculateStatisticsTool();
        tool.Name.Should().Be("calculate_statistics");
    }

    [Fact]
    public void tool_category_is_analytics()
    {
        var tool = new CalculateStatisticsTool();
        tool.Category.Should().Be("analytics");
    }

    [Fact]
    public void tool_schema_has_required_data_field()
    {
        var tool = new CalculateStatisticsTool();
        tool.Schema.Should().NotBeNull();
        var required = tool.Schema["required"]?.AsArray();
        required.Should().NotBeNull();
        required!.Select(r => r?.GetValue<string>()).Should().Contain("data");
    }
}