using FluentAssertions;
using MicroForge.Application.Services;
using MicroForge.Domain.Entities;
using MicroForge.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MicroForge.UnitTests;

public class FabExpansionTests : TestBase
{
    [Fact]
    public async Task oee_summary_returns_valid_metrics()
    {
        // Arrange
        var db = DbContext;
        var service = new FabExpansionService(
            new Repository<ProductionLine>(db),
            new Repository<ProductionRun>(db),
            new Repository<SensorReading>(db),
            new Repository<DefectRecord>(db),
            new Repository<CapaAction>(db),
            new Repository<EcoRecord>(db),
            new Repository<ShiftLog>(db),
            new Repository<ToolMaintenanceLog>(db),
            new Repository<RecipeVersion>(db)
        );

        // Act
        var summaries = await service.GetOeeSummariesAsync(CancellationToken.None);

        // Assert
        summaries.Should().NotBeNull();
        if (summaries.Count > 0)
        {
            var s = summaries.First();
            s.Availability.Should().BeGreaterThan(0);
            s.Performance.Should().BeGreaterThan(0);
            s.Quality.Should().BeGreaterThan(0);
            s.Oee.Should().Be(Math.Round(s.Availability * s.Performance * s.Quality / 10000m, 1) * 100m);
        }
    }

    [Fact]
    public async Task golden_run_diff_generates_comparison_points()
    {
        // Arrange
        var db = DbContext;
        var service = new FabExpansionService(
            new Repository<ProductionLine>(db),
            new Repository<ProductionRun>(db),
            new Repository<SensorReading>(db),
            new Repository<DefectRecord>(db),
            new Repository<CapaAction>(db),
            new Repository<EcoRecord>(db),
            new Repository<ShiftLog>(db),
            new Repository<ToolMaintenanceLog>(db),
            new Repository<RecipeVersion>(db)
        );

        // Act
        var diff = await service.GetGoldenRunDiffAsync(1, "Temperature", CancellationToken.None);

        // Assert
        diff.Should().NotBeEmpty();
        diff.Count.Should().Be(30);
        
        var point = diff.First();
        point.TimestampSeconds.Should().Be(0);
        point.Deviation.Should().Be(Math.Abs(point.ActualValue - point.GoldenValue));
    }

    [Fact]
    public async Task capas_workflow_creates_and_approves()
    {
        // Arrange
        var db = DbContext;
        var service = new FabExpansionService(
            new Repository<ProductionLine>(db),
            new Repository<ProductionRun>(db),
            new Repository<SensorReading>(db),
            new Repository<DefectRecord>(db),
            new Repository<CapaAction>(db),
            new Repository<EcoRecord>(db),
            new Repository<ShiftLog>(db),
            new Repository<ToolMaintenanceLog>(db),
            new Repository<RecipeVersion>(db)
        );

        // Act & Assert (Create)
        var capa = await service.CreateCapaAsync(101, "Test CAPA Action", "engineer01", CancellationToken.None);
        capa.Should().NotBeNull();
        capa.LotId.Should().Be(101);
        capa.Status.Should().Be("Draft");
        capa.ActionDescription.Should().Be("Test CAPA Action");

        // Act & Assert (Approve)
        var success = await service.ApproveCapaAsync(capa.Id, "admin_approver", CancellationToken.None);
        success.Should().BeTrue();

        var updated = db.CapaActions.Find(capa.Id);
        updated!.Status.Should().Be("Approved");
        updated.ApprovedBy.Should().Be("admin_approver");
    }
}
