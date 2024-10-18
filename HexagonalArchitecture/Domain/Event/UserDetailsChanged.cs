using HexagonalArchitecture.Infrastructure.Eventing;

namespace HexagonalArchitecture.Domain.Command;

public class UserDetailsChanged : DomainEvent
{
    public Guid EventId { get; }
    public EmailAddress EmailAddress { get; }
    public string FullName { get; }

    public Age Age { get; }

    private UserDetailsChanged(EmailAddress emailAddress, string fullName, Age age)
    {
        EventId = new Guid();
        EmailAddress = emailAddress;
        FullName = fullName;
        Age = age;
    }

    public static UserDetailsChanged Issue(
        EmailAddress emailAddress,
        string fullName,
        Age age)
    {
        return new UserDetailsChanged(emailAddress, fullName, age);
    }
}