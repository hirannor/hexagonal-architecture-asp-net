namespace HexagonalArchitecture.Infrastructure.Messaging;

public interface IMessagePublisher
{
    public event EventHandler<Message> OnMessageReceived;

    void Publish(IEnumerable<Message> messages);
}