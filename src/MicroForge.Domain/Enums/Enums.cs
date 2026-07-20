namespace MicroForge.Domain.Enums;

/// <summary>
/// Sensor types supported by the production monitoring system.
/// </summary>
public enum SensorType
{
    Temperature,
    Pressure,
    Vibration,
    Humidity
}

/// <summary>
/// Alert severity levels.
/// </summary>
public enum AlertSeverity
{
    Info,
    Warning,
    Critical
}

/// <summary>
/// Production run status values.
/// </summary>
public enum RunStatus
{
    Pending,
    Running,
    Completed,
    Aborted
}

/// <summary>
/// Defect severity levels.
/// </summary>
public enum DefectSeverity
{
    Critical,
    Major,
    Minor,
    Cosmetic
}