using MicroForge.Infrastructure.Data;
using MicroForge.Infrastructure.Telemetry;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MicroForge.Web.Controllers;

/// <summary>
/// Prometheus-style metrics endpoint for monitoring (MF-034).
/// Exposes both business metrics (from the database) and runtime metrics
/// (HTTP requests, errors, SignalR connections, pipeline events, agent tool invocations).
/// </summary>
[ApiController]
[Route("metrics")]
[AllowAnonymous]
public class MetricsController : ControllerBase
{
    private readonly MicroForgeDbContext _db;
    private readonly AppMetricsService _appMetrics;

    public MetricsController(MicroForgeDbContext db, AppMetricsService appMetrics)
    {
        _db = db;
        _appMetrics = appMetrics;
    }

    /// <summary>
    /// Returns application metrics in Prometheus text exposition format.
    /// </summary>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>Prometheus-formatted metrics text.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMetrics(CancellationToken ct)
    {
        var totalLines = await _db.ProductionLines.CountAsync(ct);
        var activeLines = await _db.ProductionLines.CountAsync(l => l.IsActive, ct);
        var activeRuns = await _db.ProductionRuns.CountAsync(r => r.Status == "Running", ct);
        var completedRuns = await _db.ProductionRuns.CountAsync(r => r.Status == "Completed", ct);
        var openAlerts = await _db.Alerts.CountAsync(a => !a.IsAcknowledged, ct);
        var totalDefects = await _db.DefectRecords.CountAsync(ct);
        var totalDocuments = await _db.Documents.CountAsync(ct);
        var totalUsers = await _db.Users.CountAsync(ct);
        var totalSensorReadings = await _db.SensorReadings.CountAsync(ct);

        var sb = new System.Text.StringBuilder();

        // ── Runtime metrics (MF-034) ──────────────────────────────────
        sb.AppendLine("# HELP http_requests_total Total number of HTTP requests received");
        sb.AppendLine("# TYPE http_requests_total counter");
        sb.AppendLine($"http_requests_total {_appMetrics.HttpRequestsTotal}");
        sb.AppendLine();

        sb.AppendLine("# HELP http_errors_total Total number of HTTP responses with status >= 500");
        sb.AppendLine("# TYPE http_errors_total counter");
        sb.AppendLine($"http_errors_total {_appMetrics.HttpErrorsTotal}");
        sb.AppendLine();

        sb.AppendLine("# HELP signalr_connections_active Current number of active SignalR connections");
        sb.AppendLine("# TYPE signalr_connections_active gauge");
        sb.AppendLine($"signalr_connections_active {_appMetrics.SignalrConnectionsActive}");
        sb.AppendLine();

        sb.AppendLine("# HELP pipeline_events_processed_total Total number of pipeline events processed");
        sb.AppendLine("# TYPE pipeline_events_processed_total counter");
        sb.AppendLine($"pipeline_events_processed_total {_appMetrics.PipelineEventsProcessedTotal}");
        sb.AppendLine();

        sb.AppendLine("# HELP agent_tool_invocations_total Total number of agent tool invocations");
        sb.AppendLine("# TYPE agent_tool_invocations_total counter");
        sb.AppendLine($"agent_tool_invocations_total {_appMetrics.AgentToolInvocationsTotal}");
        sb.AppendLine();

        // ── Business metrics ─────────────────────────────────────────
        sb.AppendLine("# HELP microforge_production_lines_total Total number of production lines");
        sb.AppendLine("# TYPE microforge_production_lines_total gauge");
        sb.AppendLine($"microforge_production_lines_total {totalLines}");
        sb.AppendLine();

        sb.AppendLine("# HELP microforge_active_production_lines_total Number of active production lines");
        sb.AppendLine("# TYPE microforge_active_production_lines_total gauge");
        sb.AppendLine($"microforge_active_production_lines_total {activeLines}");
        sb.AppendLine();

        sb.AppendLine("# HELP microforge_active_runs_total Number of currently running production runs");
        sb.AppendLine("# TYPE microforge_active_runs_total gauge");
        sb.AppendLine($"microforge_active_runs_total {activeRuns}");
        sb.AppendLine();

        sb.AppendLine("# HELP microforge_completed_runs_total Number of completed production runs");
        sb.AppendLine("# TYPE microforge_completed_runs_total gauge");
        sb.AppendLine($"microforge_completed_runs_total {completedRuns}");
        sb.AppendLine();

        sb.AppendLine("# HELP microforge_open_alerts_total Number of unacknowledged alerts");
        sb.AppendLine("# TYPE microforge_open_alerts_total gauge");
        sb.AppendLine($"microforge_open_alerts_total {openAlerts}");
        sb.AppendLine();

        sb.AppendLine("# HELP microforge_defects_total Total number of defect records");
        sb.AppendLine("# TYPE microforge_defects_total gauge");
        sb.AppendLine($"microforge_defects_total {totalDefects}");
        sb.AppendLine();

        sb.AppendLine("# HELP microforge_documents_total Total number of uploaded documents");
        sb.AppendLine("# TYPE microforge_documents_total gauge");
        sb.AppendLine($"microforge_documents_total {totalDocuments}");
        sb.AppendLine();

        sb.AppendLine("# HELP microforge_users_total Total number of registered users");
        sb.AppendLine("# TYPE microforge_users_total gauge");
        sb.AppendLine($"microforge_users_total {totalUsers}");
        sb.AppendLine();

        sb.AppendLine("# HELP microforge_sensor_readings_total Total number of sensor readings recorded");
        sb.AppendLine("# TYPE microforge_sensor_readings_total gauge");
        sb.AppendLine($"microforge_sensor_readings_total {totalSensorReadings}");

        return Content(sb.ToString(), "text/plain; version=0.0.4; charset=utf-8");
    }
}