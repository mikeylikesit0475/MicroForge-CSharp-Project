using MicroForge.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace MicroForge.Infrastructure.Events;

public class EventBus : IEventBus
{
    private readonly Dictionary<Type, List<Func<object, Task>>> _handlers = new();
    private readonly ILogger<EventBus> _logger;

    public EventBus(ILogger<EventBus> logger)
    {
        _logger = logger;
    }

    public void Subscribe<T>(Func<T, Task> handler) where T : class
    {
        var type = typeof(T);
        if (!_handlers.ContainsKey(type))
            _handlers[type] = new List<Func<object, Task>>();
        _handlers[type].Add(obj => handler((T)obj));
    }

    public async Task PublishAsync<T>(T @event, CancellationToken ct = default) where T : class
    {
        var type = typeof(T);
        if (!_handlers.TryGetValue(type, out var handlers))
            return;

        foreach (var handler in handlers)
        {
            try
            {
                await handler(@event);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error handling event {EventType}", type.Name);
            }
        }
    }
}