using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Domain.Command;

namespace HexagonalArchitecture.Application.Port;

public interface IAuthentication
{
    Task<Result> Register(RegisterUser cmd);
    Task<Result<AuthUser>> Login(SignInUser cmd);
}