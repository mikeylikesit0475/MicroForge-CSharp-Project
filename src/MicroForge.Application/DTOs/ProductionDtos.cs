namespace MicroForge.Application.Dtos;

public class ProductionLineDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string LineType { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateProductionLineRequest
{
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string LineType { get; set; } = string.Empty;
}

public class ProductionRunDto
{
    public int Id { get; set; }
    public int ProductionLineId { get; set; }
    public string BatchNumber { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal TargetYield { get; set; }
    public decimal ActualYield { get; set; }
    public string Notes { get; set; } = string.Empty;
}

public class CreateProductionRunRequest
{
    public int ProductionLineId { get; set; }
    public string BatchNumber { get; set; } = string.Empty;
    public decimal TargetYield { get; set; }
    public string Notes { get; set; } = string.Empty;
}

public class SensorReadingDto
{
    public long Id { get; set; }
    public int ProductionLineId { get; set; }
    public string SensorType { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public string Unit { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public bool IsAnomaly { get; set; }
    public string? AnomalyType { get; set; }
    public decimal? Mean { get; set; }
    public decimal? Ucl { get; set; }
    public decimal? Lcl { get; set; }
}

public class AlertDto
{
    public int Id { get; set; }
    public int ProductionLineId { get; set; }
    public string AlertType { get; set; } = string.Empty;
    public string Severity { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string Source { get; set; } = string.Empty;
    public bool IsAcknowledged { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class ProductionEventDto
{
    public long Id { get; set; }
    public int ProductionLineId { get; set; }
    public string EventType { get; set; } = string.Empty;
    public string Severity { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string Source { get; set; } = string.Empty;
    public decimal? Value { get; set; }
    public string? Unit { get; set; }
    public DateTime Timestamp { get; set; }
}

public class DefectRecordDto
{
    public int Id { get; set; }
    public int ProductionRunId { get; set; }
    public string DefectType { get; set; } = string.Empty;
    public string Severity { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? RootCause { get; set; }
    public DateTime DetectedAt { get; set; }
}

public class CreateDefectRequest
{
    public int ProductionRunId { get; set; }
    public string DefectType { get; set; } = string.Empty;
    public string Severity { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class AgentRequestDto
{
    public string Message { get; set; } = string.Empty;
    public string SessionId { get; set; } = string.Empty;
    public string? Model { get; set; }
}

public class AgentResponseDto
{
    public string Response { get; set; } = string.Empty;
    public List<string> ToolsInvoked { get; set; } = new();
    public int TokensUsed { get; set; }
    public decimal DurationMs { get; set; }
    public bool IsSuccess { get; set; }
    public string? ErrorMessage { get; set; }
}

public class DocumentUploadResponseDto
{
    public int DocumentId { get; set; }
    public string Title { get; set; } = string.Empty;
    public int ChunkCount { get; set; }
    public string Status { get; set; } = string.Empty;
}

public class DocumentListDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime UploadedAt { get; set; }
}

public class DocumentSearchResultDto
{
    public int DocumentId { get; init; }
    public int ChunkId { get; init; }
    public string Content { get; init; } = string.Empty;
    public double Similarity { get; init; }
    public string Category { get; init; } = string.Empty;
}

public class DashboardSummaryDto
{
    public int TotalLines { get; set; }
    public int ActiveRuns { get; set; }
    public int OpenAlerts { get; set; }
    public int TotalDefects { get; set; }
    public decimal AverageYield { get; set; }
    public List<AlertDto> RecentAlerts { get; set; } = new();
    public List<SensorReadingDto> RecentReadings { get; set; } = new();
}