using HexagonalArchitecture.Infrastructure.Eventing;

namespace HexagonalArchitecture.Domain.Event;

public class UserCreated : DomainEvent
{
    public Guid EventId { get; }
    public UserId UserId { get; }

    public EmailAddress EmailAddress { get; }

    private UserCreated(UserId userId, EmailAddress emailAddress)
    {
        EventId = new Guid();
        UserId = userId;
        EmailAddress = emailAddress;
    }

    public static UserCreated Issue(UserId id, EmailAddress emailAddress)
    {
        return new UserCreated(id, emailAddress);
    }
}