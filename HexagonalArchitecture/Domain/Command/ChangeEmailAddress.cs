using HexagonalArchitecture.Infrastructure;

namespace HexagonalArchitecture.Domain.Command;

public record ChangeEmailAddress(Guid Id, string Username, string OldEmailAddress, string NewEmailAddress) : ICommand
{
    public static ChangeEmailAddress Issue(string username, string oldEmailAddress, string newEmailAddress)
    {
        return new ChangeEmailAddress(ICommand.GenerateId(), username, oldEmailAddress, newEmailAddress);
    }
}