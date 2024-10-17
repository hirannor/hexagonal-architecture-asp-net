using HexagonalArchitecture.Infrastructure;

namespace HexagonalArchitecture.Application.UseCase;

public interface IEventPublishing
{
    void Publish(IEnumerable<Message> messages);
}