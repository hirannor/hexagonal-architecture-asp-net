using HexagonalArchitecture.Infrastructure.Eventing;

namespace HexagonalArchitecture.Domain.Event;

public class CustomerRegistered : DomainEvent
{
    public Guid EventId { get; }

    public Username Username { get; }
    public EmailAddress EmailAddress { get; }

    private CustomerRegistered(Username username, EmailAddress emailAddress)
    {
        EventId = new Guid();
        Username = username;
        EmailAddress = emailAddress;
    }

    public static CustomerRegistered Issue(Username username, EmailAddress emailAddress)
    {
        return new CustomerRegistered(username, emailAddress);
    }
}