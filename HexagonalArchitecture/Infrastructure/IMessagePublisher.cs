namespace HexagonalArchitecture.Infrastructure;

public interface IMessagePublisher
{
    public event EventHandler<Message> OnMessageReceived;

    void Publish(IEnumerable<Message> messages);
}