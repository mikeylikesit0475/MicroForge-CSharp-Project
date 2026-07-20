using MicroForge.Domain.Entities;
using MicroForge.Domain.Interfaces;
using MicroForge.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Nodes;

namespace MicroForge.Infrastructure.AI.Tools;

/// <summary>
/// Base class for all agent tools — provides helper methods for argument extraction
/// and data boundary enforcement.
/// </summary>
public abstract class BaseTool : ITool
{
    public abstract string Name { get; }
    public abstract string Description { get; }
    public abstract string Category { get; }
    public abstract JsonObject Schema { get; }
    public abstract Task<ToolResult> InvokeAsync(JsonObject? arguments, ToolExecutionContext context, CancellationToken ct = default);

    protected static int GetInt(JsonObject? args, string name, int defaultValue = 0)
    {
        if (args != null && args.TryGetPropertyValue(name, out var node) && node is JsonValue v && v.TryGetValue<int>(out var val))
            return val;
        return defaultValue;
    }

    protected static string? GetString(JsonObject? args, string name)
    {
        if (args != null && args.TryGetPropertyValue(name, out var node) && node is JsonValue v && v.TryGetValue<string>(out var val))
            return val;
        return null;
    }

    protected static bool GetBool(JsonObject? args, string name, bool defaultValue = false)
    {
        if (args != null && args.TryGetPropertyValue(name, out var node) && node is JsonValue v && v.TryGetValue<bool>(out var val))
            return val;
        return defaultValue;
    }

    protected static List<double> GetDoubleList(JsonObject? args, string name)
    {
        if (args != null && args.TryGetPropertyValue(name, out var node) && node is JsonArray arr)
        {
            return arr.Select(n => n?.GetValue<double>() ?? 0).ToList();
        }
        return new List<double>();
    }

    protected static int[] GetAllowedLineIds(ToolExecutionContext context)
    {
        if (context.Roles.Contains("Admin") || context.Roles.Contains("Engineer"))
            return Array.Empty<int>();
        return context.ProductionLineIds.ToArray();
    }

    private static readonly System.Text.Json.JsonSerializerOptions _jsonopts = new()
    {
        PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase
    };

    protected static string Serialize(object data) => System.Text.Json.JsonSerializer.Serialize(data, _jsonopts);
}

/// <summary>
/// Queries defect records with optional filtering by line, severity, and date range.
/// </summary>
public class QueryProductionDefectsTool : BaseTool
{
    private readonly MicroForgeDbContext _db;
    public QueryProductionDefectsTool(MicroForgeDbContext db) => _db = db;

    public override string Name => "query_production_defects";
    public override string Description => "Query defect records for production runs. Can filter by production line, date range, severity, and defect type.";
    public override string Category => "quality";
    public override JsonObject Schema => new()
    {
        ["type"] = "object",
        ["properties"] = new JsonObject
        {
            ["productionLineId"] = new JsonObject { ["type"] = "integer", ["description"] = "Filter by production line ID" },
            ["severity"] = new JsonObject { ["type"] = "string", ["description"] = "Filter by severity: Critical, Major, Minor, Cosmetic" },
            ["limit"] = new JsonObject { ["type"] = "integer", ["description"] = "Max results (default 20, max 100)" }
        }
    };

    public override async Task<ToolResult> InvokeAsync(JsonObject? arguments, ToolExecutionContext context, CancellationToken ct = default)
    {
        var lineId = GetInt(arguments, "productionLineId");
        var severity = GetString(arguments, "severity");
        var limit = GetInt(arguments, "limit", 20);
        if (limit > 100) limit = 100;

        var query = _db.DefectRecords.AsQueryable();
        var allowed = GetAllowedLineIds(context);
        if (allowed.Length > 0)
        {
            query = query.Where(d => _db.ProductionRuns.Any(r => r.Id == d.ProductionRunId && allowed.Contains(r.ProductionLineId)));
        }
        if (lineId > 0)
        {
            if (allowed.Length > 0 && !allowed.Contains(lineId))
                return ToolResult.Fail("Access denied to production line");
            query = query.Where(d => _db.ProductionRuns.Any(r => r.Id == d.ProductionRunId && r.ProductionLineId == lineId));
        }
        if (!string.IsNullOrEmpty(severity))
            query = query.Where(d => d.Severity == severity);

        var defects = await query.OrderByDescending(d => d.DetectedAt).Take(limit)
            .Select(d => new { d.Id, d.ProductionRunId, d.DefectType, d.Severity, d.Description, d.DetectedAt })
            .ToListAsync(ct);

        return ToolResult.Ok(Serialize(new { count = defects.Count, results = defects }));
    }
}

/// <summary>
/// Queries historical sensor data with optional filtering.
/// </summary>
public class QuerySensorReadingsTool : BaseTool
{
    private readonly MicroForgeDbContext _db;
    public QuerySensorReadingsTool(MicroForgeDbContext db) => _db = db;

    public override string Name => "query_sensor_readings";
    public override string Description => "Query historical sensor data with optional filtering by line, sensor type, and anomaly status.";
    public override string Category => "sensors";
    public override JsonObject Schema => new()
    {
        ["type"] = "object",
        ["properties"] = new JsonObject
        {
            ["productionLineId"] = new JsonObject { ["type"] = "integer" },
            ["sensorType"] = new JsonObject { ["type"] = "string", ["description"] = "Temperature, Pressure, Vibration, Humidity" },
            ["anomaliesOnly"] = new JsonObject { ["type"] = "boolean" },
            ["limit"] = new JsonObject { ["type"] = "integer", ["description"] = "Max results (default 20, max 100)" }
        }
    };

    public override async Task<ToolResult> InvokeAsync(JsonObject? arguments, ToolExecutionContext context, CancellationToken ct = default)
    {
        var lineId = GetInt(arguments, "productionLineId");
        var sensorType = GetString(arguments, "sensorType");
        var anomaliesOnly = GetBool(arguments, "anomaliesOnly");
        var limit = GetInt(arguments, "limit", 20);
        if (limit > 100) limit = 100;

        var query = _db.SensorReadings.AsQueryable();
        var allowed = GetAllowedLineIds(context);
        if (allowed.Length > 0)
            query = query.Where(r => allowed.Contains(r.ProductionLineId));
        if (lineId > 0)
        {
            if (allowed.Length > 0 && !allowed.Contains(lineId))
                return ToolResult.Fail("Access denied to production line");
            query = query.Where(r => r.ProductionLineId == lineId);
        }
        if (!string.IsNullOrEmpty(sensorType))
            query = query.Where(r => r.SensorType == sensorType);
        if (anomaliesOnly)
            query = query.Where(r => r.IsAnomaly);

        var readings = await query.OrderByDescending(r => r.Timestamp).Take(limit)
            .Select(r => new { r.Id, r.ProductionLineId, r.SensorType, r.Value, r.Unit, r.Timestamp, r.IsAnomaly, r.AnomalyType })
            .ToListAsync(ct);

        return ToolResult.Ok(Serialize(new { count = readings.Count, results = readings }));
    }
}

/// <summary>
/// Gets current status of production lines including active runs and open alerts.
/// </summary>
public class GetProductionLineStatusTool : BaseTool
{
    private readonly MicroForgeDbContext _db;
    public GetProductionLineStatusTool(MicroForgeDbContext db) => _db = db;

    public override string Name => "get_production_line_status";
    public override string Description => "Get current status of production lines including active runs and recent alerts.";
    public override string Category => "production";
    public override JsonObject Schema => new()
    {
        ["type"] = "object",
        ["properties"] = new JsonObject
        {
            ["productionLineId"] = new JsonObject { ["type"] = "integer", ["description"] = "Specific line ID (optional)" }
        }
    };

    public override async Task<ToolResult> InvokeAsync(JsonObject? arguments, ToolExecutionContext context, CancellationToken ct = default)
    {
        var lineId = GetInt(arguments, "productionLineId");
        var allowed = GetAllowedLineIds(context);

        var lineQuery = _db.ProductionLines.Where(l => l.IsActive);
        if (lineId > 0)
        {
            if (allowed.Length > 0 && !allowed.Contains(lineId))
                return ToolResult.Fail("Access denied to production line");
            lineQuery = lineQuery.Where(l => l.Id == lineId);
        }
        else if (allowed.Length > 0)
        {
            lineQuery = lineQuery.Where(l => allowed.Contains(l.Id));
        }

        var lines = await lineQuery.ToListAsync(ct);
        var results = new List<object>();
        foreach (var line in lines)
        {
            var activeRun = await _db.ProductionRuns
                .Where(r => r.ProductionLineId == line.Id && r.Status == "Running")
                .FirstOrDefaultAsync(ct);
            var openAlerts = await _db.Alerts
                .Where(a => a.ProductionLineId == line.Id && !a.IsAcknowledged)
                .CountAsync(ct);

            var recentSensorTypes = await _db.SensorReadings
                .Where(r => r.ProductionLineId == line.Id)
                .Select(r => r.SensorType)
                .Distinct()
                .ToListAsync(ct);

            var recentSensors = new List<object>();
            foreach (var sensorType in recentSensorTypes)
            {
                var latest = await _db.SensorReadings
                    .Where(r => r.ProductionLineId == line.Id && r.SensorType == sensorType)
                    .OrderByDescending(r => r.Timestamp)
                    .FirstOrDefaultAsync(ct);
                if (latest != null)
                    recentSensors.Add(new { latest.SensorType, latest.Value, latest.Unit, latest.Timestamp });
            }

            results.Add(new
            {
                line.Id, line.Name, line.Location, line.LineType,
                activeRun = activeRun != null ? new { activeRun.BatchNumber, activeRun.Status, activeRun.TargetYield } : null,
                openAlerts,
                recentSensors
            });
        }
        return ToolResult.Ok(Serialize(new { lines = results }));
    }
}

/// <summary>
/// Gets yield and defect summary for a specific production run.
/// </summary>
public class GetProductionRunSummaryTool : BaseTool
{
    private readonly MicroForgeDbContext _db;
    public GetProductionRunSummaryTool(MicroForgeDbContext db) => _db = db;

    public override string Name => "get_production_run_summary";
    public override string Description => "Get yield and defect summary for a production run.";
    public override string Category => "production";
    public override JsonObject Schema => new()
    {
        ["type"] = "object",
        ["properties"] = new JsonObject
        {
            ["runId"] = new JsonObject { ["type"] = "integer", ["description"] = "Production run ID" }
        },
        ["required"] = new JsonArray { "runId" }
    };

    public override async Task<ToolResult> InvokeAsync(JsonObject? arguments, ToolExecutionContext context, CancellationToken ct = default)
    {
        var runId = GetInt(arguments, "runId");
        if (runId <= 0)
            return ToolResult.Fail("runId is required");

        var run = await _db.ProductionRuns.Include(r => r.ProductionLine).FirstOrDefaultAsync(r => r.Id == runId, ct);
        if (run == null)
            return ToolResult.Fail($"Run {runId} not found");

        var allowed = GetAllowedLineIds(context);
        if (allowed.Length > 0 && !allowed.Contains(run.ProductionLineId))
            return ToolResult.Fail("Access denied to production line");

        var defectCount = await _db.DefectRecords.CountAsync(d => d.ProductionRunId == runId, ct);
        var yieldVariance = run.ActualYield > 0 ? run.ActualYield - run.TargetYield : 0;

        return ToolResult.Ok(Serialize(new
        {
            run.Id, run.BatchNumber, run.Status, run.TargetYield, run.ActualYield,
            yieldVariance, defectCount, lineName = run.ProductionLine?.Name,
            run.StartTime, run.EndTime
        }));
    }
}

/// <summary>
/// Semantic search across embedded manufacturing documents.
/// </summary>
public class SearchDocumentsTool : BaseTool
{
    private readonly IDocumentService _documentService;
    public SearchDocumentsTool(IDocumentService documentService) => _documentService = documentService;

    public override string Name => "search_documents";
    public override string Description => "Semantic search across embedded manufacturing documents (SOPs, specs, reports).";
    public override string Category => "rag";
    public override JsonObject Schema => new()
    {
        ["type"] = "object",
        ["properties"] = new JsonObject
        {
            ["query"] = new JsonObject { ["type"] = "string", ["description"] = "Search query" },
            ["topK"] = new JsonObject { ["type"] = "integer", ["description"] = "Number of results (default 5, max 20)" },
            ["category"] = new JsonObject { ["type"] = "string", ["description"] = "Filter by document category" }
        },
        ["required"] = new JsonArray { "query" }
    };

    public override async Task<ToolResult> InvokeAsync(JsonObject? arguments, ToolExecutionContext context, CancellationToken ct = default)
    {
        var query = GetString(arguments, "query");
        if (string.IsNullOrEmpty(query))
            return ToolResult.Fail("query is required");

        var topK = GetInt(arguments, "topK", 5);
        if (topK > 20) topK = 20;
        var category = GetString(arguments, "category");

        var results = await _documentService.SearchAsync(query, topK, category, ct);
        return ToolResult.Ok(Serialize(new { count = results.Count, results }));
    }
}

/// <summary>
/// Computes statistical metrics on numeric data.
/// </summary>
public class CalculateStatisticsTool : BaseTool
{
    public override string Name => "calculate_statistics";
    public override string Description => "Compute statistical metrics (mean, median, std, min, max) on numeric data.";
    public override string Category => "analytics";
    public override JsonObject Schema => new()
    {
        ["type"] = "object",
        ["properties"] = new JsonObject
        {
            ["data"] = new JsonObject { ["type"] = "array", ["items"] = new JsonObject { ["type"] = "number" } },
            ["metrics"] = new JsonObject { ["type"] = "array", ["items"] = new JsonObject { ["type"] = "string" } }
        },
        ["required"] = new JsonArray { "data" }
    };

    public override Task<ToolResult> InvokeAsync(JsonObject? arguments, ToolExecutionContext context, CancellationToken ct = default)
    {
        var data = GetDoubleList(arguments, "data");
        if (data.Count == 0)
            return Task.FromResult(ToolResult.Fail("data array is empty or missing"));

        var mean = data.Average();
        var sorted = data.OrderBy(x => x).ToList();
        var median = sorted.Count % 2 == 0
            ? (sorted[sorted.Count / 2 - 1] + sorted[sorted.Count / 2]) / 2.0
            : sorted[sorted.Count / 2];
        var std = Math.Sqrt(data.Select(x => Math.Pow(x - mean, 2)).Average());

        return Task.FromResult(ToolResult.Ok(Serialize(new
        {
            count = data.Count, mean, median,
            stdDev = std, min = data.Min(), max = data.Max()
        })));
    }
}

/// <summary>
/// Runs anomaly detection on a sensor data window.
/// </summary>
public class DetectAnomaliesTool : BaseTool
{
    private readonly MicroForgeDbContext _db;
    private readonly IAnomalyDetector _detector;
    public DetectAnomaliesTool(MicroForgeDbContext db, IAnomalyDetector detector) { _db = db; _detector = detector; }

    public override string Name => "detect_anomalies";
    public override string Description => "Run anomaly detection on a sensor data window for a production line.";
    public override string Category => "sensors";
    public override JsonObject Schema => new()
    {
        ["type"] = "object",
        ["properties"] = new JsonObject
        {
            ["productionLineId"] = new JsonObject { ["type"] = "integer" },
            ["sensorType"] = new JsonObject { ["type"] = "string", ["description"] = "Temperature, Pressure, Vibration, Humidity" },
            ["windowSize"] = new JsonObject { ["type"] = "integer", ["description"] = "Number of recent readings to analyze (default 50)" }
        },
        ["required"] = new JsonArray { "productionLineId" }
    };

    public override async Task<ToolResult> InvokeAsync(JsonObject? arguments, ToolExecutionContext context, CancellationToken ct = default)
    {
        var lineId = GetInt(arguments, "productionLineId");
        if (lineId <= 0)
            return ToolResult.Fail("productionLineId is required");

        var allowed = GetAllowedLineIds(context);
        if (allowed.Length > 0 && !allowed.Contains(lineId))
            return ToolResult.Fail("Access denied to production line");

        var sensorType = GetString(arguments, "sensorType") ?? "Temperature";
        var windowSize = GetInt(arguments, "windowSize", 50);

        var readings = await _db.SensorReadings
            .Where(r => r.ProductionLineId == lineId && r.SensorType == sensorType)
            .OrderByDescending(r => r.Timestamp)
            .Take(windowSize)
            .ToListAsync(ct);

        if (readings.Count == 0)
            return ToolResult.Ok(Serialize(new { message = "No readings found", anomalies = Array.Empty<object>() }));

        var values = readings.Select(r => r.Value).ToList();
        var anomalies = new List<object>();
        foreach (var reading in readings)
        {
            var result = _detector.Detect(reading.SensorType, reading.Value, reading.Unit, values);
            if (result.IsAnomaly)
            {
                anomalies.Add(new { reading.Value, reading.Unit, reading.Timestamp, result.AnomalyType, result.Description });
            }
        }

        return ToolResult.Ok(Serialize(new { totalReadings = readings.Count, anomalyCount = anomalies.Count, anomalies }));
    }
}

/// <summary>
/// Creates a new alert for a production issue.
/// </summary>
public class CreateAlertTool : BaseTool
{
    private readonly MicroForgeDbContext _db;
    public CreateAlertTool(MicroForgeDbContext db) => _db = db;

    public override string Name => "create_alert";
    public override string Description => "Create a new alert for a production issue.";
    public override string Category => "alerts";
    public override JsonObject Schema => new()
    {
        ["type"] = "object",
        ["properties"] = new JsonObject
        {
            ["productionLineId"] = new JsonObject { ["type"] = "integer" },
            ["alertType"] = new JsonObject { ["type"] = "string" },
            ["severity"] = new JsonObject { ["type"] = "string", ["description"] = "Info, Warning, Critical" },
            ["title"] = new JsonObject { ["type"] = "string" },
            ["message"] = new JsonObject { ["type"] = "string" }
        },
        ["required"] = new JsonArray { "productionLineId", "title", "message" }
    };

    public override async Task<ToolResult> InvokeAsync(JsonObject? arguments, ToolExecutionContext context, CancellationToken ct = default)
    {
        var lineId = GetInt(arguments, "productionLineId");
        if (lineId <= 0)
            return ToolResult.Fail("productionLineId is required");

        var allowed = GetAllowedLineIds(context);
        if (allowed.Length > 0 && !allowed.Contains(lineId))
            return ToolResult.Fail("Access denied to production line");

        var alert = new Alert
        {
            ProductionLineId = lineId,
            AlertType = GetString(arguments, "alertType") ?? "Manual",
            Severity = GetString(arguments, "severity") ?? "Warning",
            Title = GetString(arguments, "title") ?? "Alert",
            Message = GetString(arguments, "message") ?? "",
            Source = "AIAgent",
            IsAcknowledged = false,
            CreatedAt = DateTime.UtcNow
        };
        _db.Alerts.Add(alert);
        await _db.SaveChangesAsync(ct);

        return ToolResult.Ok(Serialize(new { alertId = alert.Id, message = "Alert created successfully" }));
    }
}

/// <summary>
/// Gets yield trend data for charting across recent production runs.
/// </summary>
public class GetYieldTrendTool : BaseTool
{
    private readonly MicroForgeDbContext _db;
    public GetYieldTrendTool(MicroForgeDbContext db) => _db = db;

    public override string Name => "get_yield_trend";
    public override string Description => "Get yield trend data for charting across recent production runs.";
    public override string Category => "analytics";
    public override JsonObject Schema => new()
    {
        ["type"] = "object",
        ["properties"] = new JsonObject
        {
            ["productionLineId"] = new JsonObject { ["type"] = "integer" },
            ["limit"] = new JsonObject { ["type"] = "integer", ["description"] = "Number of runs (default 10, max 50)" }
        }
    };

    public override async Task<ToolResult> InvokeAsync(JsonObject? arguments, ToolExecutionContext context, CancellationToken ct = default)
    {
        var lineId = GetInt(arguments, "productionLineId");
        var limit = GetInt(arguments, "limit", 10);
        if (limit > 50) limit = 50;

        var query = _db.ProductionRuns.Where(r => r.Status == "Completed");
        var allowed = GetAllowedLineIds(context);
        if (lineId > 0)
        {
            if (allowed.Length > 0 && !allowed.Contains(lineId))
                return ToolResult.Fail("Access denied to production line");
            query = query.Where(r => r.ProductionLineId == lineId);
        }
        else if (allowed.Length > 0)
        {
            query = query.Where(r => allowed.Contains(r.ProductionLineId));
        }

        var runs = await query.OrderByDescending(r => r.StartTime).Take(limit)
            .Select(r => new { r.BatchNumber, r.TargetYield, r.ActualYield, r.StartTime })
            .ToListAsync(ct);

        return ToolResult.Ok(Serialize(new { count = runs.Count, trend = runs }));
    }
}

/// <summary>
/// Queries alerts with optional filtering by line, severity, and acknowledgement status.
/// </summary>
public class QueryAlertsTool : BaseTool
{
    private readonly MicroForgeDbContext _db;
    public QueryAlertsTool(MicroForgeDbContext db) => _db = db;

    public override string Name => "query_alerts";
    public override string Description => "Query alerts with optional filtering by production line, severity, and acknowledgement status.";
    public override string Category => "alerts";
    public override JsonObject Schema => new()
    {
        ["type"] = "object",
        ["properties"] = new JsonObject
        {
            ["productionLineId"] = new JsonObject { ["type"] = "integer", ["description"] = "Filter by production line ID" },
            ["severity"] = new JsonObject { ["type"] = "string", ["description"] = "Filter by severity: Critical, Warning, Info" },
            ["unacknowledgedOnly"] = new JsonObject { ["type"] = "boolean", ["description"] = "If true, return only unacknowledged alerts" },
            ["limit"] = new JsonObject { ["type"] = "integer", ["description"] = "Max results (default 20, max 100)" }
        }
    };

    public override async Task<ToolResult> InvokeAsync(JsonObject? arguments, ToolExecutionContext context, CancellationToken ct = default)
    {
        var lineId = GetInt(arguments, "productionLineId");
        var severity = GetString(arguments, "severity");
        var unacknowledgedOnly = GetBool(arguments, "unacknowledgedOnly");
        var limit = GetInt(arguments, "limit", 20);
        if (limit > 100) limit = 100;

        var query = _db.Alerts.AsQueryable();
        var allowed = GetAllowedLineIds(context);
        if (allowed.Length > 0)
            query = query.Where(a => allowed.Contains(a.ProductionLineId));
        if (lineId > 0)
        {
            if (allowed.Length > 0 && !allowed.Contains(lineId))
                return ToolResult.Fail("Access denied to production line");
            query = query.Where(a => a.ProductionLineId == lineId);
        }
        if (!string.IsNullOrEmpty(severity))
            query = query.Where(a => a.Severity == severity);
        if (unacknowledgedOnly)
            query = query.Where(a => !a.IsAcknowledged);

        var alerts = await query.OrderByDescending(a => a.CreatedAt).Take(limit)
            .Select(a => new { a.Id, a.ProductionLineId, a.AlertType, a.Severity, a.Title, a.Message, a.IsAcknowledged, a.CreatedAt })
            .ToListAsync(ct);

        return ToolResult.Ok(Serialize(new { count = alerts.Count, results = alerts }));
    }
}

/// <summary>
/// Acknowledges an alert, marking it as reviewed by the current user.
/// </summary>
public class AcknowledgeAlertTool : BaseTool
{
    private readonly MicroForgeDbContext _db;
    public AcknowledgeAlertTool(MicroForgeDbContext db) => _db = db;

    public override string Name => "acknowledge_alert";
    public override string Description => "Acknowledge an alert, marking it as reviewed by the current user.";
    public override string Category => "alerts";
    public override JsonObject Schema => new()
    {
        ["type"] = "object",
        ["properties"] = new JsonObject
        {
            ["alertId"] = new JsonObject { ["type"] = "integer", ["description"] = "The alert ID to acknowledge" }
        },
        ["required"] = new JsonArray { "alertId" }
    };

    public override async Task<ToolResult> InvokeAsync(JsonObject? arguments, ToolExecutionContext context, CancellationToken ct = default)
    {
        var alertId = GetInt(arguments, "alertId");
        if (alertId <= 0)
            return ToolResult.Fail("alertId is required");

        var alert = await _db.Alerts.FirstOrDefaultAsync(a => a.Id == alertId, ct);
        if (alert == null)
            return ToolResult.Fail($"Alert {alertId} not found");

        var allowed = GetAllowedLineIds(context);
        if (allowed.Length > 0 && !allowed.Contains(alert.ProductionLineId))
            return ToolResult.Fail("Access denied to production line");

        alert.IsAcknowledged = true;
        alert.AcknowledgedByUserId = context.UserId;
        alert.AcknowledgedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync(ct);

        return ToolResult.Ok(Serialize(new { alertId = alert.Id, message = "Alert acknowledged successfully" }));
    }
}

/// <summary>
/// Creates a defect record for a production run. Requires write access.
/// </summary>
public class CreateDefectRecordTool : BaseTool
{
    private readonly MicroForgeDbContext _db;
    public CreateDefectRecordTool(MicroForgeDbContext db) => _db = db;

    public override string Name => "create_defect_record";
    public override string Description => "Create a defect record for a production run. Requires write access (Admin, Engineer, or Operator).";
    public override string Category => "quality";
    public override JsonObject Schema => new()
    {
        ["type"] = "object",
        ["properties"] = new JsonObject
        {
            ["productionRunId"] = new JsonObject { ["type"] = "integer", ["description"] = "Production run ID" },
            ["defectType"] = new JsonObject { ["type"] = "string", ["description"] = "Type of defect (e.g. Scratch, Crack, Contamination)" },
            ["severity"] = new JsonObject { ["type"] = "string", ["description"] = "Severity: Critical, Major, Minor, Cosmetic" },
            ["description"] = new JsonObject { ["type"] = "string", ["description"] = "Detailed description of the defect" }
        },
        ["required"] = new JsonArray { "productionRunId", "defectType", "severity", "description" }
    };

    public override async Task<ToolResult> InvokeAsync(JsonObject? arguments, ToolExecutionContext context, CancellationToken ct = default)
    {
        // Viewer role does not have write access
        if (context.Roles.Contains("Viewer") && !context.Roles.Contains("Admin") && !context.Roles.Contains("Engineer") && !context.Roles.Contains("Operator"))
            return ToolResult.Fail("Access denied: write permission required");

        var runId = GetInt(arguments, "productionRunId");
        if (runId <= 0)
            return ToolResult.Fail("productionRunId is required");

        var defectType = GetString(arguments, "defectType");
        var severity = GetString(arguments, "severity");
        var description = GetString(arguments, "description");

        if (string.IsNullOrEmpty(defectType) || string.IsNullOrEmpty(severity) || string.IsNullOrEmpty(description))
            return ToolResult.Fail("defectType, severity, and description are all required");

        var run = await _db.ProductionRuns.FirstOrDefaultAsync(r => r.Id == runId, ct);
        if (run == null)
            return ToolResult.Fail($"Production run {runId} not found");

        var allowed = GetAllowedLineIds(context);
        if (allowed.Length > 0 && !allowed.Contains(run.ProductionLineId))
            return ToolResult.Fail("Access denied to production line");

        var defect = new DefectRecord
        {
            ProductionRunId = runId,
            DefectType = defectType,
            Severity = severity,
            Description = description,
            DetectedAt = DateTime.UtcNow,
            DetectedByUserId = context.UserId
        };
        _db.DefectRecords.Add(defect);
        await _db.SaveChangesAsync(ct);

        return ToolResult.Ok(Serialize(new { defectId = defect.Id, message = "Defect record created successfully" }));
    }
}

/// <summary>
/// Queries production runs with optional filtering by line, status, and date range.
/// </summary>
public class QueryProductionRunsTool : BaseTool
{
    private readonly MicroForgeDbContext _db;
    public QueryProductionRunsTool(MicroForgeDbContext db) => _db = db;

    public override string Name => "query_production_runs";
    public override string Description => "Query production runs with optional filtering by line, status, and date range.";
    public override string Category => "production";
    public override JsonObject Schema => new()
    {
        ["type"] = "object",
        ["properties"] = new JsonObject
        {
            ["productionLineId"] = new JsonObject { ["type"] = "integer", ["description"] = "Filter by production line ID" },
            ["status"] = new JsonObject { ["type"] = "string", ["description"] = "Filter by status: Pending, Running, Paused, Completed, Cancelled" },
            ["limit"] = new JsonObject { ["type"] = "integer", ["description"] = "Max results (default 20, max 100)" }
        }
    };

    public override async Task<ToolResult> InvokeAsync(JsonObject? arguments, ToolExecutionContext context, CancellationToken ct = default)
    {
        var lineId = GetInt(arguments, "productionLineId");
        var status = GetString(arguments, "status");
        var limit = GetInt(arguments, "limit", 20);
        if (limit > 100) limit = 100;

        var query = _db.ProductionRuns.AsQueryable();
        var allowed = GetAllowedLineIds(context);
        if (allowed.Length > 0)
            query = query.Where(r => allowed.Contains(r.ProductionLineId));
        if (lineId > 0)
        {
            if (allowed.Length > 0 && !allowed.Contains(lineId))
                return ToolResult.Fail("Access denied to production line");
            query = query.Where(r => r.ProductionLineId == lineId);
        }
        if (!string.IsNullOrEmpty(status))
            query = query.Where(r => r.Status == status);

        var runs = await query.OrderByDescending(r => r.StartTime).Take(limit)
            .Select(r => new { r.Id, r.BatchNumber, r.ProductionLineId, r.Status, r.TargetYield, r.ActualYield, r.StartTime, r.EndTime })
            .ToListAsync(ct);

        return ToolResult.Ok(Serialize(new { count = runs.Count, results = runs }));
    }
}

/// <summary>
/// Updates the status of a production run. Requires write access.
/// </summary>
public class UpdateProductionRunStatusTool : BaseTool
{
    private readonly MicroForgeDbContext _db;
    public UpdateProductionRunStatusTool(MicroForgeDbContext db) => _db = db;

    public override string Name => "update_production_run_status";
    public override string Description => "Update the status of a production run. Requires write access (Admin, Engineer, or Operator).";
    public override string Category => "production";
    public override JsonObject Schema => new()
    {
        ["type"] = "object",
        ["properties"] = new JsonObject
        {
            ["runId"] = new JsonObject { ["type"] = "integer", ["description"] = "Production run ID" },
            ["status"] = new JsonObject { ["type"] = "string", ["description"] = "New status: Pending, Running, Paused, Completed, Cancelled" },
            ["actualYield"] = new JsonObject { ["type"] = "number", ["description"] = "Actual yield percentage (optional, set when completing)" }
        },
        ["required"] = new JsonArray { "runId", "status" }
    };

    public override async Task<ToolResult> InvokeAsync(JsonObject? arguments, ToolExecutionContext context, CancellationToken ct = default)
    {
        // Viewer role does not have write access
        if (context.Roles.Contains("Viewer") && !context.Roles.Contains("Admin") && !context.Roles.Contains("Engineer") && !context.Roles.Contains("Operator"))
            return ToolResult.Fail("Access denied: write permission required");

        var runId = GetInt(arguments, "runId");
        if (runId <= 0)
            return ToolResult.Fail("runId is required");

        var status = GetString(arguments, "status");
        if (string.IsNullOrEmpty(status))
            return ToolResult.Fail("status is required");

        var run = await _db.ProductionRuns.FirstOrDefaultAsync(r => r.Id == runId, ct);
        if (run == null)
            return ToolResult.Fail($"Production run {runId} not found");

        var allowed = GetAllowedLineIds(context);
        if (allowed.Length > 0 && !allowed.Contains(run.ProductionLineId))
            return ToolResult.Fail("Access denied to production line");

        run.Status = status;

        if (status == "Completed")
        {
            run.EndTime = DateTime.UtcNow;
            if (arguments != null && arguments.TryGetPropertyValue("actualYield", out var yieldNode) && yieldNode is JsonValue yv)
            {
                if (yv.TryGetValue<decimal>(out var yieldDec))
                    run.ActualYield = yieldDec;
                else if (yv.TryGetValue<double>(out var yieldDbl))
                    run.ActualYield = (decimal)yieldDbl;
            }
        }

        await _db.SaveChangesAsync(ct);

        return ToolResult.Ok(Serialize(new { runId = run.Id, status = run.Status, message = "Production run status updated successfully" }));
    }
}

/// <summary>
/// Gets an aggregated dashboard summary with KPIs: total lines, active runs, open alerts.
/// </summary>
public class GetDashboardSummaryTool : BaseTool
{
    private readonly MicroForgeDbContext _db;
    public GetDashboardSummaryTool(MicroForgeDbContext db) => _db = db;

    public override string Name => "get_dashboard_summary";
    public override string Description => "Get an aggregated dashboard summary with KPIs: total lines, active runs, open alerts.";
    public override string Category => "dashboard";
    public override JsonObject Schema => new()
    {
        ["type"] = "object",
        ["properties"] = new JsonObject()
    };

    public override async Task<ToolResult> InvokeAsync(JsonObject? arguments, ToolExecutionContext context, CancellationToken ct = default)
    {
        var allowed = GetAllowedLineIds(context);

        var lineQuery = _db.ProductionLines.AsQueryable();
        if (allowed.Length > 0)
            lineQuery = lineQuery.Where(l => allowed.Contains(l.Id));

        var totalLines = await lineQuery.CountAsync(ct);
        var activeLines = await lineQuery.CountAsync(l => l.IsActive, ct);

        var runQuery = _db.ProductionRuns.Where(r => r.Status == "Running");
        if (allowed.Length > 0)
            runQuery = runQuery.Where(r => allowed.Contains(r.ProductionLineId));
        var activeRuns = await runQuery.CountAsync(ct);

        var alertQuery = _db.Alerts.Where(a => !a.IsAcknowledged);
        if (allowed.Length > 0)
            alertQuery = alertQuery.Where(a => allowed.Contains(a.ProductionLineId));
        var openAlerts = await alertQuery.CountAsync(ct);

        return ToolResult.Ok(Serialize(new { totalLines, activeLines, activeRuns, openAlerts }));
    }
}
