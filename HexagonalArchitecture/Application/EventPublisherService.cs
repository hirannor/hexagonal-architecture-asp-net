using HexagonalArchitecture.Application.UseCase;
using HexagonalArchitecture.Infrastructure.Messaging;

namespace HexagonalArchitecture.Application;

public class EventPublisherService(IMessagePublisher messagePublisher, ILogger<EventPublisherService> logger)
    : IEventPublishing
{
    private const string MessagesIsNull = "MessagesIsNull should be not null!";

    public void Publish(IEnumerable<Message> messages)
    {
        if (messages is null)
        {
            logger.LogError(MessagesIsNull);
            ArgumentNullException.ThrowIfNull(MessagesIsNull);
        }
        
        logger.LogInformation("Publishing messages....");
        messagePublisher.Publish(messages);
    }
}