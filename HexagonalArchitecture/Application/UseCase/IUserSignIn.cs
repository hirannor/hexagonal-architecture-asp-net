using HexagonalArchitecture.Application.Port;
using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Domain.Command;

namespace HexagonalArchitecture.Application.UseCase;

public interface IUserSignIn
{
    Task<Result<AuthUser>> SignIn(SignInUser cmd);
}