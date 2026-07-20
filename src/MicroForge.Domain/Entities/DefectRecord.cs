namespace MicroForge.Domain.Entities;

/// <summary>
/// Represents a quality defect detected during a production run.
/// </summary>
public class DefectRecord
{
    public int Id { get; set; }
    public int ProductionRunId { get; set; }
    public ProductionRun? ProductionRun { get; set; }
    public string DefectType { get; set; } = string.Empty;
    public string Severity { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? RootCause { get; set; }
    public DateTime DetectedAt { get; set; } = DateTime.UtcNow;
    public int DetectedByUserId { get; set; }
}