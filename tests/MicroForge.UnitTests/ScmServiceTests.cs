using FluentAssertions;
using MicroForge.Application.Services;
using MicroForge.Domain.Entities;
using MicroForge.Domain.Interfaces;
using MicroForge.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MicroForge.UnitTests;

public class ScmServiceTests : TestBase
{
    private ScmService CreateService()
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
    public async Task get_suppliers_seeds_defaults_when_empty()
    {
        var service = CreateService();

        var suppliers = await service.GetSuppliersAsync(CancellationToken.None);

        suppliers.Should().NotBeEmpty();
        suppliers.Should().Contain(s => s.Name == "Silicon Wafers Inc.");
        suppliers.Should().Contain(s => s.Name == "ChemTech Supplies");
    }

    [Fact]
    public async Task get_materials_seeds_defaults_when_empty()
    {
        var service = CreateService();

        var materials = await service.GetMaterialsAsync(null, false, CancellationToken.None);

        materials.Should().NotBeEmpty();
        materials.Should().Contain(m => m.PartNumber == "WAF-300-001");
        materials.Should().Contain(m => m.PartNumber == "CHEM-PHOTORESIST-01");
    }

    [Fact]
    public async Task create_material_persists_and_returns_dto()
    {
        var service = CreateService();
        await service.GetSuppliersAsync(CancellationToken.None); // seed suppliers

        var dto = await service.CreateMaterialAsync(
            "TEST-001", "Test Material", "Consumable", "ea",
            100, 20, 50, 10.50m, 1, CancellationToken.None);

        dto.PartNumber.Should().Be("TEST-001");
        dto.Name.Should().Be("Test Material");
        dto.QuantityOnHand.Should().Be(100);
        dto.Status.Should().Be("OK");
    }

    [Fact]
    public async Task adjust_inventory_updates_quantity_on_hand()
    {
        var service = CreateService();
        var materials = await service.GetMaterialsAsync(null, false, CancellationToken.None);
        var first = materials.First();

        var success = await service.AdjustInventoryAsync(first.Id, 50, CancellationToken.None);

        success.Should().BeTrue();
        var updated = await service.GetMaterialsAsync(null, false, CancellationToken.None);
        var mat = updated.First(m => m.Id == first.Id);
        mat.QuantityOnHand.Should().Be(first.QuantityOnHand + 50);
    }

    [Fact]
    public async Task create_purchase_order_calculates_total_and_persists_lines()
    {
        var service = CreateService();
        var suppliers = await service.GetSuppliersAsync(CancellationToken.None);
        var materials = await service.GetMaterialsAsync(null, false, CancellationToken.None);

        var supplierId = suppliers.First().Id;
        var mat1 = materials.First();
        var mat2 = materials.Skip(1).First();

        var lines = new List<(int materialId, decimal quantity, decimal unitCost)>
        {
            (mat1.Id, 10, 50.00m),
            (mat2.Id, 5, 100.00m)
        };

        var po = await service.CreatePurchaseOrderAsync(supplierId, "test_user", lines, CancellationToken.None);

        po.PONumber.Should().StartWith("PO-");
        po.Status.Should().Be("Draft");
        po.TotalAmount.Should().Be(1000.00m);
        po.Lines.Should().HaveCount(2);
        po.Lines[0].QuantityOrdered.Should().Be(10);
        po.Lines[1].LineTotal.Should().Be(500.00m);
    }

    [Fact]
    public async Task approve_purchase_order_changes_status_to_approved()
    {
        var service = CreateService();
        var suppliers = await service.GetSuppliersAsync(CancellationToken.None);
        var materials = await service.GetMaterialsAsync(null, false, CancellationToken.None);

        var lines = new List<(int materialId, decimal quantity, decimal unitCost)>
        {
            (materials.First().Id, 10, 50.00m)
        };
        var po = await service.CreatePurchaseOrderAsync(suppliers.First().Id, "test_user", lines, CancellationToken.None);

        var success = await service.ApprovePurchaseOrderAsync(po.Id, "admin_user", CancellationToken.None);

        success.Should().BeTrue();
        var pos = await service.GetPurchaseOrdersAsync(null, CancellationToken.None);
        var approved = pos.First(p => p.Id == po.Id);
        approved.Status.Should().Be("Approved");
        approved.ApprovedBy.Should().Be("admin_user");
    }

    [Fact]
    public async Task receive_purchase_order_updates_inventory()
    {
        var service = CreateService();
        var suppliers = await service.GetSuppliersAsync(CancellationToken.None);
        var materials = await service.GetMaterialsAsync(null, false, CancellationToken.None);
        var mat = materials.First();
        var originalQty = mat.QuantityOnHand;

        var lines = new List<(int materialId, decimal quantity, decimal unitCost)>
        {
            (mat.Id, 25, 50.00m)
        };
        var po = await service.CreatePurchaseOrderAsync(suppliers.First().Id, "test_user", lines, CancellationToken.None);
        await service.ApprovePurchaseOrderAsync(po.Id, "admin", CancellationToken.None);

        var success = await service.ReceivePurchaseOrderAsync(po.Id, CancellationToken.None);

        success.Should().BeTrue();
        var updatedMaterials = await service.GetMaterialsAsync(null, false, CancellationToken.None);
        var updatedMat = updatedMaterials.First(m => m.Id == mat.Id);
        updatedMat.QuantityOnHand.Should().Be(originalQty + 25);
    }

    [Fact]
    public async Task mrp_recommendations_identify_materials_below_reorder()
    {
        var service = CreateService();
        var materials = await service.GetMaterialsAsync(null, false, CancellationToken.None);

        var recs = await service.GetMrpRecommendationsAsync(CancellationToken.None);

        recs.Should().NotBeEmpty();
        var photoresist = recs.FirstOrDefault(r => r.PartNumber == "CHEM-PHOTORESIST-01");
        photoresist.Should().NotBeNull();
        photoresist!.NeedsReorder.Should().BeTrue();
        photoresist.SuggestedOrderQuantity.Should().BeGreaterThan(0);
        photoresist.Recommendation.Should().Contain("REORDER");
    }

    [Fact]
    public async Task create_and_release_reservation_lifecycle()
    {
        var service = CreateService();
        var materials = await service.GetMaterialsAsync(null, false, CancellationToken.None);
        var mat = materials.First();

        var reservation = await service.CreateReservationAsync(mat.Id, null, 10, "LITHO", CancellationToken.None);

        reservation.Status.Should().Be("Reserved");
        reservation.QuantityReserved.Should().Be(10);
        reservation.MaterialName.Should().Be(mat.Name);

        var success = await service.ReleaseReservationAsync(reservation.Id, CancellationToken.None);
        success.Should().BeTrue();

        var reservations = await service.GetReservationsAsync(null, CancellationToken.None);
        var released = reservations.First(r => r.Id == reservation.Id);
        released.Status.Should().Be("Released");
    }

    [Fact]
    public async Task below_reorder_filter_returns_only_low_stock()
    {
        var service = CreateService();
        var all = await service.GetMaterialsAsync(null, false, CancellationToken.None);
        var belowReorder = await service.GetMaterialsAsync(null, true, CancellationToken.None);

        belowReorder.Should().NotBeEmpty();
        belowReorder.Should().OnlyContain(m => m.QuantityOnHand <= m.ReorderPoint);
    }
}