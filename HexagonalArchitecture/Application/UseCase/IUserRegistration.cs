using HexagonalArchitecture.Application.Port;
using HexagonalArchitecture.Domain.Command;

namespace HexagonalArchitecture.Application.UseCase;

public interface IUserRegistration
{
    Task<Result> Register(RegisterUser cmd);
}