using MicroForge.Application.Dtos;
using MicroForge.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MicroForge.Web.Controllers;

/// <summary>
/// Predictive analytics endpoints: yield prediction, yield trends with
/// prediction overlays, and dashboard chart data aggregation.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AnalyticsController : ControllerBase
{
    private readonly IPredictiveAnalyticsService _analyticsService;

    public AnalyticsController(IPredictiveAnalyticsService analyticsService)
    {
        _analyticsService = analyticsService;
    }

    /// <summary>
    /// Predict yield for a specific production line using linear regression.
    /// </summary>
    /// <param name="lineId">Production line ID.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>Yield prediction with confidence and feature weights.</returns>
    [HttpGet("predict-yield/{lineId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PredictYield(int lineId, CancellationToken ct)
    {
        try
        {
            var prediction = await _analyticsService.PredictYieldAsync(lineId, ct);
            return Ok(prediction);
        }
        catch (InvalidOperationException)
        {
            return NotFound($"Production line {lineId} not found");
        }
    }

    /// <summary>
    /// Get yield trend data with prediction overlays for charting.
    /// </summary>
    /// <param name="lineId">Optional production line ID filter.</param>
    /// <param name="limit">Number of historical runs (default 10, max 50).</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>Yield trends with predictions for one or all active lines.</returns>
    [HttpGet("yield-trends")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetYieldTrends(
        [FromQuery] int? lineId,
        [FromQuery] int limit = 10,
        CancellationToken ct = default)
    {
        var trends = await _analyticsService.GetYieldTrendsAsync(lineId, limit, ct);
        return Ok(trends);
    }

    /// <summary>
    /// Get aggregated chart data for the dashboard: sensor gauges,
    /// yield trends with predictions, and defect distribution.
    /// </summary>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>Dashboard chart data bundle.</returns>
    [HttpGet("dashboard-charts")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDashboardCharts(CancellationToken ct)
    {
        var chartData = await _analyticsService.GetDashboardChartDataAsync(ct);
        return Ok(chartData);
    }
}