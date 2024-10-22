using HexagonalArchitecture.Infrastructure.Eventing;

namespace HexagonalArchitecture.Domain.Event;

public class PersonalDetailsChanged : DomainEvent
{
    public static PersonalDetailsChanged Issue()
    {
        return new PersonalDetailsChanged();
    }
}