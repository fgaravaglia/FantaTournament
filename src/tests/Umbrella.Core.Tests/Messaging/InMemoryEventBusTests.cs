using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Umbrella.Core.Messaging;

namespace Umbrella.Core.Tests.Messaging;

public class TestEvent : IEvent
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
    public string Message { get; set; } = string.Empty;
}

[TestFixture]
public class InMemoryEventBusTests
{
    private ServiceProvider? _serviceProvider;
    private ILogger<InMemoryEventBus>? _logger;
    private InMemoryEventBus? _eventBus;

    [SetUp]
    public void SetUp()
    {
        _logger = Substitute.For<ILogger<InMemoryEventBus>>();
    }

    [TearDown]
    public void TearDown()
    {
        _serviceProvider?.Dispose();
    }

    [Test]
    public async Task PublishAsync_ShouldExecuteAllHandlersInParallel()
    {
        // Arrange
        var services = new ServiceCollection();
        
        var handler1 = Substitute.For<IEventHandler<TestEvent>>();
        var handler2 = Substitute.For<IEventHandler<TestEvent>>();

        // Simulate some delay in handler 1 to test parallelism
        handler1.HandleAsync(Arg.Any<TestEvent>()).Returns(async _ => await Task.Delay(100));

        services.AddSingleton(handler1);
        services.AddSingleton(handler2);
        
        _serviceProvider = services.BuildServiceProvider();
        _eventBus = new InMemoryEventBus(_serviceProvider, _logger!);

        var @event = new TestEvent { Message = "Hello" };

        // Act
        await _eventBus.PublishAsync(@event);

        // Assert
        await handler1.Received(1).HandleAsync(@event);
        await handler2.Received(1).HandleAsync(@event);
    }

    [Test]
    public async Task PublishAsync_WhenNoHandlersRegistered_ShouldLogWarningAndReturn()
    {
        // Arrange
        var services = new ServiceCollection();
        _serviceProvider = services.BuildServiceProvider();
        _eventBus = new InMemoryEventBus(_serviceProvider, _logger!);

        var @event = new TestEvent();

        // Act
        await _eventBus.PublishAsync(@event);

        // Assert
        _logger.Received(1).Log(
            LogLevel.Warning,
            Arg.Any<EventId>(),
            Arg.Is<object>(o => o.ToString()!.Contains("No handlers registered")),
            null,
            Arg.Any<Func<object, Exception?, string>>());
    }

    [Test]
    public async Task PublishAsync_WhenHandlerThrows_ShouldNotStopOtherHandlers()
    {
        // Arrange
        var services = new ServiceCollection();
        
        var handler1 = Substitute.For<IEventHandler<TestEvent>>();
        var handler2 = Substitute.For<IEventHandler<TestEvent>>();

        handler1.HandleAsync(Arg.Any<TestEvent>()).Returns(_ => throw new Exception("Handler 1 failed"));

        services.AddSingleton(handler1);
        services.AddSingleton(handler2);
        
        _serviceProvider = services.BuildServiceProvider();
        _eventBus = new InMemoryEventBus(_serviceProvider, _logger!);

        var @event = new TestEvent();

        // Act
        await _eventBus.PublishAsync(@event);

        // Assert
        await handler2.Received(1).HandleAsync(@event);
        _logger.Received(1).Log(
            LogLevel.Error,
            Arg.Any<EventId>(),
            Arg.Is<object>(o => o.ToString()!.Contains("Error handling event")),
            Arg.Any<Exception>(),
            Arg.Any<Func<object, Exception?, string>>());
    }
}
