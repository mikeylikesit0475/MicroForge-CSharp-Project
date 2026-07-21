using MicroForge.Domain.Events;
using MicroForge.Domain.Interfaces;
using MicroForge.Web.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace MicroForge.Web.Services;

/// <summary>
/// Subscribes to in-memory Domain Events and forwards them to SignalR clients.
/// This maintains clean architecture by avoiding direct dependencies from Infrastructure to Web/Hubs.
/// </summary>
public class ProductionEventForwarder : IHostedService
{
    private readonly IEventBus _eventBus;
    private readonly IHubContext<ProductionHub> _hubContext;
    private readonly ILogger<ProductionEventForwarder> _logger;

    public ProductionEventForwarder(
        IEventBus eventBus,
        IHubContext<ProductionHub> hubContext,
        ILogger<ProductionEventForwarder> logger)
    {
        _eventBus = eventBus;
        _hubContext = hubContext;
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Production event forwarder starting...");

        _eventBus.Subscribe<SensorReadingEvent>(async @event =>
        {
            _logger.LogDebug("Forwarding sensor reading event for line {LineId}", @event.ProductionLineId);
            var groupName = $"line-{@event.ProductionLineId}";
            await _hubContext.Clients.Group(groupName).SendAsync("ReceiveSensorReading", @event);
            await _hubContext.Clients.All.SendAsync("ReceiveAllSensorReadings", @event);
        });

        _eventBus.Subscribe<AlertEvent>(async @event =>
        {
            _logger.LogInformation("Forwarding alert event: {Title}", @event.Title);
            await _hubContext.Clients.All.SendAsync("ReceiveAlert", @event);
        });

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Production event forwarder stopped.");
        return Task.CompletedTask;
    }
}
