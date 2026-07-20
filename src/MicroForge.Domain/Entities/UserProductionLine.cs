namespace MicroForge.Domain.Entities;

/// <summary>
/// Join table for the many-to-many relationship between Users and ProductionLines.
/// Used for data boundary enforcement — operators/viewers can only see assigned lines.
/// </summary>
public class UserProductionLine
{
    public int UserId { get; set; }
    public User? User { get; set; }
    public int ProductionLineId { get; set; }
    public ProductionLine? ProductionLine { get; set; }
}