using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Domain.Command;

namespace HexagonalArchitecture.Application.UseCase
{
    public interface IUserCreation
    {
        Task<User> CreateBy(CreateUser cmd);
    }
}