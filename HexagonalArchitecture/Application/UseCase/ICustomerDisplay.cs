using HexagonalArchitecture.Domain;

namespace HexagonalArchitecture.Application.UseCase
{
    public interface ICustomerDisplay
    {
        Task<Customer?> DisplayBy(string username);

        Task<Customer?> DisplayBy(CustomerId id);

        Task<Customer?> DisplayBy(EmailAddress emailAddress);
    }
}