using HexagonalArchitecture.Infrastructure.Eventing;

namespace HexagonalArchitecture.Domain.Event;

public class EmailAddressChanged : DomainEvent
{
    public static EmailAddressChanged Issue()
    {
        return new EmailAddressChanged();
    }
}