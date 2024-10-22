using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Infrastructure;

namespace HexagonalArchitecture.Adapter.Persistence.EntityFramework.Mapping;

public class CustomerToModelMapper : IFunction<Customer, CustomerModel>
{
    public CustomerModel Apply(Customer? input)
    {
        if (input is null) return null;

        return new CustomerModel(
            input.CustomerId.Value,
            input.UserName.Value,
            input.EmailAddress.Value,
            input.FirstName.Value,
            input.LastName.Value,
            input.BirthOn.Value
        );
    }
}