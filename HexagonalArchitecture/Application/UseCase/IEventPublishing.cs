using HexagonalArchitecture.Infrastructure.Messaging;

namespace HexagonalArchitecture.Application.UseCase;

public interface IEventPublishing
{
    void Publish(IEnumerable<Message> messages);
}