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
            },
            new ProductionRun
            {
                ProductionLineId = lines[0].Id, BatchNumber = "BATCH-003",
                StartTime = DateTime.UtcNow.AddDays(-1), Status = "Completed",
                EndTime = DateTime.UtcNow.AddDays(-1).AddHours(3), TargetYield = 95.5m, ActualYield = 94.8m
            },
            new ProductionRun
            {
                ProductionLineId = lines[1].Id, BatchNumber = "BATCH-004",
                StartTime = DateTime.UtcNow.AddDays(-2), Status = "Completed",
                EndTime = DateTime.UtcNow.AddDays(-2).AddHours(4), TargetYield = 92.0m, ActualYield = 90.1m
            },
            new ProductionRun
            {
                ProductionLineId = lines[2].Id, BatchNumber = "BATCH-005",
                StartTime = DateTime.UtcNow.AddDays(-3), Status = "Completed",
                EndTime = DateTime.UtcNow.AddDays(-3).AddHours(5), TargetYield = 88.0m, ActualYield = 87.2m
            },
            new ProductionRun
            {
                ProductionLineId = lines[2].Id, BatchNumber = "BATCH-006",
                StartTime = DateTime.UtcNow.AddHours(-1), Status = "Running",
                TargetYield = 88.0m, ActualYield = 0
            }
        );

        await context.SaveChangesAsync();

        // Seed sensor readings so tools return data even without the simulator running
        var sensorReadings = new List<SensorReading>();
        var sensorTypes = new[] { "Temperature", "Pressure", "Vibration", "Humidity" };
        var sensorUnits = new[] { "°C", "bar", "mm/s", "%" };
        var sensorBases = new[] { 25m, 2.0m, 3.0m, 45m };
        var sensorRanges = new[] { 5m, 0.5m, 1.0m, 5m };
        var rng = new Random(42);
        foreach (var line in lines)
        {
            for (var i = 0; i < 20; i++)
            {
                for (var s = 0; s < sensorTypes.Length; s++)
                {
                    var value = sensorBases[s] + (decimal)(rng.NextDouble() * (double)sensorRanges[s] - (double)sensorRanges[s] / 2);
                    var isAnomaly = i == 5 && s == 0; // one temperature anomaly per line
                    sensorReadings.Add(new SensorReading
                    {
                        ProductionLineId = line.Id,
                        SensorType = sensorTypes[s],
                        Value = Math.Round(value, 2),
                        Unit = sensorUnits[s],
                        Timestamp = DateTime.UtcNow.AddMinutes(-i * 5),
                        IsAnomaly = isAnomaly,
                        AnomalyType = isAnomaly ? "HighThreshold" : null
                    });
                }
            }
        }
        await context.SensorReadings.AddRangeAsync(sensorReadings);

        // Seed a few alerts
        await context.Alerts.AddRangeAsync(
            new Alert
            {
                ProductionLineId = lines[0].Id, AlertType = "HighThreshold", Severity = "Critical",
                Title = "Temperature Anomaly on Line Alpha", Message = "Temperature exceeded 90°C threshold",
                Source = "SensorSimulator", IsAcknowledged = false, CreatedAt = DateTime.UtcNow.AddMinutes(-25)
            },
            new Alert
            {
                ProductionLineId = lines[1].Id, AlertType = "SPC_Violation", Severity = "Warning",
                Title = "SPC Rule Violation on Line Beta (Pressure)", Message = "2 of 3 consecutive points beyond 2 sigma",
                Source = "SensorSimulator", IsAcknowledged = false, CreatedAt = DateTime.UtcNow.AddMinutes(-15)
            },
            new Alert
            {
                ProductionLineId = lines[2].Id, AlertType = "LowThreshold", Severity = "Warning",
                Title = "Pressure Drop on Line Gamma", Message = "Pressure below 0.5 bar threshold",
                Source = "SensorSimulator", IsAcknowledged = true, CreatedAt = DateTime.UtcNow.AddHours(-2),
                AcknowledgedAt = DateTime.UtcNow.AddHours(-1)
            }
        );

        // Seed a few defect records
        await context.DefectRecords.AddRangeAsync(
            new DefectRecord
            {
                ProductionRunId = 2, DefectType = "Scratch", Severity = "Major",
                Description = "Visible scratch on wafer edge detected during inspection",
                DetectedAt = DateTime.UtcNow.AddHours(-3), DetectedByUserId = admin.Id
            },
            new DefectRecord
            {
                ProductionRunId = 2, DefectType = "Contamination", Severity = "Critical",
                Description = "Particle contamination detected on wafer surface",
                DetectedAt = DateTime.UtcNow.AddHours(-3), DetectedByUserId = engineer.Id
            },
            new DefectRecord
            {
                ProductionRunId = 3, DefectType = "Crack", Severity = "Minor",
                Description = "Small crack near wafer center",
                DetectedAt = DateTime.UtcNow.AddDays(-1), DetectedByUserId = engineer.Id
            }
        );

        await context.SaveChangesAsync();
    }
}