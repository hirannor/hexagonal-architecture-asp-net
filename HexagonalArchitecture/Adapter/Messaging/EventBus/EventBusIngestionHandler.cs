using HexagonalArchitecture.Application.Port;
using HexagonalArchitecture.Application.UseCase;
using HexagonalArchitecture.Domain.Event;
using HexagonalArchitecture.Infrastructure.Adapter;
using HexagonalArchitecture.Infrastructure.Messaging;

namespace HexagonalArchitecture.Adapter.Messaging.EventBus;

[Adapter(type: AdapterType.Driver)]
public class EventBusIngestionHandler(ILogger<EventBusIngestionHandler> logger, INotificationSending notification)
    : IMessageHandler
{
    public void Handle(object? sender, Message message)
    {
        logger.LogDebug("Received message: {message}", message);

        switch (message)
        {
            case CustomerRegistered evt:
                logger.LogDebug("Handling CustomerRegistered event: {evt}", evt);

                notification.Send(SendEmailNotification.Create(
                    evt.EmailAddress.Value,
                    "Customer registration",
                    "Customer with " + evt.Username.Value + " has been registered successfully.")
                );

                break;
            default:
                logger.LogWarning("Unhandled event type: {messageType}", message.GetType().Name);
                break;
        }
    }
}