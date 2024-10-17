using HexagonalArchitecture.Domain.Event;
using HexagonalArchitecture.Infrastructure;

namespace HexagonalArchitecture.Adapter.Messaging.EventBus;

public class EventBusIngestionHandler(ILogger<EventBusIngestionHandler> logger)
{
    public void Handle(object? sender, Message message)
    {
        logger.LogDebug("Received message: {message}", message);

        switch (message)
        {
            case UserCreated userCreated:
                logger.LogDebug("Handling UserCreated event: {evt}", userCreated);
                break;
            default:
                logger.LogWarning("Unhandled event type: {messageType}", message.GetType().Name);
                break;
        }
    }
}