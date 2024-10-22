using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Domain.Command;

namespace HexagonalArchitecture.Application.UseCase;

public interface IChangePersonalDetails
{
    Task<Customer> ChangeBy(ChangePersonalDetails cmd);
}