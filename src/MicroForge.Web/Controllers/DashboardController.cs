using MicroForge.Application.Dtos;
using MicroForge.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MicroForge.Web.Controllers;

/// <summary>
/// Dashboard summary endpoint aggregating key production metrics.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DashboardController : ControllerBase
{
    private readonly IProductionService _productionService;

    public DashboardController(IProductionService productionService)
    {
        _productionService = productionService;
    }

    /// <summary>
    /// Get a dashboard summary with production line counts, active runs, open alerts, and recent data.
    /// </summary>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>Aggregated dashboard summary.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDashboard(CancellationToken ct)
    {
        var summary = await _productionService.GetDashboardAsync(User, ct);
        return Ok(summary);
    }
}