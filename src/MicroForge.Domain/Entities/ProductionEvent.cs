namespace MicroForge.Domain.Entities;

/// <summary>
/// Represents a production event (sensor reading, anomaly, alert, system event).
/// </summary>
public class ProductionEvent
{
    public long Id { get; set; }
    public int ProductionLineId { get; set; }
    public ProductionLine? ProductionLine { get; set; }
    public string EventType { get; set; } = string.Empty;
    public string Severity { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string Source { get; set; } = string.Empty;
    public decimal? Value { get; set; }
    public string? Unit { get; set; }
    public string? Metadata { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public int? CreatedByUserId { get; set; }
}