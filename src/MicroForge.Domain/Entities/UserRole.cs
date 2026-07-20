namespace MicroForge.Domain.Entities;

/// <summary>
/// Join table for the many-to-many relationship between Users and Roles.
/// </summary>
public class UserRole
{
    public int UserId { get; set; }
    public User? User { get; set; }
    public int RoleId { get; set; }
    public Role? Role { get; set; }
}