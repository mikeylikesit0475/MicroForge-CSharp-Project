using MicroForge.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace MicroForge.Web.Controllers;

/// <summary>
/// Supply Chain Management endpoints — inventory, suppliers, purchase orders, MRP, and material reservations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ScmController : ControllerBase
{
    private readonly IScmService _scmService;
    private readonly ILogger<ScmController> _logger;

    public ScmController(IScmService scmService, ILogger<ScmController> logger)
    {
        _scmService = scmService;
        _logger = logger;
    }

    // ── Suppliers ──

    [HttpGet("suppliers")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSuppliers(CancellationToken ct)
    {
        var suppliers = await _scmService.GetSuppliersAsync(ct);
        return Ok(suppliers);
    }

    [HttpPost("suppliers/create")]
    [Authorize(Roles = "Admin,Engineer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateSupplier(
        [FromQuery] string name,
        [FromQuery] string email,
        [FromQuery] string phone,
        [FromQuery] int leadTimeDays,
        [FromQuery] string rating,
        CancellationToken ct)
    {
        var supplier = await _scmService.CreateSupplierAsync(name, email, phone, leadTimeDays, rating, ct);
        return Ok(supplier);
    }

    // ── Materials / Inventory ──

    [HttpGet("materials")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMaterials(
        [FromQuery] string? category,
        [FromQuery] bool belowReorderOnly,
        CancellationToken ct)
    {
        var materials = await _scmService.GetMaterialsAsync(category, belowReorderOnly, ct);
        return Ok(materials);
    }

    [HttpPost("materials/create")]
    [Authorize(Roles = "Admin,Engineer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateMaterial(
        [FromQuery] string partNumber,
        [FromQuery] string name,
        [FromQuery] string category,
        [FromQuery] string unit,
        [FromQuery] decimal qtyOnHand,
        [FromQuery] decimal reorderPoint,
        [FromQuery] decimal reorderQty,
        [FromQuery] decimal unitCost,
        [FromQuery] int? supplierId,
        CancellationToken ct)
    {
        var material = await _scmService.CreateMaterialAsync(partNumber, name, category, unit, qtyOnHand, reorderPoint, reorderQty, unitCost, supplierId, ct);
        return Ok(material);
    }

    [HttpPost("materials/{id:int}/adjust")]
    [Authorize(Roles = "Admin,Engineer,Operator")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AdjustInventory(int id, [FromQuery] decimal delta, CancellationToken ct)
    {
        var success = await _scmService.AdjustInventoryAsync(id, delta, ct);
        if (!success)
            return NotFound(new ProblemDetails
            {
                Title = "Material Not Found",
                Detail = $"Material ID {id} was not found",
                Status = StatusCodes.Status404NotFound
            });

        _logger.LogInformation("Inventory adjusted for material {MaterialId} by {Delta}", id, delta);
        return Ok(new { Message = "Inventory adjusted successfully" });
    }

    // ── Purchase Orders ──

    [HttpGet("purchase-orders")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPurchaseOrders([FromQuery] string? status, CancellationToken ct)
    {
        var pos = await _scmService.GetPurchaseOrdersAsync(status, ct);
        return Ok(pos);
    }

    [HttpPost("purchase-orders/create")]
    [Authorize(Roles = "Admin,Engineer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreatePurchaseOrder(
        [FromQuery] int supplierId,
        [FromBody] List<PoLineRequest> lines,
        CancellationToken ct)
    {
        if (lines == null || lines.Count == 0)
            return BadRequest(new ProblemDetails
            {
                Title = "No Lines",
                Detail = "At least one purchase order line is required",
                Status = StatusCodes.Status400BadRequest
            });

        var createdBy = GetUsername();
        var lineTuples = new List<(int materialId, decimal quantity, decimal unitCost)>();
        foreach (var l in lines)
        {
            lineTuples.Add((l.MaterialId, l.Quantity, l.UnitCost));
        }

        var po = await _scmService.CreatePurchaseOrderAsync(supplierId, createdBy, lineTuples, ct);
        _logger.LogInformation("Purchase order {PONumber} created by {User}", po.PONumber, createdBy);
        return Ok(po);
    }

    [HttpPost("purchase-orders/{id:int}/approve")]
    [Authorize(Roles = "Admin,Engineer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ApprovePurchaseOrder(int id, CancellationToken ct)
    {
        var approver = GetUsername();
        var success = await _scmService.ApprovePurchaseOrderAsync(id, approver, ct);
        if (!success)
            return BadRequest(new ProblemDetails
            {
                Title = "Approval Failed",
                Detail = "Could not approve PO. Verify it exists and is in Draft status.",
                Status = StatusCodes.Status400BadRequest
            });

        return Ok(new { Message = "Purchase order approved" });
    }

    [HttpPost("purchase-orders/{id:int}/receive")]
    [Authorize(Roles = "Admin,Engineer,Operator")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ReceivePurchaseOrder(int id, CancellationToken ct)
    {
        var success = await _scmService.ReceivePurchaseOrderAsync(id, ct);
        if (!success)
            return BadRequest(new ProblemDetails
            {
                Title = "Receiving Failed",
                Detail = "Could not receive PO. Verify it exists and is in Approved status.",
                Status = StatusCodes.Status400BadRequest
            });

        _logger.LogInformation("Purchase order {PoId} received. Inventory updated.", id);
        return Ok(new { Message = "Purchase order received. Inventory levels updated." });
    }

    // ── MRP ──

    [HttpGet("mrp")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMrpRecommendations(CancellationToken ct)
    {
        var recommendations = await _scmService.GetMrpRecommendationsAsync(ct);
        return Ok(recommendations);
    }

    // ── Reservations ──

    [HttpGet("reservations")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetReservations([FromQuery] int? lotId, CancellationToken ct)
    {
        var reservations = await _scmService.GetReservationsAsync(lotId, ct);
        return Ok(reservations);
    }

    [HttpPost("reservations/create")]
    [Authorize(Roles = "Admin,Engineer,Operator")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateReservation(
        [FromQuery] int materialId,
        [FromQuery] int? lotId,
        [FromQuery] decimal quantity,
        [FromQuery] string routeStepName,
        CancellationToken ct)
    {
        var reservation = await _scmService.CreateReservationAsync(materialId, lotId, quantity, routeStepName, ct);
        return Ok(reservation);
    }

    [HttpPost("reservations/{id:int}/release")]
    [Authorize(Roles = "Admin,Engineer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ReleaseReservation(int id, CancellationToken ct)
    {
        var success = await _scmService.ReleaseReservationAsync(id, ct);
        if (!success)
            return BadRequest(new ProblemDetails
            {
                Title = "Release Failed",
                Detail = "Could not release reservation. Verify it exists and is in Reserved status.",
                Status = StatusCodes.Status400BadRequest
            });

        return Ok(new { Message = "Reservation released" });
    }

    private string GetUsername()
    {
        return User.FindFirst("sub")?.Value
            ?? User.FindFirst(ClaimTypes.Name)?.Value
            ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? "system";
    }

    public class PoLineRequest
    {
        public int MaterialId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitCost { get; set; }
    }
}