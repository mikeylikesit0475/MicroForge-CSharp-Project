using MicroForge.Domain.Configuration;
using MicroForge.Domain.Interfaces;
using MicroForge.Infrastructure.Telemetry;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Text;
using System.Text.Json.Nodes;

namespace MicroForge.Infrastructure.AI;

/// <summary>
/// Orchestrates the LLM ↔ Tool invocation loop.
/// Sends the system prompt + tool definitions to the LLM, executes any tool calls,
/// feeds results back, and repeats until the LLM produces a final answer or max iterations is reached.
/// </summary>
public class AgentRuntime : IAgentRuntime
{
    private readonly IToolRegistry _toolRegistry;
    private readonly ILlmClient _llmClient;
    private readonly AgentSettings _settings;
    private readonly ILogger<AgentRuntime> _logger;
    private readonly AppMetricsService _metrics;

    public AgentRuntime(
        IToolRegistry toolRegistry,
        ILlmClient llmClient,
        IOptions<AgentSettings> settings,
        ILogger<AgentRuntime> logger,
        AppMetricsService metrics)
    {
        _toolRegistry = toolRegistry;
        _llmClient = llmClient;
        _settings = settings.Value;
        _logger = logger;
        _metrics = metrics;
    }

    public async Task<AgentResponse> ExecuteAsync(AgentRequest request, CancellationToken ct = default)
    {
        var sw = Stopwatch.StartNew();
        var toolsInvoked = new List<string>();
        var conversationMessages = new List<AgentMessage>();
        var totalTokens = 0;

        var systemPrompt = BuildSystemPrompt(request);
        var toolDefs = _toolRegistry.GetAllTools().Select(t => new ToolDefinition
        {
            Name = t.Name,
            Description = t.Description,
            Schema = t.Schema
        }).ToList();

        conversationMessages.Add(new AgentMessage { Role = "user", Content = request.Message });

        for (var iteration = 0; iteration < _settings.MaxIterations; iteration++)
        {
            var llmRequest = new LlmRequest
            {
                SystemPrompt = systemPrompt,
                Messages = conversationMessages.ToList(),
                AvailableTools = toolDefs,
                ModelOverride = request.Model
            };

            var llmResponse = await _llmClient.CompleteAsync(llmRequest, ct);
            totalTokens += llmResponse.TokensUsed;

            if (llmResponse.ToolCall == null || llmResponse.FinishReason)
            {
                sw.Stop();
                return new AgentResponse
                {
                    Response = llmResponse.Content,
                    ToolsInvoked = toolsInvoked,
                    Messages = conversationMessages,
                    TokensUsed = totalTokens,
                    DurationMs = (decimal)sw.Elapsed.TotalMilliseconds,
                    IsSuccess = true
                };
            }

            var toolCall = llmResponse.ToolCall;
            var tool = _toolRegistry.GetTool(toolCall.Name);
            if (tool == null)
            {
                conversationMessages.Add(new AgentMessage
                {
                    Role = "assistant",
                    Content = llmResponse.Content
                });
                conversationMessages.Add(new AgentMessage
                {
                    Role = "tool",
                    Content = $"Error: Tool '{toolCall.Name}' not found",
                    ToolName = toolCall.Name,
                    ToolResult = $"Error: Tool '{toolCall.Name}' not found"
                });
                continue;
            }

            toolsInvoked.Add(toolCall.Name);
            _metrics.IncrementAgentToolInvocations();
            _logger.LogInformation("Executing tool {ToolName} for user {Username} (iteration {Iteration})",
                toolCall.Name, request.Username, iteration);

            conversationMessages.Add(new AgentMessage
            {
                Role = "assistant",
                Content = llmResponse.Content,
                ToolName = toolCall.Name
            });

            var context = new ToolExecutionContext
            {
                UserId = request.UserId,
                Username = request.Username,
                SessionId = request.SessionId,
                Roles = request.Roles,
                ProductionLineIds = request.ProductionLineIds,
                CancellationToken = ct
            };

            try
            {
                using var cts = CancellationTokenSource.CreateLinkedTokenSource(ct);
                cts.CancelAfter(TimeSpan.FromSeconds(_settings.PerToolTimeoutSeconds));

                var toolContext = new ToolExecutionContext
                {
                    UserId = context.UserId,
                    Username = context.Username,
                    SessionId = context.SessionId,
                    Roles = context.Roles,
                    ProductionLineIds = context.ProductionLineIds,
                    CancellationToken = cts.Token
                };

                var result = await tool.InvokeAsync(toolCall.Arguments, toolContext, cts.Token);
                var resultText = result.Success ? result.Output : $"Error: {result.Error}";

                conversationMessages.Add(new AgentMessage
                {
                    Role = "tool",
                    Content = resultText,
                    ToolName = toolCall.Name,
                    ToolResult = resultText
                });
            }
            catch (OperationCanceledException)
            {
                var timeoutMsg = $"Error: Tool '{toolCall.Name}' timed out";
                conversationMessages.Add(new AgentMessage
                {
                    Role = "tool",
                    Content = timeoutMsg,
                    ToolName = toolCall.Name,
                    ToolResult = timeoutMsg
                });
                _logger.LogWarning("Tool {ToolName} timed out", toolCall.Name);
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error: {ex.Message}";
                conversationMessages.Add(new AgentMessage
                {
                    Role = "tool",
                    Content = errorMsg,
                    ToolName = toolCall.Name,
                    ToolResult = errorMsg
                });
                _logger.LogError(ex, "Tool {ToolName} failed", toolCall.Name);
            }
        }

        sw.Stop();
        return new AgentResponse
        {
            Response = "I reached the maximum number of tool iterations. Please try rephrasing your question.",
            ToolsInvoked = toolsInvoked,
            Messages = conversationMessages,
            TokensUsed = totalTokens,
            DurationMs = (decimal)sw.Elapsed.TotalMilliseconds,
            IsSuccess = true
        };
    }

    private string BuildSystemPrompt(AgentRequest request)
    {
        var sb = new StringBuilder();
        sb.AppendLine("You are the MicroForge AI Agent, an intelligent assistant for a manufacturing intelligence platform.");
        sb.AppendLine("You have access to tools that let you query production data, sensor readings, defects, documents, and more.");
        sb.AppendLine("Always use tools to gather data before answering questions about specific production metrics.");
        sb.AppendLine("When you have enough information, provide a clear, concise answer.");
        sb.AppendLine();
        sb.AppendLine($"User: {request.Username}");
        sb.AppendLine($"Roles: {string.Join(", ", request.Roles)}");
        if (request.ProductionLineIds.Count > 0)
            sb.AppendLine($"Assigned production lines: {string.Join(", ", request.ProductionLineIds)}");
        sb.AppendLine();
        sb.AppendLine(_toolRegistry.GetToolsAsSystemPrompt());
        return sb.ToString();
    }
}
