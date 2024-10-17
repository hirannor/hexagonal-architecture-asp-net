namespace HexagonalArchitecture.Infrastructure.Messaging;

public interface IMessageHandler
{
    void Handle(object? sender, Message message);
}