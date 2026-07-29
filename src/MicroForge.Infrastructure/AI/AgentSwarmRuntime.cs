using MicroForge.Domain.Configuration;
using MicroForge.Domain.Interfaces;
using MicroForge.Infrastructure.Telemetry;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;

namespace MicroForge.Infrastructure.AI;

/// <summary>
/// Implements a Multi-Agent Swarm system where a central Orchestrator
/// delegates queries to specialized Sub-Agents using recursive delegation tools.
/// </summary>
public class AgentSwarmRuntime : IAgentRuntime
{
    private readonly IToolRegistry _toolRegistry;
    private readonly ILlmClient _llmClient;
    private readonly AgentSettings _settings;
    private readonly ILogger<AgentSwarmRuntime> _logger;
    private readonly AppMetricsService _metrics;

    public AgentSwarmRuntime(
        IToolRegistry toolRegistry,
        ILlmClient llmClient,
        IOptions<AgentSettings> settings,
        ILogger<AgentSwarmRuntime> logger,
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

        // Categorize all tools from the registry
        var allTools = _toolRegistry.GetAllTools().ToList();
        var wipTools = allTools.Where(t => t.Category == "wip" || t.Category == "production" || t.Category == "dashboard").ToList();
        var fdcTools = allTools.Where(t => t.Category == "sensors" || t.Category == "fdc").ToList();
        var qualityTools = allTools.Where(t => t.Category == "quality" || t.Category == "analytics").ToList();
        var alertCollabTools = allTools.Where(t => t.Category == "alerts" || t.Category == "collaboration" || t.Category == "integration" || t.Category == "rag" || t.Category == "documents").ToList();

        // Create delegation tools for the Orchestrator
        var delegationTools = new List<ITool>
        {
            new SwarmDelegationTool(
                "delegate_to_wip_agent",
                "Delegates queries to the WIP Sub-Agent (manages lot schedules, wafer route tracking, quarantine holds, and qualification configs).",
                wipTools,
                "WIP Specialist Agent",
                "You are the WIP Specialist Agent. You manage lot statuses, cassette configurations, routing steps, and recipe qualifications.",
                this,
                ct
            ),
            new SwarmDelegationTool(
                "delegate_to_fdc_agent",
                "Delegates queries to the FDC & Sensors Sub-Agent (monitors active sensor readings, fault alarms, chamber baseline deviations, and consumable RUL).",
                fdcTools,
                "FDC and Sensor Specialist Agent",
                "You are the FDC & Sensor Specialist Agent. You monitor telemetry, trace faults, baseline matching, and remaining useful life (RUL) limits.",
                this,
                ct
            ),
            new SwarmDelegationTool(
                "delegate_to_quality_agent",
                "Delegates queries to the Quality & Analytics Sub-Agent (manages defect reviews, yield metrics, correlation analysis, and statistics).",
                qualityTools,
                "Quality and Analytics Specialist Agent",
                "You are the Quality & Analytics Specialist Agent. You review defect logs, yield metrics, correlation charts, and process statistics.",
                this,
                ct
            ),
            new SwarmDelegationTool(
                "delegate_to_alert_collab_agent",
                "Delegates queries to the Alerts & Collaboration Sub-Agent (manages active alert acknowledgements, CAPAs, ECO logs, shift handovers, and SECS/GEM logs).",
                alertCollabTools,
                "Alerts and Collaboration Specialist Agent",
                "You are the Alerts & Collaboration Specialist Agent. You manage active alarms, CAPA action tasks, recipe ECOs, shift passdowns, and SECS/GEM logs.",
                this,
                ct
            )
        };

        var toolDefs = delegationTools.Select(t => new ToolDefinition
        {
            Name = t.Name,
            Description = t.Description,
            Schema = t.Schema
        }).ToList();

        conversationMessages.Add(new AgentMessage { Role = "user", Content = request.Message });

        var orchestratorPrompt = BuildOrchestratorPrompt(request);

        for (var iteration = 0; iteration < _settings.MaxIterations; iteration++)
        {
            var isLastIteration = iteration == _settings.MaxIterations - 1;

            var llmRequest = new LlmRequest
            {
                SystemPrompt = orchestratorPrompt,
                Messages = conversationMessages.ToList(),
                AvailableTools = isLastIteration ? new List<ToolDefinition>() : toolDefs,
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
            var targetDelegationTool = delegationTools.FirstOrDefault(t => t.Name == toolCall.Name);
            if (targetDelegationTool == null)
            {
                conversationMessages.Add(new AgentMessage { Role = "assistant", Content = llmResponse.Content });
                conversationMessages.Add(new AgentMessage
                {
                    Role = "tool",
                    Content = $"Error: Delegation target '{toolCall.Name}' not found",
                    ToolName = toolCall.Name
                });
                continue;
            }

            toolsInvoked.Add(toolCall.Name);
            _metrics.IncrementAgentToolInvocations();
            _logger.LogInformation("Orchestrator delegating to sub-agent: {ToolName}", toolCall.Name);

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
                var result = await targetDelegationTool.InvokeAsync(toolCall.Arguments, context, ct);
                var resultText = result.Success ? result.Output : $"Error: {result.Error}";

                conversationMessages.Add(new AgentMessage
                {
                    Role = "tool",
                    Content = resultText,
                    ToolName = toolCall.Name,
                    ToolResult = resultText
                });
            }
            catch (Exception ex)
            {
                conversationMessages.Add(new AgentMessage
                {
                    Role = "tool",
                    Content = $"Error: {ex.Message}",
                    ToolName = toolCall.Name,
                    ToolResult = $"Error: {ex.Message}"
                });
            }
        }

        sw.Stop();
        return new AgentResponse
        {
            Response = "Orchestrator reached maximum delegation limit.",
            ToolsInvoked = toolsInvoked,
            Messages = conversationMessages,
            TokensUsed = totalTokens,
            DurationMs = (decimal)sw.Elapsed.TotalMilliseconds,
            IsSuccess = true
        };
    }

    public async Task<string> RunSubAgentInternalAsync(
        string subAgentName,
        List<ITool> allowedTools,
        string baseSystemPrompt,
        string subQuery,
        ToolExecutionContext parentContext,
        CancellationToken ct)
    {
        var conversationMessages = new List<AgentMessage>();
        var toolDefs = allowedTools.Select(t => new ToolDefinition
        {
            Name = t.Name,
            Description = t.Description,
            Schema = t.Schema
        }).ToList();

        conversationMessages.Add(new AgentMessage { Role = "user", Content = subQuery });
        var systemPrompt = baseSystemPrompt + $"\n\nYou are the specialized {subAgentName}. Address the query using your tools.";

        for (var iteration = 0; iteration < _settings.MaxIterations; iteration++)
        {
            var isLastIteration = iteration == _settings.MaxIterations - 1;

            var llmRequest = new LlmRequest
            {
                SystemPrompt = systemPrompt,
                Messages = conversationMessages.ToList(),
                AvailableTools = isLastIteration ? new List<ToolDefinition>() : toolDefs
            };

            var llmResponse = await _llmClient.CompleteAsync(llmRequest, ct);

            if (llmResponse.ToolCall == null || llmResponse.FinishReason)
            {
                return llmResponse.Content;
            }

            var toolCall = llmResponse.ToolCall;
            var tool = allowedTools.FirstOrDefault(t => t.Name == toolCall.Name);
            if (tool == null)
            {
                conversationMessages.Add(new AgentMessage { Role = "assistant", Content = llmResponse.Content });
                conversationMessages.Add(new AgentMessage
                {
                    Role = "tool",
                    Content = $"Error: Tool '{toolCall.Name}' not available to this specialist.",
                    ToolName = toolCall.Name
                });
                continue;
            }

            conversationMessages.Add(new AgentMessage
            {
                Role = "assistant",
                Content = llmResponse.Content,
                ToolName = toolCall.Name
            });

            try
            {
                var result = await tool.InvokeAsync(toolCall.Arguments, parentContext, ct);
                var resultText = result.Success ? result.Output : $"Error: {result.Error}";

                conversationMessages.Add(new AgentMessage
                {
                    Role = "tool",
                    Content = resultText,
                    ToolName = toolCall.Name,
                    ToolResult = resultText
                });
            }
            catch (Exception ex)
            {
                conversationMessages.Add(new AgentMessage
                {
                    Role = "tool",
                    Content = $"Error: {ex.Message}",
                    ToolName = toolCall.Name,
                    ToolResult = $"Error: {ex.Message}"
                });
            }
        }

        return $"Sub-agent {subAgentName} reached max iterations.";
    }

    private string BuildOrchestratorPrompt(AgentRequest request)
    {
        var sb = new StringBuilder();
        sb.AppendLine("You are the Swarm Orchestrator Agent for MicroForge.");
        sb.AppendLine("Your job is to route incoming queries to the correct specialized sub-agent.");
        sb.AppendLine("You do NOT have access to raw database tools yourself; you MUST delegate to one of the following specialists:");
        sb.AppendLine("- WIP Sub-Agent (`delegate_to_wip_agent`): For lots, route tracking, holds, and qualifications.");
        sb.AppendLine("- FDC & Sensor Sub-Agent (`delegate_to_fdc_agent`): For telemetry, baseline drift, matching, and part RUL.");
        sb.AppendLine("- Quality Sub-Agent (`delegate_to_quality_agent`): For defects, yield trends, and statistics.");
        sb.AppendLine("- Alerts & Collaboration Sub-Agent (`delegate_to_alert_collab_agent`): For alarms, CAPAs, ECOs, shift handovers, and SECS/GEM.");
        sb.AppendLine();
        sb.AppendLine("Call the appropriate tool, read the specialist's answer, and synthesize it clearly for the user.");
        return sb.ToString();
    }

    /// <summary>
    /// Custom ITool implementation representing a delegation target for the Orchestrator.
    /// </summary>
    private class SwarmDelegationTool : ITool
    {
        private readonly List<ITool> _subAgentTools;
        private readonly string _subAgentName;
        private readonly string _subAgentPrompt;
        private readonly AgentSwarmRuntime _swarmRuntime;

        public SwarmDelegationTool(
            string name,
            string description,
            List<ITool> subAgentTools,
            string subAgentName,
            string subAgentPrompt,
            AgentSwarmRuntime swarmRuntime,
            CancellationToken ct)
        {
            Name = name;
            Description = description;
            _subAgentTools = subAgentTools;
            _subAgentName = subAgentName;
            _subAgentPrompt = subAgentPrompt;
            _swarmRuntime = swarmRuntime;
        }

        public string Name { get; }
        public string Description { get; }
        public string Category => "swarm";
        public JsonObject Schema => new()
        {
            ["type"] = "object",
            ["properties"] = new JsonObject
            {
                ["query"] = new JsonObject { ["type"] = "string", ["description"] = "The query or instruction to send to the specialist agent." }
            },
            ["required"] = new JsonArray { "query" }
        };

        public async Task<ToolResult> InvokeAsync(JsonObject? arguments, ToolExecutionContext context, CancellationToken ct = default)
        {
            if (arguments == null || !arguments.TryGetPropertyValue("query", out var node) || node == null)
                return ToolResult.Fail("Missing required 'query' argument.");

            var query = node.GetValue<string>();
            var response = await _swarmRuntime.RunSubAgentInternalAsync(
                _subAgentName,
                _subAgentTools,
                _subAgentPrompt,
                query,
                context,
                ct
            );

            return ToolResult.Ok(response);
        }
    }
}
