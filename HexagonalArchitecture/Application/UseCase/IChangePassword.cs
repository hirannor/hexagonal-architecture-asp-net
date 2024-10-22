using HexagonalArchitecture.Domain.Command;

namespace HexagonalArchitecture.Application.UseCase;

public interface IChangePassword
{
    Task ChangeBy(ChangePassword cmd);
}