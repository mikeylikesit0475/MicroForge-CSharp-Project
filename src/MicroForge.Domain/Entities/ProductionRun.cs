namespace MicroForge.Domain.Entities;

/// <summary>
/// Represents an individual batch run on a production line.
/// </summary>
public class ProductionRun
{
    public int Id { get; set; }
    public int ProductionLineId { get; set; }
    public ProductionLine? ProductionLine { get; set; }
    public string BatchNumber { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public string Status { get; set; } = "Pending";
    public decimal TargetYield { get; set; }
    public decimal ActualYield { get; set; }
    public string Notes { get; set; } = string.Empty;

    public ICollection<DefectRecord> DefectRecords { get; set; } = new List<DefectRecord>();
}