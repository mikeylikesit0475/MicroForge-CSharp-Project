using FluentAssertions;
using MicroForge.Domain.Entities;
using MicroForge.Domain.Interfaces;
using MicroForge.Infrastructure.AI.Tools;
using System.Text.Json.Nodes;

namespace MicroForge.UnitTests;

/// <summary>
/// Unit tests for the QueryAlertsTool — alert querying with data boundary enforcement.
/// </summary>
public class QueryAlertsToolTests : TestBase
{
    [Fact]
    public async Task invokeasync_with_no_alerts_returns_zero_count()
    {
        await seedstandarddata();
        var tool = new QueryAlertsTool(DbContext);
        var args = new JsonObject { ["limit"] = 20 };
        var context = new ToolExecutionContext { UserId = 1, Roles = new List<string> { "Admin" } };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeTrue();
        result.Output.Should().Contain("\"count\":0");
    }

    [Fact]
    public async Task invokeasync_with_alerts_returns_them()
    {
        await seedstandarddata();
        DbContext.Alerts.Add(new Alert
        {
            ProductionLineId = 1, AlertType = "TemperatureAnomaly",
            Severity = "Critical", Title = "Overheating", Message = "Temperature exceeded threshold",
            Source = "SensorSimulator", IsAcknowledged = false, CreatedAt = DateTime.UtcNow
        });
        await DbContext.SaveChangesAsync();

        var tool = new QueryAlertsTool(DbContext);
        var args = new JsonObject { ["limit"] = 20 };
        var context = new ToolExecutionContext { UserId = 1, Roles = new List<string> { "Admin" } };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeTrue();
        result.Output.Should().Contain("\"count\":1");
        result.Output.Should().Contain("Overheating");
    }

    [Fact]
    public async Task invokeasync_filters_unacknowledged_only()
    {
        await seedstandarddata();
        DbContext.Alerts.Add(new Alert
        {
            ProductionLineId = 1, AlertType = "Anomaly", Severity = "Warning",
            Title = "Unack", Message = "Not acknowledged", Source = "test",
            IsAcknowledged = false, CreatedAt = DateTime.UtcNow
        });
        DbContext.Alerts.Add(new Alert
        {
            ProductionLineId = 1, AlertType = "Anomaly", Severity = "Info",
            Title = "Acked", Message = "Acknowledged", Source = "test",
            IsAcknowledged = true, CreatedAt = DateTime.UtcNow
        });
        await DbContext.SaveChangesAsync();

        var tool = new QueryAlertsTool(DbContext);
        var args = new JsonObject { ["unacknowledgedOnly"] = true, ["limit"] = 20 };
        var context = new ToolExecutionContext { UserId = 1, Roles = new List<string> { "Admin" } };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeTrue();
        result.Output.Should().Contain("\"count\":1");
        result.Output.Should().Contain("Unack");
        result.Output.Should().NotContain("Acked");
    }

    [Fact]
    public async Task invokeasync_filters_by_severity()
    {
        await seedstandarddata();
        DbContext.Alerts.Add(new Alert
        {
            ProductionLineId = 1, AlertType = "Anomaly", Severity = "Critical",
            Title = "Critical Alert", Message = "Critical issue", Source = "test",
            IsAcknowledged = false, CreatedAt = DateTime.UtcNow
        });
        DbContext.Alerts.Add(new Alert
        {
            ProductionLineId = 1, AlertType = "Anomaly", Severity = "Warning",
            Title = "Warning Alert", Message = "Warning issue", Source = "test",
            IsAcknowledged = false, CreatedAt = DateTime.UtcNow
        });
        await DbContext.SaveChangesAsync();

        var tool = new QueryAlertsTool(DbContext);
        var args = new JsonObject { ["severity"] = "Critical", ["limit"] = 20 };
        var context = new ToolExecutionContext { UserId = 1, Roles = new List<string> { "Admin" } };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeTrue();
        result.Output.Should().Contain("\"count\":1");
        result.Output.Should().Contain("Critical Alert");
        result.Output.Should().NotContain("Warning Alert");
    }

    [Fact]
    public async Task invokeasync_enforces_data_boundary_for_operator()
    {
        await seedstandarddata();
        DbContext.Alerts.Add(new Alert
        {
            ProductionLineId = 1, AlertType = "Anomaly", Severity = "Warning",
            Title = "Line 1 Alert", Message = "On line 1", Source = "test",
            IsAcknowledged = false, CreatedAt = DateTime.UtcNow
        });
        DbContext.Alerts.Add(new Alert
        {
            ProductionLineId = 2, AlertType = "Anomaly", Severity = "Warning",
            Title = "Line 2 Alert", Message = "On line 2", Source = "test",
            IsAcknowledged = false, CreatedAt = DateTime.UtcNow
        });
        await DbContext.SaveChangesAsync();

        var tool = new QueryAlertsTool(DbContext);
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
        result.Output.Should().Contain("Line 1 Alert");
        result.Output.Should().NotContain("Line 2 Alert");
    }

    [Fact]
    public async Task invokeasync_operator_denied_unauthorized_line()
    {
        await seedstandarddata();
        var tool = new QueryAlertsTool(DbContext);
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
    public void tool_name_is_query_alerts()
    {
        var tool = new QueryAlertsTool(DbContext);
        tool.Name.Should().Be("query_alerts");
    }

    [Fact]
    public void tool_category_is_alerts()
    {
        var tool = new QueryAlertsTool(DbContext);
        tool.Category.Should().Be("alerts");
    }
}