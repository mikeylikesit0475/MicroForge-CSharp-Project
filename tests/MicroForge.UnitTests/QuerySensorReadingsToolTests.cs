using FluentAssertions;
using MicroForge.Domain.Entities;
using MicroForge.Domain.Interfaces;
using MicroForge.Infrastructure.AI.Tools;
using System.Text.Json.Nodes;

namespace MicroForge.UnitTests;

/// <summary>
/// Unit tests for the QuerySensorReadingsTool — sensor data querying with data boundary enforcement.
/// </summary>
public class QuerySensorReadingsToolTests : TestBase
{
    [Fact]
    public async Task invokeasync_with_no_readings_returns_zero_count()
    {
        await seedstandarddata();
        var tool = new QuerySensorReadingsTool(DbContext);
        var args = new JsonObject { ["limit"] = 20 };
        var context = new ToolExecutionContext { UserId = 1, Roles = new List<string> { "Admin" } };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeTrue();
        result.Output.Should().Contain("\"count\":0");
    }

    [Fact]
    public async Task invokeasync_with_readings_returns_them()
    {
        await seedstandarddata();
        DbContext.SensorReadings.Add(new SensorReading
        {
            ProductionLineId = 1, SensorType = "Temperature",
            Value = 25.5m, Unit = "°C", Timestamp = DateTime.UtcNow
        });
        await DbContext.SaveChangesAsync();

        var tool = new QuerySensorReadingsTool(DbContext);
        var args = new JsonObject { ["limit"] = 20 };
        var context = new ToolExecutionContext { UserId = 1, Roles = new List<string> { "Admin" } };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeTrue();
        result.Output.Should().Contain("\"count\":1");
        result.Output.Should().Contain("Temperature");
    }

    [Fact]
    public async Task invokeasync_filters_by_sensortype()
    {
        await seedstandarddata();
        DbContext.SensorReadings.Add(new SensorReading
        {
            ProductionLineId = 1, SensorType = "Temperature",
            Value = 25m, Unit = "°C", Timestamp = DateTime.UtcNow
        });
        DbContext.SensorReadings.Add(new SensorReading
        {
            ProductionLineId = 1, SensorType = "Pressure",
            Value = 2m, Unit = "bar", Timestamp = DateTime.UtcNow
        });
        await DbContext.SaveChangesAsync();

        var tool = new QuerySensorReadingsTool(DbContext);
        var args = new JsonObject { ["sensorType"] = "Temperature", ["limit"] = 20 };
        var context = new ToolExecutionContext { UserId = 1, Roles = new List<string> { "Admin" } };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeTrue();
        result.Output.Should().Contain("\"count\":1");
        result.Output.Should().Contain("Temperature");
        result.Output.Should().NotContain("Pressure");
    }

    [Fact]
    public async Task invokeasync_filters_anomalies_only()
    {
        await seedstandarddata();
        DbContext.SensorReadings.Add(new SensorReading
        {
            ProductionLineId = 1, SensorType = "Temperature",
            Value = 25m, Unit = "°C", Timestamp = DateTime.UtcNow, IsAnomaly = false
        });
        DbContext.SensorReadings.Add(new SensorReading
        {
            ProductionLineId = 1, SensorType = "Temperature",
            Value = 90m, Unit = "°C", Timestamp = DateTime.UtcNow, IsAnomaly = true, AnomalyType = "HighThreshold"
        });
        await DbContext.SaveChangesAsync();

        var tool = new QuerySensorReadingsTool(DbContext);
        var args = new JsonObject { ["anomaliesOnly"] = true, ["limit"] = 20 };
        var context = new ToolExecutionContext { UserId = 1, Roles = new List<string> { "Admin" } };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeTrue();
        result.Output.Should().Contain("\"count\":1");
        result.Output.Should().Contain("90");
    }

    [Fact]
    public async Task invokeasync_enforces_data_boundary_for_operator()
    {
        await seedstandarddata();
        DbContext.SensorReadings.Add(new SensorReading
        {
            ProductionLineId = 1, SensorType = "Temperature",
            Value = 25m, Unit = "°C", Timestamp = DateTime.UtcNow
        });
        DbContext.SensorReadings.Add(new SensorReading
        {
            ProductionLineId = 2, SensorType = "Temperature",
            Value = 30m, Unit = "°C", Timestamp = DateTime.UtcNow
        });
        await DbContext.SaveChangesAsync();

        var tool = new QuerySensorReadingsTool(DbContext);
        var args = new JsonObject { ["limit"] = 20 };
        var context = new ToolExecutionContext
        {
            UserId = 2,
            Roles = new List<string> { "Operator" },
            ProductionLineIds = new List<int> { 1 }
        };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeTrue();
        result.Output.Should().Contain("\"count\":1");
        result.Output.Should().Contain("\"productionLineId\":1");
    }

    [Fact]
    public async Task invokeasync_operator_denied_unauthorized_line()
    {
        await seedstandarddata();
        var tool = new QuerySensorReadingsTool(DbContext);
        var args = new JsonObject { ["productionLineId"] = 2, ["limit"] = 20 };
        var context = new ToolExecutionContext
        {
            UserId = 2,
            Roles = new List<string> { "Operator" },
            ProductionLineIds = new List<int> { 1 }
        };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeFalse();
        result.Error.Should().Contain("Access denied");
    }

    [Fact]
    public void tool_name_is_query_sensor_readings()
    {
        var tool = new QuerySensorReadingsTool(DbContext);
        tool.Name.Should().Be("query_sensor_readings");
    }

    [Fact]
    public void tool_category_is_sensors()
    {
        var tool = new QuerySensorReadingsTool(DbContext);
        tool.Category.Should().Be("sensors");
    }
}