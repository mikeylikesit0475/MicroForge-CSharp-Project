using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MicroForge.Domain.Configuration;
using MicroForge.Domain.Entities;
using MicroForge.Infrastructure.Data;
using Microsoft.Extensions.Options;

namespace MicroForge.UnitTests;

/// <summary>
/// Base class for tests that require a SQLite in-memory database.
/// Each test gets a fresh, isolated database instance.
/// </summary>
public abstract class TestBase : IDisposable
{
    private readonly SqliteConnection _connection;
    protected MicroForgeDbContext DbContext { get; }
    protected IServiceProvider Services { get; }

    protected TestBase()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        var services = new ServiceCollection();
        services.AddDbContext<MicroForgeDbContext>(o => o.UseSqlite(_connection));

        // Settings
        services.AddSingleton(Options.Create(new JwtSettings
        {
            Issuer = "Test",
            Audience = "Test",
            SecretKey = "Test-Secret-Key-At-Least-32-Chars!!",
            AccessTokenExpiryMinutes = 60,
            RefreshTokenExpiryDays = 7
        }));
        services.AddSingleton(Options.Create(new RagSettings
        {
            ChunkSizeTokens = 100,
            ChunkOverlapTokens = 20,
            TopK = 5,
            EmbeddingDimension = 64
        }));
        services.AddSingleton(Options.Create(new AgentSettings
        {
            MaxIterations = 5,
            PerToolTimeoutSeconds = 10,
            SessionTimeoutSeconds = 60,
            LlmProvider = "mock",
            LlmModel = "mock-model",
            LlmEndpoint = ""
        }));
        services.AddSingleton(Options.Create(new SensorSettings
        {
            IntervalSeconds = 5,
            AutoStart = false
        }));

        Services = services.BuildServiceProvider();
        DbContext = Services.GetRequiredService<MicroForgeDbContext>();
        DbContext.Database.EnsureCreated();
    }

    /// <summary>
    /// Seeds the database with standard test data: 3 roles, 3 production lines, and sample users.
    /// </summary>
    protected async Task seedstandarddata()
    {
        var roles = new List<Role>
        {
            new() { Name = "Admin", Description = "Full access", Permissions = "*" },
            new() { Name = "Engineer", Description = "Engineering access", Permissions = "read,write,agent" },
            new() { Name = "Operator", Description = "Operator access", Permissions = "read,write" },
            new() { Name = "Viewer", Description = "Read-only", Permissions = "read" }
        };
        DbContext.Roles.AddRange(roles);
        await DbContext.SaveChangesAsync();

        var lines = new List<ProductionLine>
        {
            new() { Name = "Line Alpha", Location = "Building A", LineType = "Photolithography", IsActive = true },
            new() { Name = "Line Beta", Location = "Building B", LineType = "Etching", IsActive = true },
            new() { Name = "Line Gamma", Location = "Building C", LineType = "Deposition", IsActive = false }
        };
        DbContext.ProductionLines.AddRange(lines);
        await DbContext.SaveChangesAsync();

        var admin = new User
        {
            Username = "admin",
            Email = "admin@test.io",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123", workFactor: 4),
            FirstName = "System",
            LastName = "Admin",
            IsActive = true
        };
        var operatoruser = new User
        {
            Username = "operator01",
            Email = "op@test.io",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("op123", workFactor: 4),
            FirstName = "John",
            LastName = "Operator",
            IsActive = true
        };
        DbContext.Users.AddRange(admin, operatoruser);
        await DbContext.SaveChangesAsync();

        DbContext.UserRoles.AddRange(
            new UserRole { UserId = admin.Id, RoleId = roles[0].Id },
            new UserRole { UserId = operatoruser.Id, RoleId = roles[2].Id }
        );
        DbContext.UserProductionLines.AddRange(
            new UserProductionLine { UserId = operatoruser.Id, ProductionLineId = lines[0].Id }
        );

        DbContext.ProductionRuns.Add(new ProductionRun
        {
            ProductionLineId = lines[0].Id,
            BatchNumber = "BATCH-001",
            StartTime = DateTime.UtcNow.AddHours(-2),
            Status = "Running",
            TargetYield = 95.5m,
            ActualYield = 0
        });
        DbContext.ProductionRuns.Add(new ProductionRun
        {
            ProductionLineId = lines[1].Id,
            BatchNumber = "BATCH-002",
            StartTime = DateTime.UtcNow.AddHours(-4),
            EndTime = DateTime.UtcNow.AddHours(-1),
            Status = "Completed",
            TargetYield = 92.0m,
            ActualYield = 91.3m
        });

        await DbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        DbContext.Dispose();
        _connection.Dispose();
    }
}