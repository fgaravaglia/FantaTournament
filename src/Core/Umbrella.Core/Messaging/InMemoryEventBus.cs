using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Umbrella.Core.Messaging;

/// <summary>
/// A multithreaded in-memory implementation of <see cref="IEventBus"/>.
/// </summary>
public class InMemoryEventBus : IEventBus
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<InMemoryEventBus> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="InMemoryEventBus"/> class.
    /// </summary>
    /// <param name="serviceProvider">The service provider used to resolve event handlers.</param>
    /// <param name="logger">The logger instance.</param>
    public InMemoryEventBus(IServiceProvider serviceProvider, ILogger<InMemoryEventBus> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent
    {
        _logger.LogInformation("Publishing event {EventType} occurred on {OccurredOn}", typeof(TEvent).Name, @event.OccurredOn);

        // Resolve all handlers for the event type
        var handlers = _serviceProvider.GetServices<IEventHandler<TEvent>>();

        if (handlers == null || !handlers.Any())
        {
            _logger.LogWarning("No handlers registered for event {EventType}", typeof(TEvent).Name);
            return;
        }

        // Execute all handlers in parallel
        var tasks = handlers.Select(async handler =>
        {
            try
            {
                await handler.HandleAsync(@event);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error handling event {EventType} in handler {HandlerType}", 
                    typeof(TEvent).Name, handler.GetType().Name);
                // We don't rethrow here to allow other handlers to proceed
            }
        });

        await Task.WhenAll(tasks);
    }
}
