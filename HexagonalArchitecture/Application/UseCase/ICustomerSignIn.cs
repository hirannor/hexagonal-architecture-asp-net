using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Domain.Command;

namespace HexagonalArchitecture.Application.UseCase;

public interface ICustomerSignIn
{
    Task<AuthUser> SignIn(SignInCustomer cmd);
}