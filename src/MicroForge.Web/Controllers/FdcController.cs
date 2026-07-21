using MicroForge.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MicroForge.Web.Controllers;

/// <summary>
/// Fault Detection and Classification (FDC), Chamber Matching, and Remaining Useful Life (RUL) diagnostics.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class FdcController : ControllerBase
{
    private readonly IFdcService _fdcService;

    public FdcController(IFdcService fdcService)
    {
        _fdcService = fdcService;
    }

    /// <summary>
    /// Compare average process sensor metrics across active tools to flag mismatched chambers.
    /// </summary>
    [HttpGet("matching")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetChamberMatching(CancellationToken ct)
    {
        var summaries = await _fdcService.GetChamberMatchingAsync(ct);
        return Ok(summaries);
    }

    /// <summary>
    /// Calculate Remaining Useful Life (RUL) indices for critical tool consumable parts.
    /// </summary>
    [HttpGet("maintenance")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMaintenanceStatus(CancellationToken ct)
    {
        var statuses = await _fdcService.GetMaintenanceStatusAsync(ct);
        return Ok(statuses);
    }

    /// <summary>
    /// Analyze real-time sensor streams against fault signatures to flag active alarms.
    /// </summary>
    [HttpGet("faults")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetActiveFaults(CancellationToken ct)
    {
        var faults = await _fdcService.GetActiveFaultsAsync(ct);
        return Ok(faults);
    }
}
