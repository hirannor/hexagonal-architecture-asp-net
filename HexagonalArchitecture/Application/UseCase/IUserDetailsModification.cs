using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Domain.Command;

namespace HexagonalArchitecture.Application.UseCase;

public interface IUserDetailsModification
{
    Task<User> ChangeBy(ChangeUserDetails cmd);
}