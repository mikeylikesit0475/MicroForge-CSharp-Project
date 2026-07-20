using FluentAssertions;
using MicroForge.Domain.Interfaces;
using MicroForge.Infrastructure.AI;
using Microsoft.Extensions.Logging.Abstractions;
using System.Text.Json.Nodes;

namespace MicroForge.UnitTests;

/// <summary>
/// Unit tests for the ToolRegistry — tool registration, lookup, and system prompt generation.
/// </summary>
public class ToolRegistryTests
{
    private readonly ToolRegistry _registry = new(Enumerable.Empty<ITool>(), NullLogger<ToolRegistry>.Instance);

    [Fact]
    public void getalltools_on_empty_registry_returns_empty_list()
    {
        var tools = _registry.GetAllTools();

        tools.Should().BeEmpty();
    }

    [Fact]
    public void register_then_gettool_returns_tool()
    {
        var tool = new TestTool("test_tool", "A test tool", "test");

        _registry.Register(tool);

        var result = _registry.GetTool("test_tool");
        result.Should().NotBeNull();
        result!.Name.Should().Be("test_tool");
    }

    [Fact]
    public void gettool_for_nonexistent_name_returns_null()
    {
        var result = _registry.GetTool("nonexistent");

        result.Should().BeNull();
    }

    [Fact]
    public void register_multiple_tools_all_retrievable()
    {
        _registry.Register(new TestTool("tool_a", "Tool A", "cat1"));
        _registry.Register(new TestTool("tool_b", "Tool B", "cat1"));
        _registry.Register(new TestTool("tool_c", "Tool C", "cat2"));

        _registry.GetAllTools().Should().HaveCount(3);
        _registry.GetTool("tool_a").Should().NotBeNull();
        _registry.GetTool("tool_b").Should().NotBeNull();
        _registry.GetTool("tool_c").Should().NotBeNull();
    }

    [Fact]
    public void gettoolsbycategory_returns_only_tools_in_that_category()
    {
        _registry.Register(new TestTool("tool_a", "Tool A", "sensors"));
        _registry.Register(new TestTool("tool_b", "Tool B", "sensors"));
        _registry.Register(new TestTool("tool_c", "Tool C", "production"));

        var sensortools = _registry.GetToolsByCategory("sensors");

        sensortools.Should().HaveCount(2);
        sensortools.All(t => t.Category == "sensors").Should().BeTrue();
    }

    [Fact]
    public void gettoolsbycategory_for_nonexistent_category_returns_empty()
    {
        _registry.Register(new TestTool("tool_a", "Tool A", "sensors"));

        var result = _registry.GetToolsByCategory("nonexistent");

        result.Should().BeEmpty();
    }

    [Fact]
    public void gettoolsassystemprompt_includes_tool_names_and_descriptions()
    {
        _registry.Register(new TestTool("query_sensor", "Query sensor data", "sensors"));
        _registry.Register(new TestTool("get_status", "Get production status", "production"));

        var prompt = _registry.GetToolsAsSystemPrompt();

        prompt.Should().Contain("query_sensor");
        prompt.Should().Contain("Query sensor data");
        prompt.Should().Contain("get_status");
        prompt.Should().Contain("Get production status");
        prompt.Should().Contain("You have access to the following tools");
    }

    [Fact]
    public void gettoolsassystemprompt_on_empty_registry_still_returns_header()
    {
        var prompt = _registry.GetToolsAsSystemPrompt();

        prompt.Should().Contain("You have access to the following tools");
    }

    [Fact]
    public void register_same_name_overwrites_previous_tool()
    {
        _registry.Register(new TestTool("my_tool", "First version", "cat"));
        _registry.Register(new TestTool("my_tool", "Second version", "cat"));

        var tool = _registry.GetTool("my_tool");
        tool!.Description.Should().Be("Second version");
        _registry.GetAllTools().Should().HaveCount(1);
    }

    [Fact]
    public void constructor_with_initial_tools_registers_all()
    {
        var initialtools = new List<ITool>
        {
            new TestTool("init_a", "Init A", "cat1"),
            new TestTool("init_b", "Init B", "cat2")
        };
        var registry = new ToolRegistry(initialtools, NullLogger<ToolRegistry>.Instance);

        registry.GetAllTools().Should().HaveCount(2);
    }

    /// <summary>
    /// Simple test tool implementation for registry testing.
    /// </summary>
    private class TestTool : ITool
    {
        public string Name { get; }
        public string Description { get; }
        public string Category { get; }
        public JsonObject Schema => new() { ["type"] = "object" };

        public TestTool(string name, string description, string category)
        {
            Name = name;
            Description = description;
            Category = category;
        }

        public Task<ToolResult> InvokeAsync(JsonObject? arguments, ToolExecutionContext context, CancellationToken ct = default)
        {
            return Task.FromResult(ToolResult.Ok("test output"));
        }
    }
}