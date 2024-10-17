namespace HexagonalArchitecture.Infrastructure;

public interface IAggregateRoot
{
    void ClearEvents();

    List<DomainEvent> ListEvents();
}