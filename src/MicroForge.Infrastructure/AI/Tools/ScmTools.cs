using MicroForge.Application.Services;
using MicroForge.Domain.Interfaces;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;

namespace MicroForge.Infrastructure.AI.Tools;

/// <summary>
/// Tool for querying inventory levels, material details, and reorder status.
/// </summary>
public class QueryInventoryTool : BaseTool
{
    private readonly IScmService _scmService;
    public QueryInventoryTool(IScmService scmService) => _scmService = scmService;

    public override string Name => "query_inventory";
    public override string Description => "Query fab inventory materials including stock levels, reorder points, supplier info, and active reservations. Can filter by category or show only items below reorder point.";
    public override string Category => "supply_chain";
    public override JsonObject Schema => new()
    {
        ["type"] = "object",
        ["properties"] = new JsonObject
        {
            ["category"] = new JsonObject { ["type"] = "string", ["description"] = "Filter by material category: RawWafer, Chemical, Gas, Consumable, SparePart" },
            ["belowReorderOnly"] = new JsonObject { ["type"] = "boolean", ["description"] = "If true, return only materials at or below their reorder point" }
        }
    };

    public override async Task<ToolResult> InvokeAsync(JsonObject? arguments, ToolExecutionContext context, CancellationToken ct = default)
    {
        var category = GetString(arguments, "category");
        var belowReorder = GetBool(arguments, "belowReorderOnly");

        var materials = await _scmService.GetMaterialsAsync(category, belowReorder, ct);
        return ToolResult.Ok(Serialize(new { count = materials.Count, materials }));
    }
}

/// <summary>
/// Tool for managing purchase orders — list, create, approve, or receive.
/// </summary>
public class ManagePurchaseOrdersTool : BaseTool
{
    private readonly IScmService _scmService;
    public ManagePurchaseOrdersTool(IScmService scmService) => _scmService = scmService;

    public override string Name => "manage_purchase_orders";
    public override string Description => "Manage purchase orders for fab materials. Actions: list (optionally filter by status), create, approve, or receive. Receiving a PO updates inventory stock levels.";
    public override string Category => "supply_chain";
    public override JsonObject Schema => new()
    {
        ["type"] = "object",
        ["properties"] = new JsonObject
        {
            ["action"] = new JsonObject { ["type"] = "string", ["description"] = "list, create, approve, or receive" },
            ["status"] = new JsonObject { ["type"] = "string", ["description"] = "Filter by status for list action: Draft, Approved, Received" },
            ["supplierId"] = new JsonObject { ["type"] = "integer" },
            ["poId"] = new JsonObject { ["type"] = "integer" },
            ["approver"] = new JsonObject { ["type"] = "string" }
        },
        ["required"] = new JsonArray { "action" }
    };

    public override async Task<ToolResult> InvokeAsync(JsonObject? arguments, ToolExecutionContext context, CancellationToken ct = default)
    {
        var action = GetString(arguments, "action");

        if (action == "list")
        {
            var status = GetString(arguments, "status");
            var pos = await _scmService.GetPurchaseOrdersAsync(status, ct);
            return ToolResult.Ok(Serialize(new { count = pos.Count, purchaseOrders = pos }));
        }
        else if (action == "approve")
        {
            var poId = GetInt(arguments, "poId");
            var approver = GetString(arguments, "approver") ?? context.Username;
            var success = await _scmService.ApprovePurchaseOrderAsync(poId, approver, ct);
            if (!success) return ToolResult.Fail("PO approval failed. Verify the PO exists and is in Draft status.");
            return ToolResult.Ok(Serialize(new { success = true, message = "Purchase order approved." }));
        }
        else if (action == "receive")
        {
            var poId = GetInt(arguments, "poId");
            var success = await _scmService.ReceivePurchaseOrderAsync(poId, ct);
            if (!success) return ToolResult.Fail("PO receiving failed. Verify the PO exists and is in Approved status.");
            return ToolResult.Ok(Serialize(new { success = true, message = "Purchase order received. Inventory levels updated." }));
        }

        return ToolResult.Fail("Invalid action. Use: list, approve, or receive. For creating POs, use the REST API.");
    }
}

/// <summary>
/// Tool for querying MRP (Material Requirements Planning) recommendations.
/// </summary>
public class GetMrpRecommendationsTool : BaseTool
{
    private readonly IScmService _scmService;
    public GetMrpRecommendationsTool(IScmService scmService) => _scmService = scmService;

    public override string Name => "get_mrp_recommendations";
    public override string Description => "Get Material Requirements Planning (MRP) recommendations. Calculates available stock (on-hand minus reservations), identifies materials below reorder point, and suggests reorder quantities with supplier lead times.";
    public override string Category => "supply_chain";
    public override JsonObject Schema => new() { ["type"] = "object", ["properties"] = new JsonObject() };

    public override async Task<ToolResult> InvokeAsync(JsonObject? arguments, ToolExecutionContext context, CancellationToken ct = default)
    {
        var recommendations = await _scmService.GetMrpRecommendationsAsync(ct);
        var reorderCount = recommendations.Count(r => r.NeedsReorder);
        return ToolResult.Ok(Serialize(new
        {
            totalMaterials = recommendations.Count,
            materialsNeedingReorder = reorderCount,
            recommendations
        }));
    }
}