using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Infrastructure.Adapter;

namespace HexagonalArchitecture.Adapter.Persistence.InMemory;

[Adapter(type: AdapterType.Driven)]
internal class CustomerInMemoryRepository : ICustomerRepository
{
    private Dictionary<string, CustomerModel> _users = new();

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public Task<Customer?> FindBy(CustomerId id)
    {
        throw new NotImplementedException();
    }

    public Task<Customer?> FindBy(EmailAddress emailAddress)
    {
        throw new NotImplementedException();
    }

    public Task<Customer?> FindBy(string username)
    {
        throw new NotImplementedException();
    }

    public Task Save(Customer domain)
    {
        throw new NotImplementedException();
    }

    public Task Update(Customer domain)
    {
        throw new NotImplementedException();
    }
}