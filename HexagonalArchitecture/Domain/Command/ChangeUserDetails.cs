using HexagonalArchitecture.Infrastructure;

namespace HexagonalArchitecture.Domain.Command;

public record ChangeUserDetails(Guid Id, UserId UserId, EmailAddress EmailAddress, string FullName, Age Age) : ICommand
{
    public static ChangeUserDetails From(
        UserId userId,
        EmailAddress emailAddress,
        string fullName,
        Age age)
    {
        return new ChangeUserDetails(ICommand.GenerateId(), userId, emailAddress, fullName, age);
    }
}