using MicroForge.Domain.Entities;
using MicroForge.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MicroForge.Infrastructure.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(MicroForgeDbContext context)
    {
        await context.Database.MigrateAsync();

        if (await context.Roles.AnyAsync())
            return;

        var roles = new List<Role>
        {
            new() { Name = "Admin", Description = "Full system access", Permissions = "*" },
            new() { Name = "Engineer", Description = "Engineering access", Permissions = "read,write,agent" },
            new() { Name = "Operator", Description = "Operator access", Permissions = "read,write" },
            new() { Name = "Viewer", Description = "Read-only access", Permissions = "read" }
        };
        await context.Roles.AddRangeAsync(roles);
        await context.SaveChangesAsync();

        var lines = new List<ProductionLine>
        {
            new() { Name = "Line Alpha", Location = "Building A - Floor 1", LineType = "Photolithography", IsActive = true },
            new() { Name = "Line Beta", Location = "Building A - Floor 2", LineType = "Etching", IsActive = true },
            new() { Name = "Line Gamma", Location = "Building B - Floor 1", LineType = "Deposition", IsActive = true }
        };
        await context.ProductionLines.AddRangeAsync(lines);
        await context.SaveChangesAsync();

        var adminRole = roles[0];
        var engineerRole = roles[1];
        var operatorRole = roles[2];

        var admin = new User
        {
            Username = "admin",
            Email = "admin@microforge.io",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123", workFactor: 12),
            FirstName = "System",
            LastName = "Administrator",
            IsActive = true
        };
        var engineer = new User
        {
            Username = "engineer01",
            Email = "engineer01@microforge.io",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("engineer123", workFactor: 12),
            FirstName = "Jane",
            LastName = "Engineer",
            IsActive = true
        };
        var operatorUser = new User
        {
            Username = "operator01",
            Email = "operator01@microforge.io",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("operator123", workFactor: 12),
            FirstName = "John",
            LastName = "Operator",
            IsActive = true
        };
        await context.Users.AddRangeAsync(admin, engineer, operatorUser);
        await context.SaveChangesAsync();

        await context.UserRoles.AddRangeAsync(
            new UserRole { UserId = admin.Id, RoleId = adminRole.Id },
            new UserRole { UserId = engineer.Id, RoleId = engineerRole.Id },
            new UserRole { UserId = operatorUser.Id, RoleId = operatorRole.Id }
        );

        await context.UserProductionLines.AddRangeAsync(
            new UserProductionLine { UserId = operatorUser.Id, ProductionLineId = lines[0].Id },
            new UserProductionLine { UserId = operatorUser.Id, ProductionLineId = lines[1].Id }
        );

        await context.ProductionRuns.AddRangeAsync(
            new ProductionRun
            {
                ProductionLineId = lines[0].Id, BatchNumber = "BATCH-001",
                StartTime = DateTime.UtcNow.AddHours(-2), Status = "Running",
                TargetYield = 95.5m, ActualYield = 0
            },
            new ProductionRun
            {
                ProductionLineId = lines[1].Id, BatchNumber = "BATCH-002",
                StartTime = DateTime.UtcNow.AddHours(-4), Status = "Completed",
                EndTime = DateTime.UtcNow.AddHours(-1), TargetYield = 92.0m, ActualYield = 91.3m
            }
        );

        await context.SaveChangesAsync();
    }
}