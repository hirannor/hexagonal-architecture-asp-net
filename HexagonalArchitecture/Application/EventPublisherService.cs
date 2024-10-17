using HexagonalArchitecture.Application.UseCase;
using HexagonalArchitecture.Infrastructure;

namespace HexagonalArchitecture.Application;

public class EventPublisherService(IMessagePublisher messagePublisher, ILogger<EventPublisherService> logger)
    : IEventPublishing
{
    public void Publish(IEnumerable<Message> messages)
    {
        logger.LogInformation("Publishing messages....");
        messagePublisher.Publish(messages);
    }
}