using HexagonalArchitecture.Domain;

namespace HexagonalArchitecture.Application.UseCase;

public interface IUserDeletion
{
    Task DeleteBy(UserId id);
}