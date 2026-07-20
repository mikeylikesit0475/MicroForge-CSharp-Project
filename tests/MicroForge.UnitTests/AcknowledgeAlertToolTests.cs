using FluentAssertions;
using MicroForge.Domain.Entities;
using MicroForge.Domain.Interfaces;
using MicroForge.Infrastructure.AI.Tools;
using System.Text.Json.Nodes;

namespace MicroForge.UnitTests;

/// <summary>
/// Unit tests for the AcknowledgeAlertTool — alert acknowledgement with authorization.
/// </summary>
public class AcknowledgeAlertToolTests : TestBase
{
    [Fact]
    public async Task invokeasync_with_valid_alertid_acknowledges_alert()
    {
        await seedstandarddata();
        var alert = new Alert
        {
            ProductionLineId = 1, AlertType = "Anomaly", Severity = "Critical",
            Title = "Test Alert", Message = "Test message", Source = "test",
            IsAcknowledged = false, CreatedAt = DateTime.UtcNow
        };
        DbContext.Alerts.Add(alert);
        await DbContext.SaveChangesAsync();

        var tool = new AcknowledgeAlertTool(DbContext);
        var args = new JsonObject { ["alertId"] = alert.Id };
        var context = new ToolExecutionContext { UserId = 1, Roles = new List<string> { "Admin" } };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeTrue();
        result.Output.Should().Contain("acknowledged");
        var updated = DbContext.Alerts.Find(alert.Id);
        updated!.IsAcknowledged.Should().BeTrue();
        updated.AcknowledgedByUserId.Should().Be(1);
    }

    [Fact]
    public async Task invokeasync_with_nonexistent_alertid_returns_failure()
    {
        await seedstandarddata();
        var tool = new AcknowledgeAlertTool(DbContext);
        var args = new JsonObject { ["alertId"] = 9999 };
        var context = new ToolExecutionContext { UserId = 1, Roles = new List<string> { "Admin" } };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeFalse();
        result.Error.Should().Contain("not found");
    }

    [Fact]
    public async Task invokeasync_operator_denied_for_unauthorized_line()
    {
        await seedstandarddata();
        var alert = new Alert
        {
            ProductionLineId = 2, AlertType = "Anomaly", Severity = "Critical",
            Title = "Line 2 Alert", Message = "msg", Source = "test",
            IsAcknowledged = false, CreatedAt = DateTime.UtcNow
        };
        DbContext.Alerts.Add(alert);
        await DbContext.SaveChangesAsync();

        var tool = new AcknowledgeAlertTool(DbContext);
        var args = new JsonObject { ["alertId"] = alert.Id };
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
    public async Task invokeasync_operator_allowed_for_authorized_line()
    {
        await seedstandarddata();
        var alert = new Alert
        {
            ProductionLineId = 1, AlertType = "Anomaly", Severity = "Critical",
            Title = "Line 1 Alert", Message = "msg", Source = "test",
            IsAcknowledged = false, CreatedAt = DateTime.UtcNow
        };
        DbContext.Alerts.Add(alert);
        await DbContext.SaveChangesAsync();

        var tool = new AcknowledgeAlertTool(DbContext);
        var args = new JsonObject { ["alertId"] = alert.Id };
        var context = new ToolExecutionContext
        {
            UserId = 2,
            Roles = new List<string> { "Operator" },
            ProductionLineIds = new List<int> { 1 }
        };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeTrue();
    }

    [Fact]
    public async Task invokeasync_already_acknowledged_alert_returns_success()
    {
        await seedstandarddata();
        var alert = new Alert
        {
            ProductionLineId = 1, AlertType = "Anomaly", Severity = "Info",
            Title = "Already Acked", Message = "msg", Source = "test",
            IsAcknowledged = true, AcknowledgedByUserId = 1,
            AcknowledgedAt = DateTime.UtcNow, CreatedAt = DateTime.UtcNow
        };
        DbContext.Alerts.Add(alert);
        await DbContext.SaveChangesAsync();

        var tool = new AcknowledgeAlertTool(DbContext);
        var args = new JsonObject { ["alertId"] = alert.Id };
        var context = new ToolExecutionContext { UserId = 1, Roles = new List<string> { "Admin" } };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeTrue();
    }

    [Fact]
    public void tool_name_is_acknowledge_alert()
    {
        var tool = new AcknowledgeAlertTool(DbContext);
        tool.Name.Should().Be("acknowledge_alert");
    }

    [Fact]
    public void tool_category_is_alerts()
    {
        var tool = new AcknowledgeAlertTool(DbContext);
        tool.Category.Should().Be("alerts");
    }
}