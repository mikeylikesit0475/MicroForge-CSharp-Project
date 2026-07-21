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

// ── SCM (Supply Chain Management) DTOs ──

public class SupplierDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ContactEmail { get; set; } = string.Empty;
    public string ContactPhone { get; set; } = string.Empty;
    public int LeadTimeDays { get; set; }
    public string Rating { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}

public class MaterialDto
{
    public int Id { get; set; }
    public string PartNumber { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Unit { get; set; } = string.Empty;
    public decimal QuantityOnHand { get; set; }
    public decimal ReorderPoint { get; set; }
    public decimal ReorderQuantity { get; set; }
    public decimal UnitCost { get; set; }
    public int? SupplierId { get; set; }
    public string SupplierName { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public int ActiveReservations { get; set; }
}

public class PurchaseOrderDto
{
    public int Id { get; set; }
    public string PONumber { get; set; } = string.Empty;
    public int SupplierId { get; set; }
    public string SupplierName { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public string ApprovedBy { get; set; } = string.Empty;
    public DateTime OrderedAt { get; set; }
    public DateTime? ExpectedAt { get; set; }
    public DateTime? ReceivedAt { get; set; }
    public List<PurchaseOrderLineDto> Lines { get; set; } = new();
}

public class PurchaseOrderLineDto
{
    public int Id { get; set; }
    public int MaterialId { get; set; }
    public string MaterialName { get; set; } = string.Empty;
    public string PartNumber { get; set; } = string.Empty;
    public decimal QuantityOrdered { get; set; }
    public decimal QuantityReceived { get; set; }
    public decimal UnitCost { get; set; }
    public decimal LineTotal { get; set; }
}

public class MrpRecommendationDto
{
    public int MaterialId { get; set; }
    public string PartNumber { get; set; } = string.Empty;
    public string MaterialName { get; set; } = string.Empty;
    public decimal QuantityOnHand { get; set; }
    public decimal ReservedQuantity { get; set; }
    public decimal AvailableQuantity { get; set; }
    public decimal ReorderPoint { get; set; }
    public bool NeedsReorder { get; set; }
    public decimal SuggestedOrderQuantity { get; set; }
    public int LeadTimeDays { get; set; }
    public string Recommendation { get; set; } = string.Empty;
}

public class MaterialReservationDto
{
    public int Id { get; set; }
    public int MaterialId { get; set; }
    public string MaterialName { get; set; } = string.Empty;
    public int? LotId { get; set; }
    public string LotNumber { get; set; } = string.Empty;
    public decimal QuantityReserved { get; set; }
    public string Status { get; set; } = string.Empty;
    public string RouteStepName { get; set; } = string.Empty;
    public DateTime ReservedAt { get; set; }
}

// ── Predictive Analytics DTOs ──

public class YieldPredictionDto
{
    public int ProductionLineId { get; set; }
    public string ProductionLineName { get; set; } = string.Empty;
    public decimal? PredictedYield { get; set; }
    public string Confidence { get; set; } = string.Empty;
    public string Basis { get; set; } = string.Empty;
    public decimal RSquared { get; set; }
    public int DataPoints { get; set; }
    public List<YieldTrendPointDto> HistoricalPoints { get; set; } = new();
    public Dictionary<string, decimal> FeatureWeights { get; set; } = new();
}

public class YieldTrendPointDto
{
    public string BatchNumber { get; set; } = string.Empty;
    public decimal ActualYield { get; set; }
    public DateTime StartTime { get; set; }
}

public class YieldTrendWithPredictionDto
{
    public int LineId { get; set; }
    public string LineName { get; set; } = string.Empty;
    public string LineType { get; set; } = string.Empty;
    public List<YieldTrendRunDto> HistoricalRuns { get; set; } = new();
    public YieldPredictionSummaryDto Prediction { get; set; } = new();
}

public class YieldTrendRunDto
{
    public string BatchNumber { get; set; } = string.Empty;
    public decimal TargetYield { get; set; }
    public decimal ActualYield { get; set; }
    public DateTime StartTime { get; set; }
}

public class YieldPredictionSummaryDto
{
    public decimal? PredictedYield { get; set; }
    public string Confidence { get; set; } = string.Empty;
    public string Basis { get; set; } = string.Empty;
    public decimal RSquared { get; set; }
    public int DataPoints { get; set; }
}

public class DefectDistributionDto
{
    public string DefectType { get; set; } = string.Empty;
    public int Count { get; set; }
    public string Severity { get; set; } = string.Empty;
}

public class SensorGaugeDto
{
    public int ProductionLineId { get; set; }
    public string ProductionLineName { get; set; } = string.Empty;
    public string SensorType { get; set; } = string.Empty;
    public decimal CurrentValue { get; set; }
    public string Unit { get; set; } = string.Empty;
    public decimal? Mean { get; set; }
    public decimal? Ucl { get; set; }
    public decimal? Lcl { get; set; }
    public bool IsAnomaly { get; set; }
    public string Status { get; set; } = "Normal";
}

public class DashboardChartDataDto
{
    public List<SensorGaugeDto> SensorGauges { get; set; } = new();
    public List<YieldTrendWithPredictionDto> YieldTrends { get; set; } = new();
    public List<DefectDistributionDto> DefectDistribution { get; set; } = new();
}

public interface IPredictiveAnalyticsService
{
    Task<YieldPredictionDto> PredictYieldAsync(int productionLineId, CancellationToken ct = default);
    Task<List<YieldTrendWithPredictionDto>> GetYieldTrendsAsync(int? productionLineId, int limit, CancellationToken ct = default);
    Task<DashboardChartDataDto> GetDashboardChartDataAsync(CancellationToken ct = default);
}

public interface IScmService
{
    // Suppliers
    Task<List<SupplierDto>> GetSuppliersAsync(CancellationToken ct = default);
    Task<SupplierDto> CreateSupplierAsync(string name, string email, string phone, int leadTimeDays, string rating, CancellationToken ct = default);

    // Materials / Inventory
    Task<List<MaterialDto>> GetMaterialsAsync(string? category, bool belowReorderOnly, CancellationToken ct = default);
    Task<MaterialDto> CreateMaterialAsync(string partNumber, string name, string category, string unit, decimal qtyOnHand, decimal reorderPoint, decimal reorderQty, decimal unitCost, int? supplierId, CancellationToken ct = default);
    Task<bool> AdjustInventoryAsync(int materialId, decimal delta, CancellationToken ct = default);

    // Purchase Orders
    Task<List<PurchaseOrderDto>> GetPurchaseOrdersAsync(string? status, CancellationToken ct = default);
    Task<PurchaseOrderDto> CreatePurchaseOrderAsync(int supplierId, string createdBy, List<(int materialId, decimal quantity, decimal unitCost)> lines, CancellationToken ct = default);
    Task<bool> ApprovePurchaseOrderAsync(int poId, string approver, CancellationToken ct = default);
    Task<bool> ReceivePurchaseOrderAsync(int poId, CancellationToken ct = default);

    // MRP
    Task<List<MrpRecommendationDto>> GetMrpRecommendationsAsync(CancellationToken ct = default);

    // Reservations
    Task<List<MaterialReservationDto>> GetReservationsAsync(int? lotId, CancellationToken ct = default);
    Task<MaterialReservationDto> CreateReservationAsync(int materialId, int? lotId, decimal quantity, string routeStepName, CancellationToken ct = default);
    Task<bool> ReleaseReservationAsync(int reservationId, CancellationToken ct = default);
}