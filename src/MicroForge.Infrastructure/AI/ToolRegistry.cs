using MicroForge.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Text;
using System.Text.Json.Nodes;

namespace MicroForge.Infrastructure.AI;

/// <summary>
/// Registry for MCP-style tools available to the AI agent.
/// </summary>
public class ToolRegistry : IToolRegistry
{
    private readonly ConcurrentDictionary<string, ITool> _tools = new();
    private readonly ConcurrentDictionary<string, List<ITool>> _byCategory = new();
    private readonly ILogger<ToolRegistry> _logger;

    public ToolRegistry(IEnumerable<ITool> tools, ILogger<ToolRegistry> logger)
    {
        _logger = logger;
        foreach (var tool in tools)
        {
            Register(tool);
        }
    }

    public void Register(ITool tool)
    {
        _tools[tool.Name] = tool;
        if (!_byCategory.ContainsKey(tool.Category))
            _byCategory[tool.Category] = new List<ITool>();
        _byCategory[tool.Category].Add(tool);
        _logger.LogInformation("Registered tool: {ToolName} (category: {Category})", tool.Name, tool.Category);
    }

    public ITool? GetTool(string name)
    {
        _tools.TryGetValue(name, out var tool);
        return tool;
    }

    public IReadOnlyList<ITool> GetAllTools() => _tools.Values.ToList().AsReadOnly();

    public IReadOnlyList<ITool> GetToolsByCategory(string category)
    {
        return _byCategory.TryGetValue(category, out var tools) ? tools.AsReadOnly() : Array.Empty<ITool>();
    }

    public string GetToolsAsSystemPrompt()
    {
        var sb = new StringBuilder();
        sb.AppendLine("You have access to the following tools:");
        sb.AppendLine();
        foreach (var tool in _tools.Values)
        {
            sb.AppendLine($"## {tool.Name}");
            sb.AppendLine($"Category: {tool.Category}");
            sb.AppendLine($"Description: {tool.Description}");
            sb.AppendLine($"Parameters: {tool.Schema.ToJsonString()}");
            sb.AppendLine();
        }
        sb.AppendLine("To call a tool, respond with a JSON object containing \"tool_name\" and \"arguments\".");
        sb.AppendLine("If you have enough information to answer, respond normally without a tool call.");
        return sb.ToString();
    }
}
