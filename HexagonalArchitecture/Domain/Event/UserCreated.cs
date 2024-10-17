using HexagonalArchitecture.Infrastructure;

namespace HexagonalArchitecture.Domain.Event;

public class UserCreated : DomainEvent
{
    public Guid EventId { get; }
    public UserId UserId { get; }

    private UserCreated(UserId userId)
    {
        EventId = new Guid();
        UserId = userId;
    }

    public static UserCreated Issue(UserId id)
    {
        return new UserCreated(id);
    }
}