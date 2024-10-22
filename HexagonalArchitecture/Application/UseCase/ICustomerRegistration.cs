using HexagonalArchitecture.Domain.Command;

namespace HexagonalArchitecture.Application.UseCase;

public interface ICustomerRegistration
{
    Task Register(RegisterCustomer cmd);
}