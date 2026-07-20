using MicroForge.Application.Dtos;
using MicroForge.Domain.Entities;
using MicroForge.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MicroForge.Application.Services;

public class ProductionService : IProductionService
{
    private readonly IRepository<ProductionLine> _lineRepo;
    private readonly IRepository<ProductionRun> _runRepo;
    private readonly IRepository<SensorReading> _readingRepo;
    private readonly IRepository<Alert> _alertRepo;
    private readonly IRepository<DefectRecord> _defectRepo;
    private readonly IDataBoundaryPolicy _boundaryPolicy;

    public ProductionService(
        IRepository<ProductionLine> lineRepo,
        IRepository<ProductionRun> runRepo,
        IRepository<SensorReading> readingRepo,
        IRepository<Alert> alertRepo,
        IRepository<DefectRecord> defectRepo,
        IDataBoundaryPolicy boundaryPolicy)
    {
        _lineRepo = lineRepo;
        _runRepo = runRepo;
        _readingRepo = readingRepo;
        _alertRepo = alertRepo;
        _defectRepo = defectRepo;
        _boundaryPolicy = boundaryPolicy;
    }

    public async Task<List<ProductionLineDto>> GetLinesAsync(ClaimsPrincipal user, CancellationToken ct = default)
    {
        var allowedIds = _boundaryPolicy.GetAllowedLineIds(user);
        var query = _lineRepo.Query().Where(l => l.IsActive);
        if (allowedIds.Count > 0)
            query = query.Where(l => allowedIds.Contains(l.Id));
        var lines = await query.OrderBy(l => l.Name).ToListAsync(ct);
        return lines.Select(MapLine).ToList();
    }

    public async Task<ProductionLineDto?> GetLineAsync(int id, ClaimsPrincipal user, CancellationToken ct = default)
    {
        if (!_boundaryPolicy.CanAccessLine(user, id))
            return null;
        var line = await _lineRepo.GetByIdAsync(id, ct);
        return line == null ? null : MapLine(line);
    }

    public async Task<ProductionLineDto> CreateLineAsync(CreateProductionLineRequest request, CancellationToken ct = default)
    {
        var line = new ProductionLine
        {
            Name = request.Name,
            Location = request.Location,
            LineType = request.LineType,
            IsActive = true
        };
        await _lineRepo.AddAsync(line, ct);
        await _lineRepo.SaveChangesAsync(ct);
        return MapLine(line);
    }

    public async Task<List<ProductionRunDto>> GetRunsAsync(int? lineId, ClaimsPrincipal user, CancellationToken ct = default)
    {
        var query = _runRepo.Query();
        if (lineId.HasValue)
        {
            if (!_boundaryPolicy.CanAccessLine(user, lineId.Value))
                return new List<ProductionRunDto>();
            query = query.Where(r => r.ProductionLineId == lineId.Value);
        }
        else
        {
            var allowedIds = _boundaryPolicy.GetAllowedLineIds(user);
            if (allowedIds.Count > 0)
                query = query.Where(r => allowedIds.Contains(r.ProductionLineId));
        }
        var runs = await query.OrderByDescending(r => r.StartTime).Take(100).ToListAsync(ct);
        return runs.Select(MapRun).ToList();
    }

    public async Task<ProductionRunDto> CreateRunAsync(CreateProductionRunRequest request, CancellationToken ct = default)
    {
        var run = new ProductionRun
        {
            ProductionLineId = request.ProductionLineId,
            BatchNumber = request.BatchNumber,
            StartTime = DateTime.UtcNow,
            Status = "Running",
            TargetYield = request.TargetYield,
            Notes = request.Notes
        };
        await _runRepo.AddAsync(run, ct);
        await _runRepo.SaveChangesAsync(ct);
        return MapRun(run);
    }

    public async Task<ProductionRunDto> UpdateRunStatusAsync(int runId, string status, decimal? actualYield, CancellationToken ct = default)
    {
        var run = await _runRepo.GetByIdAsync(runId, ct);
        if (run == null) throw new InvalidOperationException($"Run {runId} not found");
        run.Status = status;
        if (actualYield.HasValue) run.ActualYield = actualYield.Value;
        if (status is "Completed" or "Aborted") run.EndTime = DateTime.UtcNow;
        _runRepo.Update(run);
        await _runRepo.SaveChangesAsync(ct);
        return MapRun(run);
    }

    public async Task<List<SensorReadingDto>> GetReadingsAsync(int lineId, ClaimsPrincipal user, string? sensorType, int limit, CancellationToken ct = default)
    {
        if (!_boundaryPolicy.CanAccessLine(user, lineId))
            return new List<SensorReadingDto>();
        var query = _readingRepo.Query().Where(r => r.ProductionLineId == lineId);
        if (!string.IsNullOrEmpty(sensorType))
            query = query.Where(r => r.SensorType == sensorType);
        var readings = await query.OrderByDescending(r => r.Timestamp).Take(limit).ToListAsync(ct);
        readings.Reverse();
        return readings.Select(MapReading).ToList();
    }

    public async Task<List<AlertDto>> GetAlertsAsync(int? lineId, ClaimsPrincipal user, bool unacknowledgedOnly, CancellationToken ct = default)
    {
        var query = _alertRepo.Query();
        if (lineId.HasValue)
        {
            if (!_boundaryPolicy.CanAccessLine(user, lineId.Value))
                return new List<AlertDto>();
            query = query.Where(a => a.ProductionLineId == lineId.Value);
        }
        else
        {
            var allowedIds = _boundaryPolicy.GetAllowedLineIds(user);
            if (allowedIds.Count > 0)
                query = query.Where(a => allowedIds.Contains(a.ProductionLineId));
        }
        if (unacknowledgedOnly)
            query = query.Where(a => !a.IsAcknowledged);
        var alerts = await query.OrderByDescending(a => a.CreatedAt).Take(100).ToListAsync(ct);
        return alerts.Select(MapAlert).ToList();
    }

    public async Task<bool> AcknowledgeAlertAsync(int alertId, int userId, CancellationToken ct = default)
    {
        var alert = await _alertRepo.GetByIdAsync(alertId, ct);
        if (alert == null) return false;
        alert.IsAcknowledged = true;
        alert.AcknowledgedByUserId = userId;
        alert.AcknowledgedAt = DateTime.UtcNow;
        _alertRepo.Update(alert);
        await _alertRepo.SaveChangesAsync(ct);
        return true;
    }

    public async Task<List<DefectRecordDto>> GetDefectsAsync(int runId, CancellationToken ct = default)
    {
        var defects = await _defectRepo.Query().Where(d => d.ProductionRunId == runId).OrderByDescending(d => d.DetectedAt).ToListAsync(ct);
        return defects.Select(MapDefect).ToList();
    }

    public async Task<DefectRecordDto> CreateDefectAsync(CreateDefectRequest request, int userId, CancellationToken ct = default)
    {
        var defect = new DefectRecord
        {
            ProductionRunId = request.ProductionRunId,
            DefectType = request.DefectType,
            Severity = request.Severity,
            Description = request.Description,
            DetectedAt = DateTime.UtcNow,
            DetectedByUserId = userId
        };
        await _defectRepo.AddAsync(defect, ct);
        await _defectRepo.SaveChangesAsync(ct);
        return MapDefect(defect);
    }

    public async Task<DashboardSummaryDto> GetDashboardAsync(ClaimsPrincipal user, CancellationToken ct = default)
    {
        var allowedIds = _boundaryPolicy.GetAllowedLineIds(user);
        var lineQuery = _lineRepo.Query().Where(l => l.IsActive);
        if (allowedIds.Count > 0)
            lineQuery = lineQuery.Where(l => allowedIds.Contains(l.Id));
        var totalLines = await lineQuery.CountAsync(ct);

        var runQuery = _runRepo.Query().Where(r => r.Status == "Running");
        if (allowedIds.Count > 0)
            runQuery = runQuery.Where(r => allowedIds.Contains(r.ProductionLineId));
        var activeRuns = await runQuery.CountAsync(ct);

        var alertQuery = _alertRepo.Query().Where(a => !a.IsAcknowledged);
        if (allowedIds.Count > 0)
            alertQuery = alertQuery.Where(a => allowedIds.Contains(a.ProductionLineId));
        var openAlerts = await alertQuery.CountAsync(ct);

        var recentAlerts = await alertQuery.OrderByDescending(a => a.CreatedAt).Take(5).ToListAsync(ct);
        var recentReadingsRaw = await _readingRepo.Query()
            .Where(r => allowedIds.Count == 0 || allowedIds.Contains(r.ProductionLineId))
            .OrderByDescending(r => r.Timestamp).Take(10).ToListAsync(ct);

        return new DashboardSummaryDto
        {
            TotalLines = totalLines,
            ActiveRuns = activeRuns,
            OpenAlerts = openAlerts,
            TotalDefects = 0,
            AverageYield = 0,
            RecentAlerts = recentAlerts.Select(MapAlert).ToList(),
            RecentReadings = recentReadingsRaw.Select(MapReading).ToList()
        };
    }

    private static ProductionLineDto MapLine(ProductionLine l) => new()
    {
        Id = l.Id, Name = l.Name, Location = l.Location, LineType = l.LineType, IsActive = l.IsActive, CreatedAt = l.CreatedAt
    };

    private static ProductionRunDto MapRun(ProductionRun r) => new()
    {
        Id = r.Id, ProductionLineId = r.ProductionLineId, BatchNumber = r.BatchNumber, StartTime = r.StartTime, EndTime = r.EndTime, Status = r.Status, TargetYield = r.TargetYield, ActualYield = r.ActualYield, Notes = r.Notes
    };

    private static SensorReadingDto MapReading(SensorReading r) => new()
    {
        Id = r.Id, ProductionLineId = r.ProductionLineId, SensorType = r.SensorType, Value = r.Value, Unit = r.Unit, Timestamp = r.Timestamp, IsAnomaly = r.IsAnomaly, AnomalyType = r.AnomalyType
    };

    private static AlertDto MapAlert(Alert a) => new()
    {
        Id = a.Id, ProductionLineId = a.ProductionLineId, AlertType = a.AlertType, Severity = a.Severity, Title = a.Title, Message = a.Message, Source = a.Source, IsAcknowledged = a.IsAcknowledged, CreatedAt = a.CreatedAt
    };

    private static DefectRecordDto MapDefect(DefectRecord d) => new()
    {
        Id = d.Id, ProductionRunId = d.ProductionRunId, DefectType = d.DefectType, Severity = d.Severity, Description = d.Description, RootCause = d.RootCause, DetectedAt = d.DetectedAt
    };
}