namespace HexagonalArchitecture.Domain;

public interface ICustomerRepository : IDisposable
{
    Task<Customer?> FindBy(string username);

    Task<Customer?> FindBy(EmailAddress emailAddress);

    Task<Customer?> FindBy(CustomerId id);

    Task Save(Customer domain);

    Task Update(Customer domain);
}