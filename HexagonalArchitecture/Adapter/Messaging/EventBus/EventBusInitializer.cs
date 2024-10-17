using HexagonalArchitecture.Infrastructure;

namespace HexagonalArchitecture.Adapter.Messaging.EventBus;

public class EventBusInitializer(
    IMessagePublisher messagePublisher,
    EventBusIngestionHandler eventBusIngestionHandler,
    ILogger<EventBusInitializer> logger)
{
    public void Initialize()
    {
        // Subscribe to the event
        messagePublisher.OnMessageReceived += eventBusIngestionHandler.Handle;
        logger.LogDebug("EventBus initialized and handlers subscribed.");
    }
}