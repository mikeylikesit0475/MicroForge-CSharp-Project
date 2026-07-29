using MicroForge.Application.Services;
using MicroForge.Domain.Entities;
using MicroForge.Infrastructure.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MicroForge.Web.Controllers;

/// <summary>
/// Advanced Fab Diagnostics, SECS/GEM, OEE, RCA, and Collaboration endpoints.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class FabController : ControllerBase
{
    private readonly IFabExpansionService _fabService;
    private readonly IFabSelfHealingService _selfHealingService;
    private readonly IWaferDieMapService _dieMapService;
    private readonly IMultivariateRcaService _rcaService;

    public FabController(
        IFabExpansionService fabService,
        IFabSelfHealingService selfHealingService,
        IWaferDieMapService dieMapService,
        IMultivariateRcaService rcaService)
    {
        _fabService = fabService;
        _selfHealingService = selfHealingService;
        _dieMapService = dieMapService;
        _rcaService = rcaService;
    }

    [HttpGet("oee")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOee(CancellationToken ct)
    {
        var summaries = await _fabService.GetOeeSummariesAsync(ct);
        return Ok(summaries);
    }

    [HttpGet("maintenance")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMaintenanceLogs(CancellationToken ct)
    {
        var logs = await _fabService.GetToolMaintenanceLogsAsync(ct);
        return Ok(logs);
    }

    [HttpPost("maintenance/record")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> RecordMaintenance([FromQuery] int lineId, [FromQuery] string status, [FromQuery] double runtime, CancellationToken ct)
    {
        await _fabService.RecordToolMaintenanceAsync(lineId, status, runtime, ct);
        return Ok(new { Message = "Maintenance recorded successfully" });
    }

    [HttpGet("recipes/{name}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRecipeVersions(string name, CancellationToken ct)
    {
        var versions = await _fabService.GetRecipeVersionsAsync(name, ct);
        return Ok(versions);
    }

    [HttpPost("recipes/create")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateRecipe([FromQuery] string recipeName, [FromQuery] int version, [FromQuery] string xml, [FromQuery] string author, CancellationToken ct)
    {
        var result = await _fabService.CreateRecipeVersionAsync(recipeName, version, xml, author, ct);
        return Ok(result);
    }

    [HttpGet("diff")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetGoldenRunDiff([FromQuery] int runId, [FromQuery] string sensorType, CancellationToken ct)
    {
        var diff = await _fabService.GetGoldenRunDiffAsync(runId, sensorType, ct);
        return Ok(diff);
    }

    [HttpGet("correlation")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetYieldCorrelation(CancellationToken ct)
    {
        var correlation = await _fabService.GetYieldCorrelationAsync(ct);
        return Ok(correlation);
    }

    [HttpGet("capas")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCapas(CancellationToken ct)
    {
        var capas = await _fabService.GetCapasAsync(ct);
        return Ok(capas);
    }

    [HttpPost("capas/create")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateCapa([FromQuery] int? lotId, [FromQuery] string description, [FromQuery] string assignee, CancellationToken ct)
    {
        var record = await _fabService.CreateCapaAsync(lotId, description, assignee, ct);
        return Ok(record);
    }

    [HttpPost("capas/{id:int}/approve")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ApproveCapa(int id, [FromQuery] string approver, CancellationToken ct)
    {
        var success = await _fabService.ApproveCapaAsync(id, approver, ct);
        if (!success) return BadRequest(new ProblemDetails { Title = "Approval Failed", Status = StatusCodes.Status400BadRequest });
        return Ok(new { Message = "CAPA successfully approved" });
    }

    [HttpGet("ecos")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetEcos(CancellationToken ct)
    {
        var ecos = await _fabService.GetEcosAsync(ct);
        return Ok(ecos);
    }

    [HttpPost("ecos/create")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateEco([FromQuery] string title, [FromQuery] string recipeName, [FromQuery] string details, [FromQuery] string approver, CancellationToken ct)
    {
        var record = await _fabService.CreateEcoAsync(title, recipeName, details, approver, ct);
        return Ok(record);
    }

    [HttpGet("shiftlogs")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetShiftLogs(CancellationToken ct)
    {
        var logs = await _fabService.GetShiftLogsAsync(ct);
        return Ok(logs);
    }

    [HttpPost("shiftlogs/create")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateShiftLog([FromQuery] string shiftName, [FromQuery] string notes, [FromQuery] string summary, [FromQuery] string operatorName, CancellationToken ct)
    {
        var log = await _fabService.CreateShiftLogAsync(shiftName, notes, summary, operatorName, ct);
        return Ok(log);
    }

    [HttpGet("secslogs")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetSecsLogs()
    {
        var logs = SecsGemSimulator.TrafficLogs.ToList();
        return Ok(logs);
    }

    [HttpPost("secslogs/inject")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult InjectSecsLog([FromQuery] string direction, [FromQuery] string msgType, [FromQuery] string hex, [FromQuery] string desc)
    {
        SecsGemSimulator.LogTraffic(direction, msgType, hex, desc);
        return Ok(new { Message = "Log injected successfully" });
    }

    [HttpPost("secs/send-command")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult SendSecsCommand([FromQuery] string commandName, [FromQuery] string recipeName = "DRAM-16Gb-v3")
    {
        SecsGemSimulator.SendRemoteCommand(commandName, recipeName);
        return Ok(new { Message = $"SECS remote command '{commandName}' sent to equipment socket." });
    }

    [HttpPost("self-healing/trigger")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> TriggerSelfHealing([FromQuery] int lotId, [FromQuery] string anomaly = "Thermal Runaway (+4.2σ Drift)", CancellationToken ct = default)
    {
        var result = await _selfHealingService.TriggerSelfHealingAsync(lotId, anomaly, ct);
        if (result == null) return NotFound(new { Message = $"Lot {lotId} not found." });
        return Ok(result);
    }

    [HttpGet("wafermap/{lotId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetWaferDieMap(int lotId, [FromQuery] string waferId = "", CancellationToken ct = default)
    {
        var map = await _dieMapService.GenerateWaferDieMapAsync(lotId, waferId, ct);
        return Ok(map);
    }

    [HttpGet("rca/multivariate-diff/{runId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMultivariateRca(int runId, [FromQuery] string sensorType = "RF_Power", CancellationToken ct = default)
    {
        var rca = await _rcaService.GetMultivariateRcaAsync(runId, sensorType, ct);
        return Ok(rca);
    }
}
