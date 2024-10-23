using HexagonalArchitecture.Adapter.Persistence.EntityFramework.Mapping;
using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Infrastructure;
using HexagonalArchitecture.Infrastructure.Adapter;
using Microsoft.EntityFrameworkCore;

namespace HexagonalArchitecture.Adapter.Persistence.EntityFramework;

[Adapter(type: AdapterType.Driven)]
internal sealed class CustomerEfRepository(CustomersDbContext context) : ICustomerRepository
{
    private bool _disposedValue;

    private const string UserIdentifierCannotBeNull = "User identifier cannot be null!";
    private const string UserCannotBeNull = "User cannot be null!";

    private readonly IFunction<Customer, CustomerModel> _mapUserToModel = CustomerMappingFactory.UserToModelMapper();

    private readonly IFunction<CustomerModel, Customer>
        _mapUserModelToDomain = CustomerMappingFactory.UserModelToDomainMapper();

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (_disposedValue) return;

        if (disposing)
        {
            context.Dispose();
        }

        _disposedValue = true;
    }

    public async Task<Customer?> FindBy(CustomerId id)
    {
        ArgumentNullException.ThrowIfNull(id, UserIdentifierCannotBeNull);

        CustomerModel? model = await context.Users.FirstOrDefaultAsync(model => model.CustomerId == id.Value);

        return _mapUserModelToDomain.Apply(model);
    }

    public async Task<Customer?> FindBy(EmailAddress emailAddress)
    {
        ArgumentNullException.ThrowIfNull(emailAddress, "Email address cannot be null!");

        CustomerModel? model =
            await context.Users.FirstOrDefaultAsync(model => model.EmailAddress == emailAddress.Value);

        return _mapUserModelToDomain.Apply(model);
    }

    public async Task<Customer?> FindBy(string username)
    {
        ArgumentNullException.ThrowIfNull(username, UserIdentifierCannotBeNull);

        CustomerModel? model = await context.Users.FirstOrDefaultAsync(model => model.Username == username);

        return _mapUserModelToDomain.Apply(model);
    }

    public async Task Save(Customer domain)
    {
        ArgumentNullException.ThrowIfNull(domain, UserCannotBeNull);

        CustomerModel model = _mapUserToModel.Apply(domain);
        await context.Users.AddAsync(model);

        await context.SaveChangesAsync();
    }

    public async Task Update(Customer domain)
    {
        ArgumentNullException.ThrowIfNull(domain, "User cannot be null!");

        CustomerModel? existingModel =
            await context.Users.FirstOrDefaultAsync(model => model.Username == domain.UserName.Value);

        if (existingModel == null)
        {
            throw new InvalidOperationException($"User with ID {domain.CustomerId.Value} not found.");
        }

        CustomerModeller.ApplyChangesFrom(domain).To(existingModel);
        await context.SaveChangesAsync();
    }
}