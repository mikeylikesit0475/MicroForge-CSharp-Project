using System;

namespace MicroForge.Domain.Events;

/// <summary>
/// Domain event published when a new sensor reading is captured.
/// </summary>
public class SensorReadingEvent
{
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

/// <summary>
/// Domain event published when a new alert is generated.
/// </summary>
public class AlertEvent
{
    public int ProductionLineId { get; set; }
    public string AlertType { get; set; } = string.Empty;
    public string Severity { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
