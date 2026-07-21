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
    public DbSet<Lot> Lots => Set<Lot>();
    public DbSet<Wafer> Wafers => Set<Wafer>();
    public DbSet<RouteStep> RouteSteps => Set<RouteStep>();
    public DbSet<LotMovementLog> LotMovementLogs => Set<LotMovementLog>();
    public DbSet<ToolMaintenanceLog> ToolMaintenanceLogs => Set<ToolMaintenanceLog>();
    public DbSet<RecipeVersion> RecipeVersions => Set<RecipeVersion>();
    public DbSet<CapaAction> CapaActions => Set<CapaAction>();
    public DbSet<EcoRecord> EcoRecords => Set<EcoRecord>();
    public DbSet<ShiftLog> ShiftLogs => Set<ShiftLog>();
    public DbSet<ToolQualification> ToolQualifications => Set<ToolQualification>();
    public DbSet<WorkInstruction> WorkInstructions => Set<WorkInstruction>();
    public DbSet<LotChecklistProgress> LotChecklistProgresses => Set<LotChecklistProgress>();

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

        modelBuilder.Entity<Lot>(e =>
        {
            e.HasIndex(l => l.LotNumber).IsUnique();
            e.Property(l => l.LotNumber).IsRequired().HasMaxLength(50);
            e.Property(l => l.RecipeName).IsRequired().HasMaxLength(100);
            e.HasOne(l => l.ActiveRouteStep).WithMany().HasForeignKey(l => l.ActiveRouteStepId);
        });

        modelBuilder.Entity<Wafer>(e =>
        {
            e.HasIndex(w => w.WaferId).IsUnique();
            e.Property(w => w.WaferId).IsRequired().HasMaxLength(50);
            e.HasOne(w => w.Lot).WithMany(l => l.Wafers).HasForeignKey(w => w.LotId);
        });

        modelBuilder.Entity<RouteStep>(e =>
        {
            e.HasIndex(r => r.StepName).IsUnique();
            e.Property(r => r.StepName).IsRequired().HasMaxLength(50);
        });

        modelBuilder.Entity<LotMovementLog>(e =>
        {
            e.HasIndex(m => m.LotId);
            e.HasOne(m => m.Lot).WithMany(l => l.MovementLogs).HasForeignKey(m => m.LotId);
            e.HasOne(m => m.RouteStep).WithMany().HasForeignKey(m => m.RouteStepId);
        });

        modelBuilder.Entity<ToolMaintenanceLog>(e =>
        {
            e.HasIndex(m => m.ProductionLineId);
            e.HasOne(m => m.ProductionLine).WithMany().HasForeignKey(m => m.ProductionLineId);
        });

        modelBuilder.Entity<RecipeVersion>(e =>
        {
            e.HasIndex(r => new { r.RecipeName, r.VersionNumber }).IsUnique();
        });

        modelBuilder.Entity<CapaAction>(e =>
        {
            e.HasIndex(c => c.LotId);
        });

        modelBuilder.Entity<EcoRecord>(e =>
        {
            e.HasIndex(ec => ec.RecipeName);
        });

        modelBuilder.Entity<ShiftLog>(e =>
        {
            e.Property(s => s.ShiftName).IsRequired().HasMaxLength(50);
        });

        modelBuilder.Entity<ToolQualification>(e =>
        {
            e.HasIndex(q => new { q.ProductionLineId, q.RecipeName }).IsUnique();
            e.HasOne(q => q.ProductionLine).WithMany().HasForeignKey(q => q.ProductionLineId);
        });

        modelBuilder.Entity<WorkInstruction>(e =>
        {
            e.HasIndex(w => w.RouteStepId);
            e.HasOne(w => w.RouteStep).WithMany().HasForeignKey(w => w.RouteStepId);
        });

        modelBuilder.Entity<LotChecklistProgress>(e =>
        {
            e.HasIndex(p => new { p.LotId, p.WorkInstructionId }).IsUnique();
            e.HasOne(p => p.Lot).WithMany().HasForeignKey(p => p.LotId);
            e.HasOne(p => p.WorkInstruction).WithMany().HasForeignKey(p => p.WorkInstructionId);
        });
    }
}