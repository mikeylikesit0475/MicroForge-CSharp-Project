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
/// WIP (Work In Progress), Lot, and Wafer tracking endpoints.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class WipController : ControllerBase
{
    private readonly IWipService _wipService;
    private readonly ILogger<WipController> _logger;

    public WipController(IWipService wipService, ILogger<WipController> logger)
    {
        _wipService = wipService;
        _logger = logger;
    }

    /// <summary>
    /// List all active WIP lots in the fab.
    /// </summary>
    [HttpGet("lots")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetLots(CancellationToken ct)
    {
        var lots = await _wipService.GetActiveLotsAsync(ct);
        return Ok(lots);
    }

    /// <summary>
    /// Get process history and wafer details for a lot.
    /// </summary>
    [HttpGet("lots/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetLotDetail(int id, CancellationToken ct)
    {
        var lot = await _wipService.GetLotDetailAsync(id, ct);
        if (lot == null)
            return NotFound(new ProblemDetails
            {
                Title = "Lot Not Found",
                Detail = $"Lot ID {id} was not found",
                Status = StatusCodes.Status404NotFound
            });
        return Ok(lot);
    }

    /// <summary>
    /// Get all available route steps.
    /// </summary>
    [HttpGet("steps")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSteps(CancellationToken ct)
    {
        var steps = await _wipService.GetRouteStepsAsync(ct);
        return Ok(steps);
    }

    /// <summary>
    /// Move a lot to another step along the fab route.
    /// </summary>
    [HttpPost("lots/{id:int}/move")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> MoveLot(int id, [FromQuery] int targetStepId, CancellationToken ct)
    {
        var operatorId = GetUserId();
        var success = await _wipService.MoveLotToStepAsync(id, targetStepId, operatorId, ct);
        if (!success)
            return BadRequest(new ProblemDetails
            {
                Title = "Move Blocked",
                Detail = "Could not route the lot. Verify the lot is active and target step exists.",
                Status = StatusCodes.Status400BadRequest
            });
        
        _logger.LogInformation("Lot {LotId} successfully routed to step {StepId} by Operator {OperatorId}", id, targetStepId, operatorId);
        return Ok(new { Message = "Lot successfully routed" });
    }

    /// <summary>
    /// Quarantine hold a lot (Engineering action).
    /// </summary>
    [HttpPost("lots/{id:int}/hold")]
    [Authorize(Roles = "Admin,Engineer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> HoldLot(int id, [FromQuery] string reason, CancellationToken ct)
    {
        var success = await _wipService.HoldLotAsync(id, reason, ct);
        if (!success)
            return BadRequest(new ProblemDetails
            {
                Title = "Hold Failed",
                Detail = "Could not place hold. Verify the lot is active.",
                Status = StatusCodes.Status400BadRequest
            });

        _logger.LogWarning("Lot {LotId} placed on Engineering Hold. Reason: {Reason}", id, reason);
        return Ok(new { Message = "Lot successfully held" });
    }

    /// <summary>
    /// Release hold on a lot (Engineering action).
    /// </summary>
    [HttpPost("lots/{id:int}/release")]
    [Authorize(Roles = "Admin,Engineer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ReleaseLot(int id, CancellationToken ct)
    {
        var success = await _wipService.ReleaseLotAsync(id, ct);
        if (!success)
            return BadRequest(new ProblemDetails
            {
                Title = "Release Failed",
                Detail = "Could not release hold. Verify the lot is currently on hold.",
                Status = StatusCodes.Status400BadRequest
            });

        _logger.LogInformation("Lot {LotId} released from Hold.", id);
        return Ok(new { Message = "Lot successfully released" });
    }

    /// <summary>
    /// Create a new WIP lot.
    /// </summary>
    [HttpPost("lots/create")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateLot([FromQuery] string lotNumber, [FromQuery] string recipeName, [FromBody] List<string> waferIds, CancellationToken ct)
    {
        var lot = await _wipService.CreateLotAsync(lotNumber, recipeName, waferIds, ct);
        return Ok(lot);
    }

    private int GetUserId()
    {
        var idClaim = User.FindFirst("sub")?.Value ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0";
        int.TryParse(idClaim, out var val);
        return val;
    }

    [HttpGet("instructions/{stepId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetInstructions(int stepId, CancellationToken ct)
    {
        var list = await _wipService.GetWorkInstructionsAsync(stepId, ct);
        return Ok(list);
    }

    [HttpGet("lots/{id:int}/checklist")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetChecklistProgress(int id, CancellationToken ct)
    {
        var list = await _wipService.GetChecklistProgressAsync(id, ct);
        return Ok(list);
    }

    [HttpPost("lots/{id:int}/checklist/toggle")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ToggleChecklist(int id, [FromQuery] int instructionId, [FromQuery] bool completed, CancellationToken ct)
    {
        var operatorId = GetUserId();
        await _wipService.UpdateChecklistProgressAsync(id, instructionId, completed, operatorId, ct);
        return Ok(new { Message = "Checklist progress updated" });
    }

    [HttpGet("qualifications")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetQualifications(CancellationToken ct)
    {
        var list = await _wipService.GetQualificationsAsync(ct);
        return Ok(list);
    }

    [HttpPost("qualifications/set")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> SetQualification([FromQuery] int lineId, [FromQuery] string recipeName, [FromQuery] bool qualified, CancellationToken ct)
    {
        await _wipService.SetQualificationAsync(lineId, recipeName, qualified, ct);
        return Ok(new { Message = "Qualification status set" });
    }
}
