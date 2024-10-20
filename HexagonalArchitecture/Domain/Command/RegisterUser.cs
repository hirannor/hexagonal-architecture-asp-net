using HexagonalArchitecture.Infrastructure;

namespace HexagonalArchitecture.Domain.Command;

public record RegisterUser(Guid Id, EmailAddress EmailAddress, string Password, string FullName, Age Age) : ICommand
{
    public static RegisterUser Issue(EmailAddress emailAddress, string password, string fullName, Age age)
    {
        return new RegisterUser(ICommand.GenerateId(), emailAddress, password, fullName, age);
    }
}