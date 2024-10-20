using HexagonalArchitecture.Domain.Command;

namespace HexagonalArchitecture.Application.UseCase;

public interface IUserRegistration
{
    Task Register(RegisterUser cmd);
}