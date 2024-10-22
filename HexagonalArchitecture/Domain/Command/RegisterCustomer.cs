using HexagonalArchitecture.Infrastructure;

namespace HexagonalArchitecture.Domain.Command;

public record RegisterCustomer(
    Guid Id,
    string Username,
    string EmailAddress,
    string Password,
    string FirstName,
    string LastName,
    DateOnly BirthOn)
    : ICommand
{
    public static RegisterCustomer Issue(
        string username,
        string emailAddress,
        string password,
        string firstName,
        string lastName,
        DateOnly birthOn)
    {
        return new RegisterCustomer(
            ICommand.GenerateId(),
            username,
            emailAddress,
            password,
            firstName,
            lastName,
            birthOn
        );
    }
}