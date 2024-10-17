using HexagonalArchitecture.Infrastructure.Messaging;

namespace HexagonalArchitecture.Infrastructure.Eventing;

public class EventBusInitializer(
    IMessagePublisher messagePublisher,
    IMessageHandler messageHandler,
    ILogger<EventBusInitializer> logger)
{
    public void Initialize()
    {
        // Subscribe to the event
        messagePublisher.OnMessageReceived += messageHandler.Handle;
        logger.LogDebug("EventBus initialized and handlers subscribed.");
    }
}