using FluentAssertions;
using MicroForge.Domain.Entities;
using MicroForge.Domain.Interfaces;
using MicroForge.Infrastructure.AI.Tools;
using System.Text.Json.Nodes;

namespace MicroForge.UnitTests;

/// <summary>
/// Unit tests for the GetDashboardSummaryTool — aggregated KPI dashboard.
/// </summary>
public class GetDashboardSummaryToolTests : TestBase
{
    [Fact]
    public async Task invokeasync_returns_summary_for_admin()
    {
        await seedstandarddata();
        var tool = new GetDashboardSummaryTool(DbContext);
        var args = new JsonObject();
        var context = new ToolExecutionContext { UserId = 1, Roles = new List<string> { "Admin" } };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeTrue();
        result.Output.Should().Contain("totalLines");
        result.Output.Should().Contain("activeRuns");
        result.Output.Should().Contain("openAlerts");
    }

    [Fact]
    public async Task invokeasync_counts_active_and_inactive_lines()
    {
        await seedstandarddata();
        var tool = new GetDashboardSummaryTool(DbContext);
        var args = new JsonObject();
        var context = new ToolExecutionContext { UserId = 1, Roles = new List<string> { "Admin" } };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeTrue();
        result.Output.Should().Contain("\"totalLines\":3");
        result.Output.Should().Contain("\"activeLines\":2");
    }

    [Fact]
    public async Task invokeasync_counts_active_runs()
    {
        await seedstandarddata();
        var tool = new GetDashboardSummaryTool(DbContext);
        var args = new JsonObject();
        var context = new ToolExecutionContext { UserId = 1, Roles = new List<string> { "Admin" } };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeTrue();
        result.Output.Should().Contain("\"activeRuns\":1");
    }

    [Fact]
    public async Task invokeasync_counts_open_alerts()
    {
        await seedstandarddata();
        DbContext.Alerts.Add(new Alert
        {
            ProductionLineId = 1, AlertType = "Anomaly", Severity = "Critical",
            Title = "Alert 1", Message = "msg", Source = "test",
            IsAcknowledged = false, CreatedAt = DateTime.UtcNow
        });
        DbContext.Alerts.Add(new Alert
        {
            ProductionLineId = 1, AlertType = "Anomaly", Severity = "Warning",
            Title = "Alert 2", Message = "msg", Source = "test",
            IsAcknowledged = true, CreatedAt = DateTime.UtcNow
        });
        await DbContext.SaveChangesAsync();

        var tool = new GetDashboardSummaryTool(DbContext);
        var args = new JsonObject();
        var context = new ToolExecutionContext { UserId = 1, Roles = new List<string> { "Admin" } };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeTrue();
        result.Output.Should().Contain("\"openAlerts\":1");
    }

    [Fact]
    public async Task invokeasync_operator_gets_filtered_summary()
    {
        await seedstandarddata();
        var tool = new GetDashboardSummaryTool(DbContext);
        var args = new JsonObject();
        var context = new ToolExecutionContext
        {
            UserId = 2,
            Roles = new List<string> { "Operator" },
            ProductionLineIds = new List<int> { 1 }
        };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeTrue();
        result.Output.Should().Contain("\"totalLines\":1");
    }

    [Fact]
    public void tool_name_is_get_dashboard_summary()
    {
        var tool = new GetDashboardSummaryTool(DbContext);
        tool.Name.Should().Be("get_dashboard_summary");
    }

    [Fact]
    public void tool_category_is_dashboard()
    {
        var tool = new GetDashboardSummaryTool(DbContext);
        tool.Category.Should().Be("dashboard");
    }
}