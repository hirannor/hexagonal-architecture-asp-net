using HexagonalArchitecture.Infrastructure;

namespace HexagonalArchitecture.Domain.Command;

public record ChangePersonalDetails(Guid Id, string Username, string FirstName, string LastName, DateOnly BirthOn)
    : ICommand
{
    public static ChangePersonalDetails Issue(string username, string firstName, string lastName, DateOnly birthOn)
    {
        return new ChangePersonalDetails(ICommand.GenerateId(), username, firstName, lastName, birthOn);
    }
}