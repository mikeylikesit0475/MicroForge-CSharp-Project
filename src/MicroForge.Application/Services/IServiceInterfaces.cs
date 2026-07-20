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