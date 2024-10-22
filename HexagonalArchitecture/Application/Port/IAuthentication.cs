using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Domain.Command;

namespace HexagonalArchitecture.Application.Port;

public interface IAuthentication
{
    Task<Result> ChangeEmailAddress(ChangeEmailAddress cmd);
    Task<Result> ChangePassword(ChangePassword cmd);
    Task<Result<AuthUser>> Login(SignInCustomer cmd);
    Task<Result> Register(RegisterCustomer cmd);
}