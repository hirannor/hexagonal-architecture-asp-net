using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Infrastructure;

namespace HexagonalArchitecture.Adapter.Persistence.EntityFramework.Mapping;

internal class CustomerModelToDomainMapper : IFunction<CustomerModel, Customer>
{
    public Customer Apply(CustomerModel? input)
    {
        if (input is null) return null;

        return Customer.From(
            CustomerId.From(input.CustomerId),
            Username.From(input.Username),
            EmailAddress.From(input.EmailAddress),
            FirstName.From(input.FirstName),
            LastName.From(input.LastName),
            DateOfBirth.From(input.BirthOn)
        );
    }
}