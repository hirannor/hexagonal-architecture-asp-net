using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Infrastructure;

namespace HexagonalArchitecture.Adapter.Persistence.EntityFramework.Mapping;

internal class CustomerModelToDomainMapper : IFunction<CustomerModel, Customer>
{
    private readonly IFunction<AddressModel, Address> _mapAddressModelToDomain;

    public CustomerModelToDomainMapper()
    {
        _mapAddressModelToDomain = new AddressModelToDomainMapper();
    }

    public Customer Apply(CustomerModel? input)
    {
        if (input is null) return null;

        Address domain = _mapAddressModelToDomain.Apply(input.Address);

        return Customer.From(
            CustomerId.From(input.CustomerId),
            Username.From(input.Username),
            EmailAddress.From(input.EmailAddress),
            FirstName.From(input.FirstName),
            LastName.From(input.LastName),
            DateOfBirth.From(input.BirthOn),
            domain
        );
    }
}