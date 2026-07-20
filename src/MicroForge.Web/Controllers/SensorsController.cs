using MicroForge.Application.Dtos;
using MicroForge.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MicroForge.Web.Controllers;

/// <summary>
/// Sensor reading and anomaly endpoints.
/// </summary>
[ApiController]
[Route("api/sensors")]
[Authorize]
public class SensorsController : ControllerBase
{
    private readonly IProductionService _productionService;

    public SensorsController(IProductionService productionService)
    {
        _productionService = productionService;
    }

    /// <summary>
    /// Query historical sensor readings for a production line.
    /// </summary>
    /// <param name="lineId">Production line ID.</param>
    /// <param name="sensorType">Optional sensor type filter (Temperature, Pressure, Vibration, Humidity).</param>
    /// <param name="limit">Maximum number of readings (default 50, max 500).</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>List of sensor readings.</returns>
    [HttpGet("readings")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetReadings(
        [FromQuery] int lineId,
        [FromQuery] string? sensorType,
        [FromQuery] int limit = 50,
        CancellationToken ct = default)
    {
        if (limit > 500) limit = 500;
        var readings = await _productionService.GetReadingsAsync(lineId, User, sensorType, limit, ct);
        return Ok(readings);
    }

    /// <summary>
    /// List alerts, optionally filtered by line and acknowledgement status.
    /// </summary>
    /// <param name="lineId">Optional production line filter.</param>
    /// <param name="unacknowledgedOnly">If true, return only unacknowledged alerts.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>List of alerts.</returns>
    [HttpGet("anomalies")]
    [Authorize(Roles = "Admin,Engineer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAnomalies(
        [FromQuery] int? lineId,
        [FromQuery] bool unacknowledgedOnly = false,
        CancellationToken ct = default)
    {
        var alerts = await _productionService.GetAlertsAsync(lineId, User, unacknowledgedOnly, ct);
        return Ok(alerts);
    }

    /// <summary>
    /// Acknowledge an alert.
    /// </summary>
    /// <param name="alertId">Alert ID.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>200 if acknowledged, 404 if not found.</returns>
    [HttpPut("alerts/{alertId:int}/acknowledge")]
    [Authorize(Roles = "Admin,Engineer,Operator")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AcknowledgeAlert(int alertId, CancellationToken ct)
    {
        var userId = GetUserId();
        var success = await _productionService.AcknowledgeAlertAsync(alertId, userId, ct);
        if (!success)
            return NotFound(new ProblemDetails
            {
                Title = "Not found",
                Detail = $"Alert {alertId} not found",
                Status = StatusCodes.Status404NotFound
            });
        return Ok(new { message = "Alert acknowledged" });
    }

    private int GetUserId()
    {
        var idClaim = User.FindFirst("sub")?.Value ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0";
        return int.TryParse(idClaim, out var id) ? id : 0;
    }
}