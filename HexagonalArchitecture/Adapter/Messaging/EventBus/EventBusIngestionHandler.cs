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
                        "UserCreated",
                        "User has been created successfully."
                    )
                );
                break;
            case UserDetailsChanged userDetailsChanged:
                logger.LogDebug("Handling UserDetailsChanged event: {evt}", userDetailsChanged);
                notification.Send(SendEmailNotification.Create(
                        userDetailsChanged.EmailAddress.value,
                        "UserDetailsChanged",
                        "User details has been modified successfully."
                    )
                );
                break;
            default:
                logger.LogWarning("Unhandled event type: {messageType}", message.GetType().Name);
                break;
        }
    }
}