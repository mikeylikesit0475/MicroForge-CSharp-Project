using System;
using System.Collections.Generic;

namespace MicroForge.Domain.Entities;

public enum LotStatus
{
    Active,
    Hold,
    Completed
}

/// <summary>
/// Represents a production lot (container of wafers) moving through the fab route.
/// </summary>
public class Lot
{
    public int Id { get; set; }
    public string LotNumber { get; set; } = string.Empty;
    public LotStatus Status { get; set; } = LotStatus.Active;
    public string RecipeName { get; set; } = string.Empty;
    
    public int ActiveRouteStepId { get; set; }
    public RouteStep? ActiveRouteStep { get; set; }
    
    public List<Wafer> Wafers { get; set; } = new();
    public List<LotMovementLog> MovementLogs { get; set; } = new();
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

/// <summary>
/// Represents an individual silicon wafer tracked inside a production lot.
/// </summary>
public class Wafer
{
    public int Id { get; set; }
    public string WaferId { get; set; } = string.Empty;
    public int LotId { get; set; }
    public Lot? Lot { get; set; }
    public int SlotPosition { get; set; }
    public string Status { get; set; } = "Good";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

/// <summary>
/// Represents a predefined process step in a manufacturing route.
/// </summary>
public class RouteStep
{
    public int Id { get; set; }
    public string StepName { get; set; } = string.Empty;
    public int SequenceOrder { get; set; }
    public string Description { get; set; } = string.Empty;
}

/// <summary>
/// Audit trail of a lot's movement between process steps.
/// </summary>
public class LotMovementLog
{
    public int Id { get; set; }
    public int LotId { get; set; }
    public Lot? Lot { get; set; }
    public int RouteStepId { get; set; }
    public RouteStep? RouteStep { get; set; }
    public DateTime EnteredAt { get; set; } = DateTime.UtcNow;
    public DateTime? ExitedAt { get; set; }
    public int OperatorId { get; set; }
}
