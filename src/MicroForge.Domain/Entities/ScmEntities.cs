using System;
using System.Collections.Generic;

namespace MicroForge.Domain.Entities;

/// <summary>
/// Material category classification for fab inventory items.
/// </summary>
public enum MaterialCategory
{
    RawWafer,
    Chemical,
    Gas,
    Consumable,
    SparePart
}

/// <summary>
/// Supplier record for procurement and lead-time tracking.
/// </summary>
public class Supplier
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ContactEmail { get; set; } = string.Empty;
    public string ContactPhone { get; set; } = string.Empty;
    public int LeadTimeDays { get; set; }
    public string Rating { get; set; } = "A";
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

/// <summary>
/// Inventory item representing a raw material, chemical, gas, consumable, or spare part.
/// </summary>
public class Material
{
    public int Id { get; set; }
    public string PartNumber { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public MaterialCategory Category { get; set; } = MaterialCategory.Consumable;
    public string Unit { get; set; } = "ea";
    public decimal QuantityOnHand { get; set; }
    public decimal ReorderPoint { get; set; }
    public decimal ReorderQuantity { get; set; }
    public decimal UnitCost { get; set; }
    public int? SupplierId { get; set; }
    public Supplier? Supplier { get; set; }
    public DateTime? LastReceivedAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<MaterialReservation> Reservations { get; set; } = new();
}

/// <summary>
/// Purchase order header for procurement tracking.
/// </summary>
public class PurchaseOrder
{
    public int Id { get; set; }
    public string PONumber { get; set; } = string.Empty;
    public int SupplierId { get; set; }
    public Supplier? Supplier { get; set; }
    public string Status { get; set; } = "Draft";
    public decimal TotalAmount { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public string ApprovedBy { get; set; } = string.Empty;
    public DateTime OrderedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ExpectedAt { get; set; }
    public DateTime? ReceivedAt { get; set; }

    public List<PurchaseOrderLine> Lines { get; set; } = new();
}

/// <summary>
/// Individual line item on a purchase order.
/// </summary>
public class PurchaseOrderLine
{
    public int Id { get; set; }
    public int PurchaseOrderId { get; set; }
    public PurchaseOrder? PurchaseOrder { get; set; }
    public int MaterialId { get; set; }
    public Material? Material { get; set; }
    public decimal QuantityOrdered { get; set; }
    public decimal QuantityReceived { get; set; }
    public decimal UnitCost { get; set; }
    public decimal LineTotal => QuantityOrdered * UnitCost;
}

/// <summary>
/// Reservation linking materials to WIP lots for MRP consumption tracking.
/// </summary>
public class MaterialReservation
{
    public int Id { get; set; }
    public int MaterialId { get; set; }
    public Material? Material { get; set; }
    public int? LotId { get; set; }
    public Lot? Lot { get; set; }
    public decimal QuantityReserved { get; set; }
    public string Status { get; set; } = "Reserved";
    public string RouteStepName { get; set; } = string.Empty;
    public DateTime ReservedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ReleasedAt { get; set; }
}