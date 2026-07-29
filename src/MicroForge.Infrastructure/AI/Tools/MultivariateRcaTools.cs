using MicroForge.Application.Services;
using MicroForge.Domain.Interfaces;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;

namespace MicroForge.Infrastructure.AI.Tools;

/// <summary>
/// Tool for performing multi-variate trace diffing against Golden Run profiles grounded by RAG SOP citations.
/// </summary>
public class AnalyzeRunTraceWithRagTool : BaseTool
{
    private readonly IMultivariateRcaService _rcaService;

    public AnalyzeRunTraceWithRagTool(IMultivariateRcaService rcaService)
    {
        _rcaService = rcaService;
    }

    public override string Name => "analyze_run_trace_with_rag_citation";
    public override string Description => "Analyzes multi-variate sensor telemetry trace against Golden Run baseline and grounds deviation with RAG SOP runbook citations.";
    public override string Category => "analytics";

    public override JsonObject Schema => new()
    {
        ["type"] = "object",
        ["properties"] = new JsonObject
        {
            ["runId"] = new JsonObject { ["type"] = "integer", ["description"] = "Production Run ID." },
            ["sensorType"] = new JsonObject { ["type"] = "string", ["description"] = "Sensor type (RF_Power, Temperature, Chamber_Pressure, Gas_Flow)." }
        },
        ["required"] = new JsonArray { "runId" }
    };

    public override async Task<ToolResult> InvokeAsync(JsonObject? arguments, ToolExecutionContext context, CancellationToken ct = default)
    {
        var runId = GetInt(arguments, "runId");
        var sensor = GetString(arguments, "sensorType") ?? "RF_Power";

        var result = await _rcaService.GetMultivariateRcaAsync(runId, sensor, ct);
        return ToolResult.Ok(Serialize(result));
    }
}
