using HexagonalArchitecture.Infrastructure;

namespace HexagonalArchitecture.Domain.Command;

public record SignInCustomer(Guid Id, string Username, string Password) : ICommand
{
    public static SignInCustomer Issue(string username, string password)
    {
        return new SignInCustomer(ICommand.GenerateId(), username, password);
    }
}