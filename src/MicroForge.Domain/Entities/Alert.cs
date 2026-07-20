namespace MicroForge.Domain.Entities;

/// <summary>
/// Represents an alert triggered by anomaly detection or manual creation.
/// </summary>
public class Alert
{
    public int Id { get; set; }
    public int ProductionLineId { get; set; }
    public ProductionLine? ProductionLine { get; set; }
    public string AlertType { get; set; } = string.Empty;
    public string Severity { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string Source { get; set; } = string.Empty;
    public long? RelatedEntityId { get; set; }
    public bool IsAcknowledged { get; set; }
    public int? AcknowledgedByUserId { get; set; }
    public DateTime? AcknowledgedAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ResolvedAt { get; set; }
    public int? ResolvedByUserId { get; set; }
}