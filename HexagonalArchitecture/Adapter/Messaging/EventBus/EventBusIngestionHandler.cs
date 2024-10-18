using HexagonalArchitecture.Application.Port;
using HexagonalArchitecture.Application.UseCase;
using HexagonalArchitecture.Domain.Event;
using HexagonalArchitecture.Infrastructure.Messaging;

namespace HexagonalArchitecture.Adapter.Messaging.EventBus;

public class EventBusIngestionHandler(ILogger<EventBusIngestionHandler> logger, INotificationSending notification)
    : IMessageHandler
{
    public void Handle(object? sender, Message message)
    {
        logger.LogDebug("Received message: {message}", message);

        switch (message)
        {
            case UserCreated userCreated:
                logger.LogDebug("Handling UserCreated event: {evt}", userCreated);
                notification.Send(SendEmailNotification.Create(
                        userCreated.EmailAddress.value,
                        "subject",
                        "content"
                    )
                );
                break;
            default:
                logger.LogWarning("Unhandled event type: {messageType}", message.GetType().Name);
                break;
        }
    }
}