using FluentAssertions;
using MicroForge.Application.Services;
using MicroForge.Domain.Entities;
using MicroForge.Domain.Analytics;
using MicroForge.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MicroForge.UnitTests;

public class FdcServiceTests : TestBase
{
    [Fact]
    public void fdc_analyzer_detects_thermal_runaway()
    {
        // Arrange
        var sensorType = "Temperature";
        var values = new List<decimal> { 100m, 110m, 150m, 210m, 300m }; // Continuously rising by > 40%

        // Act
        var result = FdcAnalyzer.Analyze(sensorType, values);

        // Assert
        result.FaultDetected.Should().BeTrue();
        result.FaultName.Should().Be("Thermal Runaway");
        result.Severity.Should().Be("Critical");
    }

    [Fact]
    public void fdc_analyzer_detects_vacuum_leak()
    {
        // Arrange
        var sensorType = "Pressure";
        var values = new List<decimal> { 0.05m, 0.05m, 0.06m, 0.05m, 0.20m }; // Huge spike on last reading

        // Act
        var result = FdcAnalyzer.Analyze(sensorType, values);

        // Assert
        result.FaultDetected.Should().BeTrue();
        result.FaultName.Should().Be("Chamber Vacuum Leak");
        result.Severity.Should().Be("Critical");
    }

    [Fact]
    public void fdc_analyzer_detects_plasma_instability()
    {
        // Arrange
        var sensorType = "Vibration";
        var values = new List<decimal> { 1.0m, 5.0m, 0.5m, 8.0m, 1.2m }; // High volatility (standard deviation > 60% of mean)

        // Act
        var result = FdcAnalyzer.Analyze(sensorType, values);

        // Assert
        result.FaultDetected.Should().BeTrue();
        result.FaultName.Should().Be("RF Plasma Instability");
        result.Severity.Should().Be("High");
    }

    [Fact]
    public async Task fdc_service_calculates_chamber_mismatch_correctly()
    {
        var db = DbContext;
        var lineRepo = new Repository<ProductionLine>(db);
        var readingRepo = new Repository<SensorReading>(db);
        var runRepo = new Repository<ProductionRun>(db);

        var line1 = new ProductionLine { Name = "Chamber 1", IsActive = true };
        var line2 = new ProductionLine { Name = "Chamber 2", IsActive = true };
        db.ProductionLines.AddRange(line1, line2);
        await db.SaveChangesAsync();

        // Feed standard temp readings for Chamber 1 (around 20)
        db.SensorReadings.Add(new SensorReading { ProductionLineId = line1.Id, SensorType = "Temperature", Value = 20m, Timestamp = DateTime.UtcNow });
        db.SensorReadings.Add(new SensorReading { ProductionLineId = line1.Id, SensorType = "Temperature", Value = 21m, Timestamp = DateTime.UtcNow });
        // Chamber 2 has mismatched temp (around 50)
        db.SensorReadings.Add(new SensorReading { ProductionLineId = line2.Id, SensorType = "Temperature", Value = 50m, Timestamp = DateTime.UtcNow });
        db.SensorReadings.Add(new SensorReading { ProductionLineId = line2.Id, SensorType = "Temperature", Value = 52m, Timestamp = DateTime.UtcNow });
        await db.SaveChangesAsync();

        var service = new FdcService(lineRepo, readingRepo, runRepo);

        // Act
        var summaries = await service.GetChamberMatchingAsync(CancellationToken.None);

        // Assert
        summaries.Should().NotBeEmpty();
        var tempSummary = summaries.First(s => s.SensorType == "Temperature");
        tempSummary.Chambers.Should().HaveCount(2);

        var c1 = tempSummary.Chambers.First(c => c.ProductionLineId == line1.Id);
        var c2 = tempSummary.Chambers.First(c => c.ProductionLineId == line2.Id);

        // Chamber 2 averages 51, group average is (20.5 + 51)/2 = 35.75.
        // Chamber 2 deviates by (51 - 35.75)/35.75 = 42% (> 15%). It must be flagged mismatched!
        c2.IsMismatched.Should().BeTrue();
        c2.Status.Should().Be("Mismatched");

        // Chamber 1 averages 20.5, deviates by (35.75 - 20.5)/35.75 = 42.6% (> 15%). It should also be flagged mismatched relative to the group!
        c1.IsMismatched.Should().BeTrue();
    }
}
