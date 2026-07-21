using System.Text.Json;
using System.Text.Json.Nodes;

namespace MicroForge.Domain.Interfaces;

/// <summary>
/// Contract for an MCP-style tool that the AI agent can invoke.
/// </summary>
public interface ITool
{
    string Name { get; }
    string Description { get; }
    string Category { get; }
    JsonObject Schema { get; }
    Task<ToolResult> InvokeAsync(JsonObject? arguments, ToolExecutionContext context, CancellationToken ct = default);
}

/// <summary>
/// Result of a tool invocation.
/// </summary>
public class ToolResult
{
    public bool Success { get; init; }
    public string Output { get; init; } = string.Empty;
    public string? Error { get; init; }
    public Dictionary<string, object> Metadata { get; init; } = new();

    public static ToolResult Ok(string output, Dictionary<string, object>? metadata = null) => new()
    {
        Success = true,
        Output = output,
        Metadata = metadata ?? new()
    };

    public static ToolResult Fail(string error) => new()
    {
        Success = false,
        Error = error
    };
}

/// <summary>
/// Context passed to every tool invocation — provides user identity and session info.
/// </summary>
public class ToolExecutionContext
{
    public int UserId { get; init; }
    public string Username { get; init; } = string.Empty;
    public string SessionId { get; init; } = string.Empty;
    public List<string> Roles { get; init; } = new();
    public List<int> ProductionLineIds { get; init; } = new();
    public CancellationToken CancellationToken { get; init; }
}

/// <summary>
/// Registry for discovering and managing available tools.
/// </summary>
public interface IToolRegistry
{
    void Register(ITool tool);
    ITool? GetTool(string name);
    IReadOnlyList<ITool> GetAllTools();
    IReadOnlyList<ITool> GetToolsByCategory(string category);
    string GetToolsAsSystemPrompt();
}

/// <summary>
/// Orchestrates the LLM ↔ Tool invocation loop.
/// </summary>
public interface IAgentRuntime
{
    Task<AgentResponse> ExecuteAsync(AgentRequest request, CancellationToken ct = default);
}

/// <summary>
/// Request to the agent runtime.
/// </summary>
public class AgentRequest
{
    public string Message { get; init; } = string.Empty;
    public string SessionId { get; init; } = string.Empty;
    public int UserId { get; init; }
    public string Username { get; init; } = string.Empty;
    public List<string> Roles { get; init; } = new();
    public List<int> ProductionLineIds { get; init; } = new();
    public List<AgentMessage> History { get; init; } = new();
    public string? Model { get; init; }
}

/// <summary>
/// Response from the agent runtime.
/// </summary>
public class AgentResponse
{
    public string Response { get; init; } = string.Empty;
    public List<string> ToolsInvoked { get; init; } = new();
    public List<AgentMessage> Messages { get; init; } = new();
    public int TokensUsed { get; init; }
    public decimal DurationMs { get; init; }
    public bool IsSuccess { get; init; }
    public string? ErrorMessage { get; init; }
}

/// <summary>
/// A single message in the agent conversation.
/// </summary>
public class AgentMessage
{
    public string Role { get; init; } = string.Empty;
    public string Content { get; init; } = string.Empty;
    public string? ToolName { get; init; }
    public string? ToolResult { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}

/// <summary>
/// Interface for the LLM client (supports mock and real implementations).
/// </summary>
public interface ILlmClient
{
    Task<LlmResponse> CompleteAsync(LlmRequest request, CancellationToken ct = default);
    Task<List<string>> GetAvailableModelsAsync(CancellationToken ct = default);
}

/// <summary>
/// Request to the LLM.
/// </summary>
public class LlmRequest
{
    public string SystemPrompt { get; init; } = string.Empty;
    public List<AgentMessage> Messages { get; init; } = new();
    public List<ToolDefinition> AvailableTools { get; init; } = new();
    public string? ModelOverride { get; init; }
}

/// <summary>
/// Response from the LLM.
/// </summary>
public class LlmResponse
{
    public string Content { get; init; } = string.Empty;
    public ToolCall? ToolCall { get; init; }
    public bool FinishReason { get; init; }
    public int TokensUsed { get; init; }
}

/// <summary>
/// A tool definition exposed to the LLM.
/// </summary>
public class ToolDefinition
{
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public JsonObject Schema { get; init; } = new();
}

/// <summary>
/// A tool call requested by the LLM.
/// </summary>
public class ToolCall
{
    public string Name { get; init; } = string.Empty;
    public JsonObject? Arguments { get; init; }
}