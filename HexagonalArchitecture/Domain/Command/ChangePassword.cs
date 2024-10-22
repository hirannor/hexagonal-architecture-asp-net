using HexagonalArchitecture.Infrastructure;

namespace HexagonalArchitecture.Domain.Command;

public record ChangePassword(Guid Id, string Username, string OldPassword, string NewPassword) : ICommand
{
    public static ChangePassword Issue(string username, string oldPassword, string newPassword)
    {
        return new ChangePassword(ICommand.GenerateId(), username, oldPassword, newPassword);
    }
}