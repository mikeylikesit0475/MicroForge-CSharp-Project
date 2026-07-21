using MicroForge.Application.Services;
using MicroForge.Domain.Entities;
using MicroForge.Domain.Interfaces;
using MicroForge.Infrastructure.Events;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;

namespace MicroForge.Infrastructure.AI.Tools;

/// <summary>
/// Tool for querying WIP lots, statuses, and active steps.
/// </summary>
public class QueryWipLotsTool : BaseTool
{
    private readonly IWipService _wipService;
    public QueryWipLotsTool(IWipService wipService) => _wipService = wipService;

    public override string Name => "query_wip_lots";
    public override string Description => "Query active WIP lots in the fab, including their active route step, status (Active/Hold/Completed), and wafer count.";
    public override string Category => "wip";
    public override JsonObject Schema => new()
    {
        ["type"] = "object",
        ["properties"] = new JsonObject
        {
            ["lotId"] = new JsonObject { ["type"] = "integer", ["description"] = "Optional specific lot ID to get detailed wafer slots and route log history." }
        }
    };

    public override async Task<ToolResult> InvokeAsync(JsonObject? arguments, ToolExecutionContext context, CancellationToken ct = default)
    {
        var lotId = GetInt(arguments, "lotId");
        if (lotId > 0)
        {
            var detail = await _wipService.GetLotDetailAsync(lotId, ct);
            if (detail == null) return ToolResult.Fail($"Lot ID {lotId} not found");
            return ToolResult.Ok(Serialize(detail));
        }

        var lots = await _wipService.GetActiveLotsAsync(ct);
        return ToolResult.Ok(Serialize(lots));
    }
}

/// <summary>
/// Tool for moving a WIP lot to a target step in the production route.
/// </summary>
public class MoveWipLotTool : BaseTool
{
    private readonly IWipService _wipService;
    public MoveWipLotTool(IWipService wipService) => _wipService = wipService;

    public override string Name => "move_wip_lot";
    public override string Description => "Move a WIP lot to a target step in the production route. Note: blocks if mandatory EWI checklists are incomplete or tool is unqualified.";
    public override string Category => "wip";
    public override JsonObject Schema => new()
    {
        ["type"] = "object",
        ["properties"] = new JsonObject
        {
            ["lotId"] = new JsonObject { ["type"] = "integer" },
            ["targetStepId"] = new JsonObject { ["type"] = "integer" }
        },
        ["required"] = new JsonArray { "lotId", "targetStepId" }
    };

    public override async Task<ToolResult> InvokeAsync(JsonObject? arguments, ToolExecutionContext context, CancellationToken ct = default)
    {
        var lotId = GetInt(arguments, "lotId");
        var stepId = GetInt(arguments, "targetStepId");

        var success = await _wipService.MoveLotToStepAsync(lotId, stepId, 1, ct);
        if (!success) return ToolResult.Fail("Move blocked. Verify the lot is active, target step exists, tool is qualified, and EWI checklists are complete.");
        return ToolResult.Ok(Serialize(new { success = true, message = "Lot successfully moved." }));
    }
}

/// <summary>
/// Tool for placing a WIP lot on engineering hold status.
/// </summary>
public class HoldWipLotTool : BaseTool
{
    private readonly IWipService _wipService;
    public HoldWipLotTool(IWipService wipService) => _wipService = wipService;

    public override string Name => "hold_wip_lot";
    public override string Description => "Place a WIP lot on Engineering Quarantine Hold status.";
    public override string Category => "wip";
    public override JsonObject Schema => new()
    {
        ["type"] = "object",
        ["properties"] = new JsonObject
        {
            ["lotId"] = new JsonObject { ["type"] = "integer" },
            ["reason"] = new JsonObject { ["type"] = "string" }
        },
        ["required"] = new JsonArray { "lotId", "reason" }
    };

    public override async Task<ToolResult> InvokeAsync(JsonObject? arguments, ToolExecutionContext context, CancellationToken ct = default)
    {
        var lotId = GetInt(arguments, "lotId");
        var reason = GetString(arguments, "reason") ?? "Manual hold";

        var success = await _wipService.HoldLotAsync(lotId, reason, ct);
        if (!success) return ToolResult.Fail("Failed to place hold on lot.");
        return ToolResult.Ok(Serialize(new { success = true, message = "Lot placed on quarantine hold." }));
    }
}

/// <summary>
/// Tool for releasing a WIP lot from hold status.
/// </summary>
public class ReleaseWipLotTool : BaseTool
{
    private readonly IWipService _wipService;
    public ReleaseWipLotTool(IWipService wipService) => _wipService = wipService;

    public override string Name => "release_wip_lot";
    public override string Description => "Release a WIP lot from Engineering Hold status to restore active routing.";
    public override string Category => "wip";
    public override JsonObject Schema => new()
    {
        ["type"] = "object",
        ["properties"] = new JsonObject
        {
            ["lotId"] = new JsonObject { ["type"] = "integer" }
        },
        ["required"] = new JsonArray { "lotId" }
    };

    public override async Task<ToolResult> InvokeAsync(JsonObject? arguments, ToolExecutionContext context, CancellationToken ct = default)
    {
        var lotId = GetInt(arguments, "lotId");
        var success = await _wipService.ReleaseLotAsync(lotId, ct);
        if (!success) return ToolResult.Fail("Failed to release hold on lot.");
        return ToolResult.Ok(Serialize(new { success = true, message = "Lot released from hold." }));
    }
}

/// <summary>
/// Tool for querying OEE summaries for production lines.
/// </summary>
public class GetOeeMetricsTool : BaseTool
{
    private readonly IFabExpansionService _fabService;
    public GetOeeMetricsTool(IFabExpansionService fabService) => _fabService = fabService;

    public override string Name => "get_oee_metrics";
    public override string Description => "Get live OEE (Overall Equipment Effectiveness) summaries including Availability, Performance, and Quality parameters for all active production lines.";
    public override string Category => "analytics";
    public override JsonObject Schema => new() { ["type"] = "object", ["properties"] = new JsonObject() };

    public override async Task<ToolResult> InvokeAsync(JsonObject? arguments, ToolExecutionContext context, CancellationToken ct = default)
    {
        var summaries = await _fabService.GetOeeSummariesAsync(ct);
        return ToolResult.Ok(Serialize(summaries));
    }
}

/// <summary>
/// Tool for querying active FDC warnings and tool baseline mismatches.
/// </summary>
public class QueryFdcDiagnosticsTool : BaseTool
{
    private readonly IFdcService _fdcService;
    public QueryFdcDiagnosticsTool(IFdcService fdcService) => _fdcService = fdcService;

    public override string Name => "query_fdc_diagnostics";
    public override string Description => "Query active FDC diagnostics, including baseline chamber mismatches, active trace fault alarms, and remaining useful life (RUL) of parts.";
    public override string Category => "fdc";
    public override JsonObject Schema => new() { ["type"] = "object", ["properties"] = new JsonObject() };

    public override async Task<ToolResult> InvokeAsync(JsonObject? arguments, ToolExecutionContext context, CancellationToken ct = default)
    {
        var matching = await _fdcService.GetChamberMatchingAsync(ct);
        var maintenance = await _fdcService.GetMaintenanceStatusAsync(ct);
        var faults = await _fdcService.GetActiveFaultsAsync(ct);

        return ToolResult.Ok(Serialize(new
        {
            activeFaults = faults,
            chamberMatching = matching,
            consumableRul = maintenance
        }));
    }
}

/// <summary>
/// Tool for managing CAPA (Corrective and Preventive Action) items.
/// </summary>
public class ManageCapaActionsTool : BaseTool
{
    private readonly IFabExpansionService _fabService;
    public ManageCapaActionsTool(IFabExpansionService fabService) => _fabService = fabService;

    public override string Name => "manage_capa_actions";
    public override string Description => "Manage Corrective and Preventive Actions (CAPA) actions. Can list, create, or approve items.";
    public override string Category => "collaboration";
    public override JsonObject Schema => new()
    {
        ["type"] = "object",
        ["properties"] = new JsonObject
        {
            ["action"] = new JsonObject { ["type"] = "string", ["description"] = "list, create, or approve" },
            ["lotId"] = new JsonObject { ["type"] = "integer" },
            ["description"] = new JsonObject { ["type"] = "string" },
            ["assignee"] = new JsonObject { ["type"] = "string" },
            ["capaId"] = new JsonObject { ["type"] = "integer" }
        },
        ["required"] = new JsonArray { "action" }
    };

    public override async Task<ToolResult> InvokeAsync(JsonObject? arguments, ToolExecutionContext context, CancellationToken ct = default)
    {
        var action = GetString(arguments, "action");
        if (action == "list")
        {
            var list = await _fabService.GetCapasAsync(ct);
            return ToolResult.Ok(Serialize(list));
        }
        else if (action == "create")
        {
            var lotIdVal = GetInt(arguments, "lotId");
            int? lotId = lotIdVal > 0 ? lotIdVal : null;
            var desc = GetString(arguments, "description") ?? "";
            var assignee = GetString(arguments, "assignee") ?? "";

            var record = await _fabService.CreateCapaAsync(lotId, desc, assignee, ct);
            return ToolResult.Ok(Serialize(record));
        }
        else if (action == "approve")
        {
            var capaId = GetInt(arguments, "capaId");
            var success = await _fabService.ApproveCapaAsync(capaId, "agent_system", ct);
            if (!success) return ToolResult.Fail("CAPA approval failed. Check ID exists.");
            return ToolResult.Ok(Serialize(new { success = true }));
        }

        return ToolResult.Fail("Invalid action parameter.");
    }
}

/// <summary>
/// Tool for querying simulated SECS traffic logs.
/// </summary>
public class GetSecsGemLogsTool : BaseTool
{
    public override string Name => "get_secs_gem_logs";
    public override string Description => "Query simulated SECS message traffic logs (HSMS exchange messages: Select.req, S1F1 online query, S2F21 remote commands).";
    public override string Category => "integration";
    public override JsonObject Schema => new() { ["type"] = "object", ["properties"] = new JsonObject() };

    public override Task<ToolResult> InvokeAsync(JsonObject? arguments, ToolExecutionContext context, CancellationToken ct = default)
    {
        var logs = SecsGemSimulator.TrafficLogs.ToList();
        return Task.FromResult(ToolResult.Ok(Serialize(logs)));
    }
}

/// <summary>
/// Tool for managing recipe-to-chamber qualification parameters.
/// </summary>
public class ManageQualificationsTool : BaseTool
{
    private readonly IWipService _wipService;
    public ManageQualificationsTool(IWipService wipService) => _wipService = wipService;

    public override string Name => "manage_qualifications";
    public override string Description => "Retrieve tool qualification mappings or qualify/unqualify a recipe for a production line.";
    public override string Category => "wip";
    public override JsonObject Schema => new()
    {
        ["type"] = "object",
        ["properties"] = new JsonObject
        {
            ["action"] = new JsonObject { ["type"] = "string", ["description"] = "list or set" },
            ["lineId"] = new JsonObject { ["type"] = "integer" },
            ["recipeName"] = new JsonObject { ["type"] = "string" },
            ["isQualified"] = new JsonObject { ["type"] = "boolean" }
        },
        ["required"] = new JsonArray { "action" }
    };

    public override async Task<ToolResult> InvokeAsync(JsonObject? arguments, ToolExecutionContext context, CancellationToken ct = default)
    {
        var action = GetString(arguments, "action");
        if (action == "list")
        {
            var list = await _wipService.GetQualificationsAsync(ct);
            return ToolResult.Ok(Serialize(list));
        }
        else if (action == "set")
        {
            var lineId = GetInt(arguments, "lineId");
            var recipe = GetString(arguments, "recipeName") ?? "";
            var isQual = GetBool(arguments, "isQualified", true);

            await _wipService.SetQualificationAsync(lineId, recipe, isQual, ct);
            return ToolResult.Ok(Serialize(new { success = true }));
        }

        return ToolResult.Fail("Invalid action parameter.");
    }
}
