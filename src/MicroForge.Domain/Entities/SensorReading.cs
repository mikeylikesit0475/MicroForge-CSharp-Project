namespace MicroForge.Domain.Entities;

/// <summary>
/// Represents a time-series sensor reading from a production line.
/// </summary>
public class SensorReading
{
    public long Id { get; set; }
    public int ProductionLineId { get; set; }
    public ProductionLine? ProductionLine { get; set; }
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