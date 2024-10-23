using HexagonalArchitecture.Infrastructure.Eventing;

namespace HexagonalArchitecture.Infrastructure;

public interface IAggregateRoot
{
    void ClearEvents();

    IReadOnlyList<DomainEvent> ListEvents();
}