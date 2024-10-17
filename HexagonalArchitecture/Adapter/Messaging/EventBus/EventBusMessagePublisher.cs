using HexagonalArchitecture.Infrastructure.Messaging;

namespace HexagonalArchitecture.Adapter.Messaging.EventBus;

public class EventBusMessagePublisher : IMessagePublisher
{
    public event EventHandler<Message>? OnMessageReceived;

    public void Publish(IEnumerable<Message> messages)
    {
        foreach (var message in messages)
        {
            OnMessageReceived?.Invoke(this, message);
        }
    }
}