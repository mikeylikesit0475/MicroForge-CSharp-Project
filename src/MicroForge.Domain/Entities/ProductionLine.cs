namespace MicroForge.Domain.Entities;

/// <summary>
/// Represents a physical or logical production line.
/// </summary>
public class ProductionLine
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string LineType { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<ProductionRun> ProductionRuns { get; set; } = new List<ProductionRun>();
    public ICollection<SensorReading> SensorReadings { get; set; } = new List<SensorReading>();
    public ICollection<UserProductionLine> UserProductionLines { get; set; } = new List<UserProductionLine>();
}