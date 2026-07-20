using MicroForge.Infrastructure.Telemetry;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace MicroForge.Web.Hubs;

/// <summary>
/// SignalR hub for real-time production monitoring — pushes sensor readings and alerts to connected clients.
/// </summary>
[Authorize]
public class ProductionHub : Hub
{
    private readonly ILogger<ProductionHub> _logger;
    private readonly AppMetricsService _metrics;

    public ProductionHub(ILogger<ProductionHub> logger, AppMetricsService metrics)
    {
        _logger = logger;
        _metrics = metrics;
    }

    /// <summary>
    /// Called when a client connects. Logs the connection and increments the active connection gauge.
    /// </summary>
    public override Task OnConnectedAsync()
    {
        _metrics.IncrementSignalrConnections();
        _logger.LogInformation("Client connected to ProductionHub: {ConnectionId}", Context.ConnectionId);
        return base.OnConnectedAsync();
    }

    /// <summary>
    /// Called when a client disconnects. Logs the disconnection and decrements the active connection gauge.
    /// </summary>
    public override Task OnDisconnectedAsync(Exception? exception)
    {
        _metrics.DecrementSignalrConnections();
        _logger.LogInformation("Client disconnected from ProductionHub: {ConnectionId}", Context.ConnectionId);
        return base.OnDisconnectedAsync(exception);
    }

    /// <summary>
    /// Subscribe to updates for a specific production line.
    /// </summary>
    public Task SubscribeToLine(int lineId)
    {
        var groupName = $"line-{lineId}";
        Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        _logger.LogInformation("Client {ConnectionId} subscribed to line {LineId}", Context.ConnectionId, lineId);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Unsubscribe from updates for a specific production line.
    /// </summary>
    public Task UnsubscribeFromLine(int lineId)
    {
        var groupName = $"line-{lineId}";
        Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        return Task.CompletedTask;
    }
}