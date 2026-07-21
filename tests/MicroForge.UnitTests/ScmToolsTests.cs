using FluentAssertions;
using MicroForge.Application.Services;
using MicroForge.Domain.Entities;
using MicroForge.Domain.Interfaces;
using MicroForge.Infrastructure.AI.Tools;
using MicroForge.Infrastructure.Data;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MicroForge.UnitTests;

public class ScmToolsTests : TestBase
{
    private ScmService CreateScmService()
    {
        return new ScmService(
            new Repository<Supplier>(DbContext),
            new Repository<Material>(DbContext),
            new Repository<PurchaseOrder>(DbContext),
            new Repository<PurchaseOrderLine>(DbContext),
            new Repository<MaterialReservation>(DbContext),
            new Repository<Lot>(DbContext));
    }

    [Fact]
    public async Task query_inventory_tool_returns_materials()
    {
        var service = CreateScmService();
        var tool = new QueryInventoryTool(service);

        var result = await tool.InvokeAsync(null, new ToolExecutionContext { Username = "operator" }, CancellationToken.None);

        result.Success.Should().BeTrue();
        result.Output.Should().Contain("materials");
        result.Output.Should().Contain("WAF-300-001");
    }

    [Fact]
    public async Task query_inventory_tool_filters_below_reorder()
    {
        var service = CreateScmService();
        var tool = new QueryInventoryTool(service);

        var args = new JsonObject { ["belowReorderOnly"] = true };
        var result = await tool.InvokeAsync(args, new ToolExecutionContext { Username = "operator" }, CancellationToken.None);

        result.Success.Should().BeTrue();
        result.Output.Should().Contain("count");
    }

    [Fact]
    public async Task get_mrp_recommendations_tool_returns_reorder_flags()
    {
        var service = CreateScmService();
        var tool = new GetMrpRecommendationsTool(service);

        var result = await tool.InvokeAsync(null, new ToolExecutionContext { Username = "engineer" }, CancellationToken.None);

        result.Success.Should().BeTrue();
        result.Output.Should().Contain("materialsNeedingReorder");
        result.Output.Should().Contain("REORDER");
    }

    [Fact]
    public async Task manage_purchase_orders_tool_lists_pos()
    {
        var service = CreateScmService();
        var tool = new ManagePurchaseOrdersTool(service);

        var args = new JsonObject { ["action"] = "list" };
        var result = await tool.InvokeAsync(args, new ToolExecutionContext { Username = "admin" }, CancellationToken.None);

        result.Success.Should().BeTrue();
        result.Output.Should().Contain("purchaseOrders");
    }

    [Fact]
    public async Task manage_purchase_orders_tool_approves_po()
    {
        var service = CreateScmService();
        var suppliers = await service.GetSuppliersAsync(CancellationToken.None);
        var materials = await service.GetMaterialsAsync(null, false, CancellationToken.None);

        var lines = new System.Collections.Generic.List<(int, decimal, decimal)>
        {
            (materials.First().Id, 10, 50.00m)
        };
        var po = await service.CreatePurchaseOrderAsync(suppliers.First().Id, "test", lines, CancellationToken.None);

        var tool = new ManagePurchaseOrdersTool(service);
        var args = new JsonObject { ["action"] = "approve", ["poId"] = po.Id, ["approver"] = "admin_test" };
        var result = await tool.InvokeAsync(args, new ToolExecutionContext { Username = "admin" }, CancellationToken.None);

        result.Success.Should().BeTrue();
        result.Output.Should().Contain("approved");
    }

    [Fact]
    public async Task manage_purchase_orders_tool_receives_po_and_updates_inventory()
    {
        var service = CreateScmService();
        var suppliers = await service.GetSuppliersAsync(CancellationToken.None);
        var materials = await service.GetMaterialsAsync(null, false, CancellationToken.None);
        var mat = materials.First();
        var originalQty = mat.QuantityOnHand;

        var lines = new System.Collections.Generic.List<(int, decimal, decimal)>
        {
            (mat.Id, 30, 50.00m)
        };
        var po = await service.CreatePurchaseOrderAsync(suppliers.First().Id, "test", lines, CancellationToken.None);
        await service.ApprovePurchaseOrderAsync(po.Id, "admin", CancellationToken.None);

        var tool = new ManagePurchaseOrdersTool(service);
        var args = new JsonObject { ["action"] = "receive", ["poId"] = po.Id };
        var result = await tool.InvokeAsync(args, new ToolExecutionContext { Username = "operator" }, CancellationToken.None);

        result.Success.Should().BeTrue();
        result.Output.Should().Contain("received");

        var updatedMaterials = await service.GetMaterialsAsync(null, false, CancellationToken.None);
        var updatedMat = updatedMaterials.First(m => m.Id == mat.Id);
        updatedMat.QuantityOnHand.Should().Be(originalQty + 30);
    }
}