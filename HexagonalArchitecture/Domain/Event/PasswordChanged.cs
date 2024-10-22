using HexagonalArchitecture.Infrastructure.Eventing;

namespace HexagonalArchitecture.Domain.Event;

public class PasswordChanged : DomainEvent
{
    public static PasswordChanged Issue()
    {
        return new PasswordChanged();
    }
}