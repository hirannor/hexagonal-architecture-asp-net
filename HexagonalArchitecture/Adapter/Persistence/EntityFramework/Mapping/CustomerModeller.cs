using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Infrastructure;

namespace HexagonalArchitecture.Adapter.Persistence.EntityFramework.Mapping;

public class CustomerModeller : IModeller<CustomerModel>
{
    private readonly IFunction<Address, AddressModel> _mapAddressToModel;
    private readonly Customer _domainCustomer;

    public CustomerModeller(Customer domainCustomer)
    {
        _domainCustomer = domainCustomer;
        _mapAddressToModel = new AddressToModelMapper();
    }

    public static CustomerModeller ApplyChangesFrom(Customer domainCustomer)
    {
        return new CustomerModeller(domainCustomer);
    }

    public CustomerModel To(CustomerModel? model)
    {
        if (model is null) return null;

        model.EmailAddress = _domainCustomer.EmailAddress.Value;
        model.FirstName = _domainCustomer.FirstName.Value;
        model.LastName = _domainCustomer.LastName.Value;
        model.BirthOn = _domainCustomer.BirthOn.Value;

        model.Address = _mapAddressToModel.Apply(_domainCustomer.Address);

        return model;
    }
}