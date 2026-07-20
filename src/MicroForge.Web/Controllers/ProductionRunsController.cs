using MicroForge.Application.Dtos;
using MicroForge.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MicroForge.Web.Controllers;

/// <summary>
/// Production run management endpoints.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProductionRunsController : ControllerBase
{
    private readonly IProductionService _productionService;
    private readonly ILogger<ProductionRunsController> _logger;

    public ProductionRunsController(IProductionService productionService, ILogger<ProductionRunsController> logger)
    {
        _productionService = productionService;
        _logger = logger;
    }

    /// <summary>
    /// List production runs, optionally filtered by line.
    /// </summary>
    /// <param name="lineId">Optional production line filter.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>List of production runs.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] int? lineId, CancellationToken ct)
    {
        var runs = await _productionService.GetRunsAsync(lineId, User, ct);
        return Ok(runs);
    }

    /// <summary>
    /// Create a new production run. Requires Engineer or Operator role.
    /// </summary>
    /// <param name="request">Run details.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>201 Created with the new run.</returns>
    [HttpPost]
    [Authorize(Roles = "Admin,Engineer,Operator")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateProductionRunRequest request, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var run = await _productionService.CreateRunAsync(request, ct);
        return CreatedAtAction(nameof(GetAll), null, run);
    }

    /// <summary>
    /// Update the status and/or actual yield of a production run.
    /// </summary>
    /// <param name="id">Run ID.</param>
    /// <param name="request">Update details.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>Updated run or 404.</returns>
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin,Engineer,Operator")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateRunStatusRequest request, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var run = await _productionService.UpdateRunStatusAsync(id, request.Status, request.ActualYield, ct);
            return Ok(run);
        }
        catch (InvalidOperationException)
        {
            return NotFound(new ProblemDetails
            {
                Title = "Not found",
                Detail = $"Production run {id} not found",
                Status = StatusCodes.Status404NotFound
            });
        }
    }

    /// <summary>
    /// List defects for a specific production run.
    /// </summary>
    /// <param name="id">Run ID.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>List of defect records.</returns>
    [HttpGet("{id:int}/defects")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDefects(int id, CancellationToken ct)
    {
        var defects = await _productionService.GetDefectsAsync(id, ct);
        return Ok(defects);
    }

    /// <summary>
    /// Report a defect for a production run. Requires Engineer or Operator role.
    /// </summary>
    /// <param name="id">Run ID.</param>
    /// <param name="request">Defect details.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>201 Created with the defect record.</returns>
    [HttpPost("{id:int}/defects")]
    [Authorize(Roles = "Admin,Engineer,Operator")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateDefect(int id, [FromBody] CreateDefectRequest request, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        request.ProductionRunId = id;
        var userId = GetUserId();
        var defect = await _productionService.CreateDefectAsync(request, userId, ct);
        return CreatedAtAction(nameof(GetDefects), new { id }, defect);
    }

    private int GetUserId()
    {
        var idClaim = User.FindFirst("sub")?.Value ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0";
        return int.TryParse(idClaim, out var id) ? id : 0;
    }
}

/// <summary>
/// Request model for updating run status.
/// </summary>
public class UpdateRunStatusRequest
{
    public string Status { get; set; } = string.Empty;
    public decimal? ActualYield { get; set; }
}