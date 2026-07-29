using MicroForge.Application.Services;
using MicroForge.Domain.Interfaces;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;

namespace MicroForge.Infrastructure.AI.Tools;

/// <summary>
/// Tool for triggering autonomous closed-loop self-healing remediation on a lot.
/// </summary>
public class TriggerSelfHealingTool : BaseTool
{
    private readonly IFabSelfHealingService _selfHealingService;

    public TriggerSelfHealingTool(IFabSelfHealingService selfHealingService)
    {
        _selfHealingService = selfHealingService;
    }

    public override string Name => "trigger_self_healing_remediation";
    public override string Description => "Execute autonomous closed-loop fab remediation for a lot: quatantines lot, creates CAPA, drafts ECO recipe tuning, and routes lot to rework step.";
    public override string Category => "fdc";

    public override JsonObject Schema => new()
    {
        ["type"] = "object",
        ["properties"] = new JsonObject
        {
            ["lotId"] = new JsonObject { ["type"] = "integer", ["description"] = "Lot ID to remediate." },
            ["anomalyDescription"] = new JsonObject { ["type"] = "string", ["description"] = "Root cause anomaly description." }
        },
        ["required"] = new JsonArray { "lotId" }
    };

    public override async Task<ToolResult> InvokeAsync(JsonObject? arguments, ToolExecutionContext context, CancellationToken ct = default)
    {
        var lotId = GetInt(arguments, "lotId");
        var anomaly = GetString(arguments, "anomalyDescription") ?? "Thermal Runaway (+4.2σ Drift)";

        var result = await _selfHealingService.TriggerSelfHealingAsync(lotId, anomaly, ct);
        if (result == null) return ToolResult.Fail($"Lot ID {lotId} not found.");

        return ToolResult.Ok(Serialize(result));
    }
}
