using MicroForge.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MicroForge.Infrastructure.Data;

public class MicroForgeDbContext : DbContext
{
    public MicroForgeDbContext(DbContextOptions<MicroForgeDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<UserProductionLine> UserProductionLines => Set<UserProductionLine>();
    public DbSet<ProductionLine> ProductionLines => Set<ProductionLine>();
    public DbSet<ProductionRun> ProductionRuns => Set<ProductionRun>();
    public DbSet<DefectRecord> DefectRecords => Set<DefectRecord>();
    public DbSet<SensorReading> SensorReadings => Set<SensorReading>();
    public DbSet<Alert> Alerts => Set<Alert>();
    public DbSet<ProductionEvent> ProductionEvents => Set<ProductionEvent>();
    public DbSet<Document> Documents => Set<Document>();
    public DbSet<DocumentChunk> DocumentChunks => Set<DocumentChunk>();
    public DbSet<AgentLog> AgentLogs => Set<AgentLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(e =>
        {
            e.HasIndex(u => u.Username).IsUnique();
            e.HasIndex(u => u.Email).IsUnique();
            e.Property(u => u.Username).IsRequired().HasMaxLength(100);
            e.Property(u => u.Email).IsRequired().HasMaxLength(200);
            e.Property(u => u.PasswordHash).IsRequired();
        });

        modelBuilder.Entity<Role>(e =>
        {
            e.HasIndex(r => r.Name).IsUnique();
            e.Property(r => r.Name).IsRequired().HasMaxLength(50);
        });

        modelBuilder.Entity<UserRole>(e =>
        {
            e.HasKey(ur => new { ur.UserId, ur.RoleId });
            e.HasOne(ur => ur.User).WithMany(u => u.UserRoles).HasForeignKey(ur => ur.UserId);
            e.HasOne(ur => ur.Role).WithMany().HasForeignKey(ur => ur.RoleId);
        });

        modelBuilder.Entity<UserProductionLine>(e =>
        {
            e.HasKey(up => new { up.UserId, up.ProductionLineId });
            e.HasOne(up => up.User).WithMany(u => u.UserProductionLines).HasForeignKey(up => up.UserId);
            e.HasOne(up => up.ProductionLine).WithMany().HasForeignKey(up => up.ProductionLineId);
        });

        modelBuilder.Entity<ProductionLine>(e =>
        {
            e.HasIndex(p => p.Name).IsUnique();
            e.Property(p => p.Name).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<ProductionRun>(e =>
        {
            e.HasIndex(r => r.BatchNumber).IsUnique();
            e.HasIndex(r => new { r.ProductionLineId, r.StartTime });
            e.Property(r => r.BatchNumber).IsRequired().HasMaxLength(50);
            e.HasOne(r => r.ProductionLine).WithMany().HasForeignKey(r => r.ProductionLineId);
        });

        modelBuilder.Entity<DefectRecord>(e =>
        {
            e.HasIndex(d => d.ProductionRunId);
            e.HasIndex(d => d.Severity);
            e.HasOne(d => d.ProductionRun).WithMany().HasForeignKey(d => d.ProductionRunId);
        });

        modelBuilder.Entity<SensorReading>(e =>
        {
            e.HasIndex(s => new { s.ProductionLineId, s.Timestamp });
            e.Property(s => s.SensorType).IsRequired().HasMaxLength(50);
        });

        modelBuilder.Entity<Alert>(e =>
        {
            e.HasIndex(a => a.ProductionLineId);
            e.HasIndex(a => a.IsAcknowledged);
            e.HasOne(a => a.ProductionLine).WithMany().HasForeignKey(a => a.ProductionLineId);
        });

        modelBuilder.Entity<ProductionEvent>(e =>
        {
            e.HasIndex(ev => new { ev.ProductionLineId, ev.Timestamp });
        });

        modelBuilder.Entity<Document>(e =>
        {
            e.HasIndex(d => d.Category);
            e.Property(d => d.Title).IsRequired().HasMaxLength(200);
            e.Property(d => d.FileName).IsRequired().HasMaxLength(300);
            e.Property(d => d.Status).IsRequired().HasMaxLength(50);
        });

        modelBuilder.Entity<DocumentChunk>(e =>
        {
            e.HasIndex(c => c.DocumentId);
            e.HasOne(c => c.Document).WithMany(d => d.DocumentChunks).HasForeignKey(c => c.DocumentId);
        });

        modelBuilder.Entity<AgentLog>(e =>
        {
            e.HasIndex(a => new { a.UserId, a.Timestamp });
        });
    }
}