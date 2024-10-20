using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Domain.Command;

namespace HexagonalArchitecture.Application.UseCase;

public interface IUserSignIn
{
    Task<AuthUser> SignIn(SignInUser cmd);
}