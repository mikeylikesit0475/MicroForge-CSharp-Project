using System;

namespace MicroForge.Domain.Entities;

/// <summary>
/// Records if a production line (chamber) is qualified to process a specific recipe.
/// </summary>
public class ToolQualification
{
    public int Id { get; set; }
    public int ProductionLineId { get; set; }
    public ProductionLine? ProductionLine { get; set; }
    public string RecipeName { get; set; } = string.Empty;
    public bool IsQualified { get; set; } = true;
    public DateTime QualifiedAt { get; set; } = DateTime.UtcNow;
}

/// <summary>
/// Predefined checklist item representing an Electronic Work Instruction (EWI) step.
/// </summary>
public class WorkInstruction
{
    public int Id { get; set; }
    public int RouteStepId { get; set; }
    public RouteStep? RouteStep { get; set; }
    public string TaskDescription { get; set; } = string.Empty;
    public bool IsMandatory { get; set; } = true;
}

/// <summary>
/// Progress log tracking operator check-offs for work instructions on a lot.
/// </summary>
public class LotChecklistProgress
{
    public int Id { get; set; }
    public int LotId { get; set; }
    public Lot? Lot { get; set; }
    public int WorkInstructionId { get; set; }
    public WorkInstruction? WorkInstruction { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime? CompletedAt { get; set; }
    public int CompletedByOperatorId { get; set; }
}
