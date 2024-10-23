using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Infrastructure;

namespace HexagonalArchitecture.Adapter.Persistence.EntityFramework.Mapping;

public class CustomerToModelMapper : IFunction<Customer, CustomerModel>
{
    private readonly IFunction<Address, AddressModel> _mapAddressToModel;

    public CustomerToModelMapper()
    {
        _mapAddressToModel = new AddressToModelMapper();
    }

    public CustomerModel Apply(Customer? input)
    {
        if (input is null) return null;

        AddressModel address = _mapAddressToModel.Apply(input.Address);

        return new CustomerModel(
            input.CustomerId.Value,
            input.UserName.Value,
            input.EmailAddress.Value,
            input.FirstName.Value,
            input.LastName.Value,
            input.BirthOn.Value,
            address
        );
    }
}