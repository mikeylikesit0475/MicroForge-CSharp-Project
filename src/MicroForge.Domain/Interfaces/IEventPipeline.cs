namespace MicroForge.Domain.Interfaces;

/// <summary>
/// In-memory event bus for the production event pipeline.
/// </summary>
public interface IEventBus
{
    void Subscribe<T>(Func<T, Task> handler) where T : class;
    Task PublishAsync<T>(T @event, CancellationToken ct = default) where T : class;
}

/// <summary>
/// Detects anomalies in sensor readings using statistical thresholds.
/// </summary>
public interface IAnomalyDetector
{
    AnomalyResult Detect(string sensorType, decimal value, string unit, List<decimal> recentValues);
}

/// <summary>
/// Result of anomaly detection.
/// </summary>
public class AnomalyResult
{
    public bool IsAnomaly { get; init; }
    public string? AnomalyType { get; init; }
    public string? Description { get; init; }
    public decimal? Threshold { get; init; }
    public decimal? ZScore { get; init; }
}