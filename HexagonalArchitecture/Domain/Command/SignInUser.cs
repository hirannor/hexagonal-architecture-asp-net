using HexagonalArchitecture.Infrastructure;

namespace HexagonalArchitecture.Domain.Command;

public record SignInUser(Guid Id, EmailAddress EmailAddress, string Password) : ICommand
{
    public static SignInUser Issue(EmailAddress emailAddress, string password)
    {
        return new SignInUser(ICommand.GenerateId(), emailAddress, password);
    }
}