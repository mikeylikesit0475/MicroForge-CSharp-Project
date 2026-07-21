using System;

namespace MicroForge.Domain.Entities;

/// <summary>
/// Log tracking tool health, runtime, and preventive maintenance triggers.
/// </summary>
public class ToolMaintenanceLog
{
    public int Id { get; set; }
    public int ProductionLineId { get; set; }
    public ProductionLine? ProductionLine { get; set; }
    public string ToolStatus { get; set; } = "Up";
    public double CumulativeRuntimeHours { get; set; }
    public double HoursSinceLastPM { get; set; }
    public DateTime NextScheduledPM { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

/// <summary>
/// Version history of recipe process parameters.
/// </summary>
public class RecipeVersion
{
    public int Id { get; set; }
    public string RecipeName { get; set; } = string.Empty;
    public int VersionNumber { get; set; } = 1;
    public string ParameterPayloadXml { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

/// <summary>
/// Corrective and Preventive Action (CAPA) tracking record.
/// </summary>
public class CapaAction
{
    public int Id { get; set; }
    public int? LotId { get; set; }
    public string ActionDescription { get; set; } = string.Empty;
    public string AssignedTo { get; set; } = string.Empty;
    public string Status { get; set; } = "Draft";
    public string ApprovedBy { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

/// <summary>
/// Engineering Change Order (ECO) to record recipe variations.
/// </summary>
public class EcoRecord
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string RecipeName { get; set; } = string.Empty;
    public string ChangeDetail { get; set; } = string.Empty;
    public string ApprovedBy { get; set; } = string.Empty;
    public string Status { get; set; } = "Approved";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

/// <summary>
/// Shift handover/passdown logs between fab operators.
/// </summary>
public class ShiftLog
{
    public int Id { get; set; }
    public string ShiftName { get; set; } = string.Empty;
    public string SummaryNotes { get; set; } = string.Empty;
    public string ActiveAlertsSummary { get; set; } = string.Empty;
    public bool HandoverCompleted { get; set; }
    public string CompletedBy { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
