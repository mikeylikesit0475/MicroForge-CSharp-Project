using MicroForge.Domain.Configuration;
using MicroForge.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace MicroForge.Infrastructure.AI;

/// <summary>
/// LLM client that calls an OpenAI-compatible API endpoint.
/// Falls back to a heuristic response generator when the endpoint is unavailable
/// or when configured with provider "mock".
/// </summary>
public class LlmService : ILlmClient
{
    private readonly HttpClient _httpClient;
    private readonly AgentSettings _settings;
    private readonly ILogger<LlmService> _logger;

    public LlmService(HttpClient httpClient, IOptions<AgentSettings> settings, ILogger<LlmService> logger)
    {
        _httpClient = httpClient;
        _settings = settings.Value;
        _logger = logger;
    }

    public async Task<LlmResponse> CompleteAsync(LlmRequest request, CancellationToken ct = default)
    {
        if (string.IsNullOrEmpty(_settings.LlmProvider) || _settings.LlmProvider == "mock" ||
            string.IsNullOrEmpty(_settings.LlmEndpoint))
        {
            return GenerateMockResponse(request);
        }

        try
        {
            var messages = new List<object>
            {
                new { role = "system", content = request.SystemPrompt }
            };
            foreach (var msg in request.Messages)
            {
                messages.Add(new { role = msg.Role, content = msg.Content });
            }

            var payload = new
            {
                model = _settings.LlmModel,
                messages,
                temperature = 0.3,
                max_tokens = 2000
            };

            var response = await _httpClient.PostAsJsonAsync($"{_settings.LlmEndpoint}/v1/chat/completions", payload, ct);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<JsonElement>(cancellationToken: ct);
            var content = result.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString() ?? "";
            var tokens = result.TryGetProperty("usage", out var usage) && usage.TryGetProperty("total_tokens", out var t)
                ? t.GetInt32() : 0;

            return new LlmResponse
            {
                Content = content,
                ToolCall = null,
                FinishReason = true,
                TokensUsed = tokens
            };
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "LLM endpoint unavailable, using mock response");
            return GenerateMockResponse(request);
        }
    }

    private LlmResponse GenerateMockResponse(LlmRequest request)
    {
        var lastUserMessage = request.Messages.LastOrDefault(m => m.Role == "user")?.Content ?? "";
        var toolMessages = request.Messages.Where(m => m.Role == "tool").ToList();

        if (toolMessages.Count > 0)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Based on the data I retrieved:");
            foreach (var tm in toolMessages)
            {
                sb.AppendLine($"- {tm.Content}");
            }
            sb.AppendLine();
            sb.AppendLine("This information was gathered using the available tools. " +
                          "For more detailed analysis, please check the production dashboard.");

            return new LlmResponse
            {
                Content = sb.ToString(),
                ToolCall = null,
                FinishReason = true,
                TokensUsed = EstimateTokens(sb.ToString())
            };
        }

        var toolCall = DetermineToolCall(lastUserMessage, request.AvailableTools);
        if (toolCall != null)
        {
            return new LlmResponse
            {
                Content = "Let me look up that information for you.",
                ToolCall = toolCall,
                FinishReason = false,
                TokensUsed = EstimateTokens(lastUserMessage)
            };
        }

        return new LlmResponse
        {
            Content = "I'm the MicroForge AI Agent. I can help you with:\n" +
                      "- Production line status and dashboard summaries\n" +
                      "- Production runs, yields, and trends\n" +
                      "- Defect records and quality issues\n" +
                      "- Sensor readings and anomaly detection\n" +
                      "- Alerts (query, create, acknowledge)\n" +
                      "- Document search (SOPs, specs, reports)\n" +
                      "- Statistical analysis\n\n" +
                      "Try asking something like:\n" +
                      "- \"What's happening on the floor?\"\n" +
                      "- \"Show me recent defects\"\n" +
                      "- \"Any sensor anomalies?\"\n" +
                      "- \"What's the yield trend?\"",
            ToolCall = null,
            FinishReason = true,
            TokensUsed = 50
        };
    }

    private static ToolCall? DetermineToolCall(string message, List<ToolDefinition> availableTools)
    {
        var lower = message.ToLowerInvariant();
        var toolNames = availableTools.Select(t => t.Name).ToHashSet();

        string? candidate = null;
        JsonObject? args = null;

        // Dashboard / overview / summary queries
        if (lower.Contains("dashboard") || lower.Contains("overview") || lower.Contains("summary") ||
            lower.Contains("kpi") || lower.Contains("how are things") || lower.Contains("what's happening") ||
            lower.Contains("whats happening") || lower.Contains("how's it going") || lower.Contains("hows it going"))
        {
            candidate = "get_dashboard_summary";
            args = new JsonObject();
        }
        // Alert-related queries (check before generic "status" to be more specific)
        else if (lower.Contains("alert") || lower.Contains("alarm") || lower.Contains("warning") ||
                 lower.Contains("acknowledg") || lower.Contains("unacknowledg"))
        {
            if (lower.Contains("acknowledg"))
            {
                candidate = "acknowledge_alert";
                args = new JsonObject { ["alertId"] = 1 };
            }
            else
            {
                candidate = "query_alerts";
                args = new JsonObject { ["limit"] = 20 };
            }
        }
        // Anomaly detection
        else if (lower.Contains("anomal") || lower.Contains("detect") || lower.Contains("outlier"))
        {
            candidate = "detect_anomalies";
            args = new JsonObject();
        }
        // Defect queries
        else if (lower.Contains("defect") || lower.Contains("quality issue") || lower.Contains("reject"))
        {
            candidate = "query_production_defects";
            args = new JsonObject { ["limit"] = 20 };
        }
        // Sensor readings
        else if (lower.Contains("sensor") || lower.Contains("reading") || lower.Contains("temperature") ||
                 lower.Contains("pressure") || lower.Contains("humidity") || lower.Contains("vibration"))
        {
            candidate = "query_sensor_readings";
            args = new JsonObject { ["limit"] = 20 };
        }
        // Production line status
        else if (lower.Contains("status") || lower.Contains("line") || lower.Contains("floor") ||
                 lower.Contains("production line") || lower.Contains("lines"))
        {
            candidate = "get_production_line_status";
            args = new JsonObject();
        }
        // Yield trends
        else if (lower.Contains("yield") || lower.Contains("trend") || lower.Contains("chart"))
        {
            candidate = "get_yield_trend";
            args = new JsonObject();
        }
        // Document search
        else if (lower.Contains("document") || lower.Contains("search") || lower.Contains("sop") ||
                 lower.Contains("spec") || lower.Contains("manual") || lower.Contains("procedure") ||
                 lower.Contains("report") || lower.Contains("instruction"))
        {
            candidate = "search_documents";
            args = new JsonObject { ["query"] = message, ["topK"] = 5 };
        }
        // Production run summary
        else if (lower.Contains("run") || lower.Contains("batch") || lower.Contains("production run"))
        {
            candidate = "get_production_run_summary";
            args = new JsonObject();
        }
        // Statistics
        else if (lower.Contains("statistic") || lower.Contains("average") || lower.Contains("mean") ||
                 lower.Contains("median") || lower.Contains("std") || lower.Contains("standard deviation") ||
                 lower.Contains("min") || lower.Contains("max"))
        {
            candidate = "calculate_statistics";
            args = new JsonObject();
        }
        // Update production run status
        else if (lower.Contains("update") && (lower.Contains("status") || lower.Contains("run")))
        {
            candidate = "update_production_run_status";
            args = new JsonObject();
        }
        // Create alert
        else if (lower.Contains("create") && lower.Contains("alert"))
        {
            candidate = "create_alert";
            args = new JsonObject();
        }
        // Create defect record
        else if (lower.Contains("create") && lower.Contains("defect"))
        {
            candidate = "create_defect_record";
            args = new JsonObject();
        }
        // Query production runs
        else if (lower.Contains("production run") || lower.Contains("list run") || lower.Contains("show run"))
        {
            candidate = "query_production_runs";
            args = new JsonObject { ["limit"] = 20 };
        }

        if (candidate != null && toolNames.Contains(candidate))
        {
            return new ToolCall { Name = candidate, Arguments = args };
        }

        return null;
    }

    private static int EstimateTokens(string text) => (int)Math.Ceiling(text.Length / 4.0);
}
