using MicroForge.Domain.Entities;
using MicroForge.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MicroForge.Application.Services;

/// <summary>
/// Supply Chain Management service — inventory, suppliers, purchase orders, MRP, and material reservations.
/// </summary>
public class ScmService : IScmService
{
    private readonly IRepository<Supplier> _supplierRepo;
    private readonly IRepository<Material> _materialRepo;
    private readonly IRepository<PurchaseOrder> _poRepo;
    private readonly IRepository<PurchaseOrderLine> _poLineRepo;
    private readonly IRepository<MaterialReservation> _reservationRepo;
    private readonly IRepository<Lot> _lotRepo;

    public ScmService(
        IRepository<Supplier> supplierRepo,
        IRepository<Material> materialRepo,
        IRepository<PurchaseOrder> poRepo,
        IRepository<PurchaseOrderLine> poLineRepo,
        IRepository<MaterialReservation> reservationRepo,
        IRepository<Lot> lotRepo)
    {
        _supplierRepo = supplierRepo;
        _materialRepo = materialRepo;
        _poRepo = poRepo;
        _poLineRepo = poLineRepo;
        _reservationRepo = reservationRepo;
        _lotRepo = lotRepo;
    }

    // ── Suppliers ──

    public async Task<List<SupplierDto>> GetSuppliersAsync(CancellationToken ct = default)
    {
        var suppliers = await _supplierRepo.Query()
            .OrderByDescending(s => s.IsActive)
            .ThenBy(s => s.Name)
            .Select(s => new SupplierDto
            {
                Id = s.Id,
                Name = s.Name,
                ContactEmail = s.ContactEmail,
                ContactPhone = s.ContactPhone,
                LeadTimeDays = s.LeadTimeDays,
                Rating = s.Rating,
                IsActive = s.IsActive
            })
            .ToListAsync(ct);

        if (suppliers.Count == 0)
        {
            await SeedDefaultSuppliersAsync(ct);
            suppliers = await _supplierRepo.Query()
                .OrderBy(s => s.Name)
                .Select(s => new SupplierDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    ContactEmail = s.ContactEmail,
                    ContactPhone = s.ContactPhone,
                    LeadTimeDays = s.LeadTimeDays,
                    Rating = s.Rating,
                    IsActive = s.IsActive
                })
                .ToListAsync(ct);
        }

        return suppliers;
    }

    public async Task<SupplierDto> CreateSupplierAsync(string name, string email, string phone, int leadTimeDays, string rating, CancellationToken ct = default)
    {
        var supplier = new Supplier
        {
            Name = name,
            ContactEmail = email,
            ContactPhone = phone,
            LeadTimeDays = leadTimeDays,
            Rating = rating,
            IsActive = true
        };
        await _supplierRepo.AddAsync(supplier, ct);
        await _supplierRepo.SaveChangesAsync(ct);

        return new SupplierDto
        {
            Id = supplier.Id,
            Name = supplier.Name,
            ContactEmail = supplier.ContactEmail,
            ContactPhone = supplier.ContactPhone,
            LeadTimeDays = supplier.LeadTimeDays,
            Rating = supplier.Rating,
            IsActive = supplier.IsActive
        };
    }

    // ── Materials / Inventory ──

    public async Task<List<MaterialDto>> GetMaterialsAsync(string? category, bool belowReorderOnly, CancellationToken ct = default)
    {
        var query = _materialRepo.Query()
            .Include(m => m.Supplier)
            .Include(m => m.Reservations)
            .AsQueryable();

        if (!string.IsNullOrEmpty(category) && Enum.TryParse<MaterialCategory>(category, true, out var cat))
        {
            query = query.Where(m => m.Category == cat);
        }

        var materials = await query.OrderBy(m => m.Name).ToListAsync(ct);

        if (materials.Count == 0)
        {
            await SeedDefaultMaterialsAsync(ct);
            materials = await _materialRepo.Query()
                .Include(m => m.Supplier)
                .Include(m => m.Reservations)
                .OrderBy(m => m.Name)
                .ToListAsync(ct);
        }

        var dtos = materials.Select(m => MapMaterialToDto(m)).ToList();

        if (belowReorderOnly)
        {
            dtos = dtos.Where(d => d.QuantityOnHand <= d.ReorderPoint).ToList();
        }

        return dtos;
    }

    public async Task<MaterialDto> CreateMaterialAsync(string partNumber, string name, string category, string unit,
        decimal qtyOnHand, decimal reorderPoint, decimal reorderQty, decimal unitCost, int? supplierId, CancellationToken ct = default)
    {
        Enum.TryParse<MaterialCategory>(category, true, out var cat);

        var material = new Material
        {
            PartNumber = partNumber,
            Name = name,
            Category = cat,
            Unit = unit,
            QuantityOnHand = qtyOnHand,
            ReorderPoint = reorderPoint,
            ReorderQuantity = reorderQty,
            UnitCost = unitCost,
            SupplierId = supplierId
        };
        await _materialRepo.AddAsync(material, ct);
        await _materialRepo.SaveChangesAsync(ct);

        return MapMaterialToDto(material);
    }

    public async Task<bool> AdjustInventoryAsync(int materialId, decimal delta, CancellationToken ct = default)
    {
        var material = await _materialRepo.GetByIdAsync(materialId, ct);
        if (material == null) return false;

        material.QuantityOnHand += delta;
        if (delta > 0)
        {
            material.LastReceivedAt = DateTime.UtcNow;
        }
        _materialRepo.Update(material);
        await _materialRepo.SaveChangesAsync(ct);
        return true;
    }

    // ── Purchase Orders ──

    public async Task<List<PurchaseOrderDto>> GetPurchaseOrdersAsync(string? status, CancellationToken ct = default)
    {
        var query = _poRepo.Query()
            .Include(po => po.Supplier)
            .Include(po => po.Lines)
                .ThenInclude(l => l.Material)
            .AsQueryable();

        if (!string.IsNullOrEmpty(status))
        {
            query = query.Where(po => po.Status == status);
        }

        var pos = await query.OrderByDescending(po => po.OrderedAt).ToListAsync(ct);
        return pos.Select(po => MapPoToDto(po)).ToList();
    }

    public async Task<PurchaseOrderDto> CreatePurchaseOrderAsync(int supplierId, string createdBy,
        List<(int materialId, decimal quantity, decimal unitCost)> lines, CancellationToken ct = default)
    {
        var supplier = await _supplierRepo.GetByIdAsync(supplierId, ct);
        if (supplier == null)
        {
            throw new ArgumentException($"Supplier {supplierId} not found");
        }

        var poNumber = $"PO-{DateTime.UtcNow:yyyyMMddHHmmss}";
        var po = new PurchaseOrder
        {
            PONumber = poNumber,
            SupplierId = supplierId,
            Status = "Draft",
            CreatedBy = createdBy,
            OrderedAt = DateTime.UtcNow,
            ExpectedAt = DateTime.UtcNow.AddDays(supplier.LeadTimeDays)
        };
        await _poRepo.AddAsync(po, ct);
        await _poRepo.SaveChangesAsync(ct);

        decimal total = 0;
        foreach (var line in lines)
        {
            var poLine = new PurchaseOrderLine
            {
                PurchaseOrderId = po.Id,
                MaterialId = line.materialId,
                QuantityOrdered = line.quantity,
                UnitCost = line.unitCost
            };
            await _poLineRepo.AddAsync(poLine, ct);
            total += line.quantity * line.unitCost;
        }
        await _poLineRepo.SaveChangesAsync(ct);

        po.TotalAmount = total;
        _poRepo.Update(po);
        await _poRepo.SaveChangesAsync(ct);

        var loadedPo = await _poRepo.Query()
            .Include(p => p.Supplier)
            .Include(p => p.Lines)
                .ThenInclude(l => l.Material)
            .FirstOrDefaultAsync(p => p.Id == po.Id, ct);

        return MapPoToDto(loadedPo!);
    }

    public async Task<bool> ApprovePurchaseOrderAsync(int poId, string approver, CancellationToken ct = default)
    {
        var po = await _poRepo.GetByIdAsync(poId, ct);
        if (po == null || po.Status != "Draft") return false;

        po.Status = "Approved";
        po.ApprovedBy = approver;
        _poRepo.Update(po);
        await _poRepo.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> ReceivePurchaseOrderAsync(int poId, CancellationToken ct = default)
    {
        var po = await _poRepo.Query()
            .Include(p => p.Lines)
            .FirstOrDefaultAsync(p => p.Id == poId, ct);

        if (po == null || po.Status != "Approved") return false;

        po.Status = "Received";
        po.ReceivedAt = DateTime.UtcNow;

        foreach (var line in po.Lines)
        {
            line.QuantityReceived = line.QuantityOrdered;
            _poLineRepo.Update(line);

            var material = await _materialRepo.GetByIdAsync(line.MaterialId, ct);
            if (material != null)
            {
                material.QuantityOnHand += line.QuantityOrdered;
                material.LastReceivedAt = DateTime.UtcNow;
                _materialRepo.Update(material);
            }
        }

        _poRepo.Update(po);
        await _poRepo.SaveChangesAsync(ct);
        await _poLineRepo.SaveChangesAsync(ct);
        await _materialRepo.SaveChangesAsync(ct);
        return true;
    }

    // ── MRP ──

    public async Task<List<MrpRecommendationDto>> GetMrpRecommendationsAsync(CancellationToken ct = default)
    {
        var materials = await _materialRepo.Query()
            .Include(m => m.Supplier)
            .Include(m => m.Reservations)
            .OrderBy(m => m.Name)
            .ToListAsync(ct);

        if (materials.Count == 0)
        {
            await SeedDefaultMaterialsAsync(ct);
            materials = await _materialRepo.Query()
                .Include(m => m.Supplier)
                .Include(m => m.Reservations)
                .OrderBy(m => m.Name)
                .ToListAsync(ct);
        }

        var recommendations = new List<MrpRecommendationDto>();

        foreach (var m in materials)
        {
            var reservedQty = m.Reservations.Where(r => r.Status == "Reserved").Sum(r => r.QuantityReserved);
            var available = m.QuantityOnHand - reservedQty;
            var needsReorder = available <= m.ReorderPoint;

            string recommendation;
            if (needsReorder)
            {
                recommendation = $"REORDER: Available stock ({available} {m.Unit}) is at or below reorder point ({m.ReorderPoint} {m.Unit}). Suggested order: {m.ReorderQuantity} {m.Unit}.";
            }
            else if (available <= m.ReorderPoint * 1.5m)
            {
                recommendation = $"WATCH: Available stock ({available} {m.Unit}) is approaching reorder point. Monitor closely.";
            }
            else
            {
                recommendation = $"OK: Available stock ({available} {m.Unit}) is well above reorder point ({m.ReorderPoint} {m.Unit}).";
            }

            recommendations.Add(new MrpRecommendationDto
            {
                MaterialId = m.Id,
                PartNumber = m.PartNumber,
                MaterialName = m.Name,
                QuantityOnHand = m.QuantityOnHand,
                ReservedQuantity = reservedQty,
                AvailableQuantity = available,
                ReorderPoint = m.ReorderPoint,
                NeedsReorder = needsReorder,
                SuggestedOrderQuantity = needsReorder ? m.ReorderQuantity : 0,
                LeadTimeDays = m.Supplier?.LeadTimeDays ?? 0,
                Recommendation = recommendation
            });
        }

        return recommendations;
    }

    // ── Reservations ──

    public async Task<List<MaterialReservationDto>> GetReservationsAsync(int? lotId, CancellationToken ct = default)
    {
        var query = _reservationRepo.Query()
            .Include(r => r.Material)
            .Include(r => r.Lot)
            .AsQueryable();

        if (lotId.HasValue)
        {
            query = query.Where(r => r.LotId == lotId.Value);
        }

        var reservations = await query.OrderByDescending(r => r.ReservedAt).ToListAsync(ct);
        return reservations.Select(r => new MaterialReservationDto
        {
            Id = r.Id,
            MaterialId = r.MaterialId,
            MaterialName = r.Material?.Name ?? "Unknown",
            LotId = r.LotId,
            LotNumber = r.Lot?.LotNumber ?? "N/A",
            QuantityReserved = r.QuantityReserved,
            Status = r.Status,
            RouteStepName = r.RouteStepName,
            ReservedAt = r.ReservedAt
        }).ToList();
    }

    public async Task<MaterialReservationDto> CreateReservationAsync(int materialId, int? lotId, decimal quantity, string routeStepName, CancellationToken ct = default)
    {
        var material = await _materialRepo.GetByIdAsync(materialId, ct);
        if (material == null)
        {
            throw new ArgumentException($"Material {materialId} not found");
        }

        var reservation = new MaterialReservation
        {
            MaterialId = materialId,
            LotId = lotId,
            QuantityReserved = quantity,
            RouteStepName = routeStepName,
            Status = "Reserved",
            ReservedAt = DateTime.UtcNow
        };
        await _reservationRepo.AddAsync(reservation, ct);
        await _reservationRepo.SaveChangesAsync(ct);

        string lotNumber = "N/A";
        if (lotId.HasValue)
        {
            var lot = await _lotRepo.GetByIdAsync(lotId.Value, ct);
            lotNumber = lot?.LotNumber ?? "N/A";
        }

        return new MaterialReservationDto
        {
            Id = reservation.Id,
            MaterialId = materialId,
            MaterialName = material.Name,
            LotId = lotId,
            LotNumber = lotNumber,
            QuantityReserved = quantity,
            Status = reservation.Status,
            RouteStepName = routeStepName,
            ReservedAt = reservation.ReservedAt
        };
    }

    public async Task<bool> ReleaseReservationAsync(int reservationId, CancellationToken ct = default)
    {
        var reservation = await _reservationRepo.GetByIdAsync(reservationId, ct);
        if (reservation == null || reservation.Status != "Reserved") return false;

        reservation.Status = "Released";
        reservation.ReleasedAt = DateTime.UtcNow;
        _reservationRepo.Update(reservation);
        await _reservationRepo.SaveChangesAsync(ct);
        return true;
    }

    // ── Private helpers ──

    private static MaterialDto MapMaterialToDto(Material m)
    {
        var activeRes = m.Reservations?.Count(r => r.Status == "Reserved") ?? 0;
        var available = m.QuantityOnHand - (m.Reservations?.Where(r => r.Status == "Reserved").Sum(r => r.QuantityReserved) ?? 0);

        return new MaterialDto
        {
            Id = m.Id,
            PartNumber = m.PartNumber,
            Name = m.Name,
            Category = m.Category.ToString(),
            Unit = m.Unit,
            QuantityOnHand = m.QuantityOnHand,
            ReorderPoint = m.ReorderPoint,
            ReorderQuantity = m.ReorderQuantity,
            UnitCost = m.UnitCost,
            SupplierId = m.SupplierId,
            SupplierName = m.Supplier?.Name ?? "Unassigned",
            Status = available <= m.ReorderPoint ? "Reorder" : "OK",
            ActiveReservations = activeRes
        };
    }

    private static PurchaseOrderDto MapPoToDto(PurchaseOrder po)
    {
        return new PurchaseOrderDto
        {
            Id = po.Id,
            PONumber = po.PONumber,
            SupplierId = po.SupplierId,
            SupplierName = po.Supplier?.Name ?? "Unknown",
            Status = po.Status,
            TotalAmount = po.TotalAmount,
            CreatedBy = po.CreatedBy,
            ApprovedBy = po.ApprovedBy,
            OrderedAt = po.OrderedAt,
            ExpectedAt = po.ExpectedAt,
            ReceivedAt = po.ReceivedAt,
            Lines = po.Lines?.Select(l => new PurchaseOrderLineDto
            {
                Id = l.Id,
                MaterialId = l.MaterialId,
                MaterialName = l.Material?.Name ?? "Unknown",
                PartNumber = l.Material?.PartNumber ?? "N/A",
                QuantityOrdered = l.QuantityOrdered,
                QuantityReceived = l.QuantityReceived,
                UnitCost = l.UnitCost,
                LineTotal = l.LineTotal
            }).ToList() ?? new()
        };
    }

    private async Task SeedDefaultSuppliersAsync(CancellationToken ct)
    {
        var defaults = new List<Supplier>
        {
            new() { Name = "Silicon Wafers Inc.", ContactEmail = "sales@siliconwafers.com", ContactPhone = "+1-555-0101", LeadTimeDays = 14, Rating = "A" },
            new() { Name = "ChemTech Supplies", ContactEmail = "orders@chemtech.com", ContactPhone = "+1-555-0102", LeadTimeDays = 7, Rating = "A" },
            new() { Name = "PrecisionParts Ltd.", ContactEmail = "contact@precisionparts.com", ContactPhone = "+1-555-0103", LeadTimeDays = 21, Rating = "B" },
            new() { Name = "GasPro Industrial", ContactEmail = "supply@gaspro.com", ContactPhone = "+1-555-0104", LeadTimeDays = 5, Rating = "A" }
        };

        foreach (var s in defaults)
        {
            await _supplierRepo.AddAsync(s, ct);
        }
        await _supplierRepo.SaveChangesAsync(ct);
    }

    private async Task SeedDefaultMaterialsAsync(CancellationToken ct)
    {
        var suppliers = await _supplierRepo.ListAsync(ct);
        if (suppliers.Count == 0)
        {
            await SeedDefaultSuppliersAsync(ct);
            suppliers = await _supplierRepo.ListAsync(ct);
        }

        var waferSupplier = suppliers.FirstOrDefault(s => s.Name.Contains("Silicon"));
        var chemSupplier = suppliers.FirstOrDefault(s => s.Name.Contains("Chem"));
        var partsSupplier = suppliers.FirstOrDefault(s => s.Name.Contains("Precision"));
        var gasSupplier = suppliers.FirstOrDefault(s => s.Name.Contains("Gas"));

        var defaults = new List<Material>
        {
            new()
            {
                PartNumber = "WAF-300-001", Name = "300mm Bare Silicon Wafer", Category = MaterialCategory.RawWafer,
                Unit = "ea", QuantityOnHand = 450, ReorderPoint = 200, ReorderQuantity = 500, UnitCost = 85.00m,
                SupplierId = waferSupplier?.Id
            },
            new()
            {
                PartNumber = "CHEM-PHOTORESIST-01", Name = "Photoresist PR-405", Category = MaterialCategory.Chemical,
                Unit = "L", QuantityOnHand = 35, ReorderPoint = 50, ReorderQuantity = 100, UnitCost = 320.00m,
                SupplierId = chemSupplier?.Id
            },
            new()
            {
                PartNumber = "CHEM-DEVELOPER-02", Name = "Developer Solution DEV-200", Category = MaterialCategory.Chemical,
                Unit = "L", QuantityOnHand = 80, ReorderPoint = 40, ReorderQuantity = 120, UnitCost = 45.00m,
                SupplierId = chemSupplier?.Id
            },
            new()
            {
                PartNumber = "GAS-CF4-01", Name = "CF4 Etch Gas", Category = MaterialCategory.Gas,
                Unit = "kg", QuantityOnHand = 12, ReorderPoint = 15, ReorderQuantity = 50, UnitCost = 120.00m,
                SupplierId = gasSupplier?.Id
            },
            new()
            {
                PartNumber = "GAS-AR-01", Name = "Argon Gas", Category = MaterialCategory.Gas,
                Unit = "kg", QuantityOnHand = 60, ReorderPoint = 30, ReorderQuantity = 100, UnitCost = 8.50m,
                SupplierId = gasSupplier?.Id
            },
            new()
            {
                PartNumber = "PART-FOCUS-RING", Name = "Chamber Focus Ring", Category = MaterialCategory.SparePart,
                Unit = "ea", QuantityOnHand = 8, ReorderPoint = 5, ReorderQuantity = 10, UnitCost = 1850.00m,
                SupplierId = partsSupplier?.Id
            },
            new()
            {
                PartNumber = "PART-SHOWERHEAD", Name = "Showerhead Assembly", Category = MaterialCategory.SparePart,
                Unit = "ea", QuantityOnHand = 3, ReorderPoint = 4, ReorderQuantity = 6, UnitCost = 4200.00m,
                SupplierId = partsSupplier?.Id
            },
            new()
            {
                PartNumber = "CONS-CLEAN-WIPES", Name = "Cleanroom Wipes", Category = MaterialCategory.Consumable,
                Unit = "box", QuantityOnHand = 120, ReorderPoint = 50, ReorderQuantity = 200, UnitCost = 35.00m,
                SupplierId = chemSupplier?.Id
            }
        };

        foreach (var m in defaults)
        {
            await _materialRepo.AddAsync(m, ct);
        }
        await _materialRepo.SaveChangesAsync(ct);
    }
}