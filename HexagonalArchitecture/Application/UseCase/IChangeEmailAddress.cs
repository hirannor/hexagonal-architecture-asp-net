using HexagonalArchitecture.Domain.Command;

namespace HexagonalArchitecture.Application.UseCase;

public interface IChangeEmailAddress
{
    Task ChangeBy(ChangeEmailAddress cmd);
}