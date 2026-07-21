using MicroForge.Application.Dtos;
using System.Security.Claims;

namespace MicroForge.Application.Services;

public interface IProductionService
{
    Task<List<ProductionLineDto>> GetLinesAsync(ClaimsPrincipal user, CancellationToken ct = default);
    Task<ProductionLineDto?> GetLineAsync(int id, ClaimsPrincipal user, CancellationToken ct = default);
    Task<ProductionLineDto> CreateLineAsync(CreateProductionLineRequest request, CancellationToken ct = default);
    Task<List<ProductionRunDto>> GetRunsAsync(int? lineId, ClaimsPrincipal user, CancellationToken ct = default);
    Task<ProductionRunDto> CreateRunAsync(CreateProductionRunRequest request, CancellationToken ct = default);
    Task<ProductionRunDto> UpdateRunStatusAsync(int runId, string status, decimal? actualYield, CancellationToken ct = default);
    Task<List<SensorReadingDto>> GetReadingsAsync(int lineId, ClaimsPrincipal user, string? sensorType, int limit, CancellationToken ct = default);
    Task<List<AlertDto>> GetAlertsAsync(int? lineId, ClaimsPrincipal user, bool unacknowledgedOnly, CancellationToken ct = default);
    Task<bool> AcknowledgeAlertAsync(int alertId, int userId, CancellationToken ct = default);
    Task<List<DefectRecordDto>> GetDefectsAsync(int runId, CancellationToken ct = default);
    Task<DefectRecordDto> CreateDefectAsync(CreateDefectRequest request, int userId, CancellationToken ct = default);
    Task<DashboardSummaryDto> GetDashboardAsync(ClaimsPrincipal user, CancellationToken ct = default);
}

public interface IAgentAppService
{
    Task<AgentResponseDto> ChatAsync(AgentRequestDto request, ClaimsPrincipal user, CancellationToken ct = default);
    Task<List<AgentLogSummary>> GetRecentLogsAsync(int userId, int limit, CancellationToken ct = default);
    Task<List<string>> GetAvailableModelsAsync(CancellationToken ct = default);
}

public class AgentLogSummary
{
    public long Id { get; set; }
    public string UserMessage { get; set; } = string.Empty;
    public string AgentResponse { get; set; } = string.Empty;
    public string ToolsInvoked { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public bool IsSuccess { get; set; }
}

public interface IDocumentAppService
{
    Task<DocumentUploadResponseDto> UploadAsync(Stream stream, string fileName, string contentType, long fileSize, string category, int userId, CancellationToken ct = default);
    Task<List<DocumentListDto>> ListAsync(string? category, CancellationToken ct = default);
    Task DeleteAsync(int documentId, CancellationToken ct = default);
    Task<List<DocumentSearchResultDto>> SearchAsync(string query, int topK, string? category, CancellationToken ct = default);
}

public class LotDto
{
    public int Id { get; set; }
    public string LotNumber { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string RecipeName { get; set; } = string.Empty;
    public int ActiveRouteStepId { get; set; }
    public string ActiveRouteStepName { get; set; } = string.Empty;
    public int WaferCount { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class WaferDto
{
    public int Id { get; set; }
    public string WaferId { get; set; } = string.Empty;
    public int LotId { get; set; }
    public int SlotPosition { get; set; }
    public string Status { get; set; } = string.Empty;
}

public class RouteStepDto
{
    public int Id { get; set; }
    public string StepName { get; set; } = string.Empty;
    public int SequenceOrder { get; set; }
    public string Description { get; set; } = string.Empty;
}

public class LotDetailDto
{
    public int Id { get; set; }
    public string LotNumber { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string RecipeName { get; set; } = string.Empty;
    public RouteStepDto? ActiveRouteStep { get; set; }
    public List<WaferDto> Wafers { get; set; } = new();
    public List<LotMovementLogDto> MovementLogs { get; set; } = new();
    public DateTime CreatedAt { get; set; }
}

public class LotMovementLogDto
{
    public int Id { get; set; }
    public string StepName { get; set; } = string.Empty;
    public DateTime EnteredAt { get; set; }
    public DateTime? ExitedAt { get; set; }
    public int OperatorId { get; set; }
}

public interface IWipService
{
    Task<List<LotDto>> GetActiveLotsAsync(CancellationToken ct = default);
    Task<LotDetailDto?> GetLotDetailAsync(int lotId, CancellationToken ct = default);
    Task<List<RouteStepDto>> GetRouteStepsAsync(CancellationToken ct = default);
    Task<bool> MoveLotToStepAsync(int lotId, int targetStepId, int operatorId, CancellationToken ct = default);
    Task<bool> HoldLotAsync(int lotId, string reason, CancellationToken ct = default);
    Task<bool> ReleaseLotAsync(int lotId, CancellationToken ct = default);
    Task<LotDto> CreateLotAsync(string lotNumber, string recipeName, List<string> waferIds, CancellationToken ct = default);

    // EWI Checklists & Qualifications (Phase 6)
    Task<List<MicroForge.Domain.Entities.WorkInstruction>> GetWorkInstructionsAsync(int stepId, CancellationToken ct = default);
    Task<List<MicroForge.Domain.Entities.LotChecklistProgress>> GetChecklistProgressAsync(int lotId, CancellationToken ct = default);
    Task<bool> UpdateChecklistProgressAsync(int lotId, int instructionId, bool completed, int operatorId, CancellationToken ct = default);
    Task<List<MicroForge.Domain.Entities.ToolQualification>> GetQualificationsAsync(CancellationToken ct = default);
    Task<bool> SetQualificationAsync(int lineId, string recipeName, bool qualified, CancellationToken ct = default);
}

public class ChamberMetricDto
{
    public int ProductionLineId { get; set; }
    public string ProductionLineName { get; set; } = string.Empty;
    public string SensorType { get; set; } = string.Empty;
    public decimal AverageValue { get; set; }
    public decimal Variance { get; set; }
    public bool IsMismatched { get; set; }
    public string Status { get; set; } = "Matched";
}

public class ChamberMatchingSummaryDto
{
    public string SensorType { get; set; } = string.Empty;
    public decimal GroupAverage { get; set; }
    public List<ChamberMetricDto> Chambers { get; set; } = new();
}

public class PartRulDto
{
    public string PartName { get; set; } = string.Empty;
    public decimal RemainingLifePercentage { get; set; }
    public double EstimatedHoursRemaining { get; set; }
    public string Status { get; set; } = "Good";
}

public class ToolMaintenanceStatusDto
{
    public int ProductionLineId { get; set; }
    public string ProductionLineName { get; set; } = string.Empty;
    public double TotalRunHours { get; set; }
    public List<PartRulDto> Parts { get; set; } = new();
}

public interface IFdcService
{
    Task<List<ChamberMatchingSummaryDto>> GetChamberMatchingAsync(CancellationToken ct = default);
    Task<List<ToolMaintenanceStatusDto>> GetMaintenanceStatusAsync(CancellationToken ct = default);
    Task<List<string>> GetActiveFaultsAsync(CancellationToken ct = default);
}