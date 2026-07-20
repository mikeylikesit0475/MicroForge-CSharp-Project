using MicroForge.Application.Dtos;
using MicroForge.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MicroForge.Web.Controllers;

/// <summary>
/// Production line management endpoints.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProductionLinesController : ControllerBase
{
    private readonly IProductionService _productionService;
    private readonly ILogger<ProductionLinesController> _logger;

    public ProductionLinesController(IProductionService productionService, ILogger<ProductionLinesController> logger)
    {
        _productionService = productionService;
        _logger = logger;
    }

    /// <summary>
    /// List all active production lines the user has access to.
    /// </summary>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>List of production lines.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var lines = await _productionService.GetLinesAsync(User, ct);
        return Ok(lines);
    }

    /// <summary>
    /// Get details for a specific production line.
    /// </summary>
    /// <param name="id">Production line ID.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>Production line details or 404.</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var line = await _productionService.GetLineAsync(id, User, ct);
        if (line == null)
            return NotFound(new ProblemDetails
            {
                Title = "Not found",
                Detail = $"Production line {id} not found or access denied",
                Status = StatusCodes.Status404NotFound
            });
        return Ok(line);
    }

    /// <summary>
    /// Create a new production line. Requires Admin or Engineer role.
    /// </summary>
    /// <param name="request">Production line details.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>201 Created with the new line.</returns>
    [HttpPost]
    [Authorize(Roles = "Admin,Engineer")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateProductionLineRequest request, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var line = await _productionService.CreateLineAsync(request, ct);
        return CreatedAtAction(nameof(GetById), new { id = line.Id }, line);
    }
}