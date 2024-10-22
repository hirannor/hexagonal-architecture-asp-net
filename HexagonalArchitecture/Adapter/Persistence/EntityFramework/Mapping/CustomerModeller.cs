using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Infrastructure;

namespace HexagonalArchitecture.Adapter.Persistence.EntityFramework.Mapping;

public class CustomerModeller(Customer domainCustomer) : IModeller<CustomerModel>
{
    public static CustomerModeller ApplyChangesFrom(Customer domainCustomer)
    {
        return new CustomerModeller(domainCustomer);
    }

    public CustomerModel To(CustomerModel? model)
    {
        if (model is null) return null;

        model.EmailAddress = domainCustomer.EmailAddress.Value;
        model.FirstName = domainCustomer.FirstName.Value;
        model.LastName = domainCustomer.LastName.Value;
        model.BirthOn = domainCustomer.BirthOn.Value;

        return model;
    }
}