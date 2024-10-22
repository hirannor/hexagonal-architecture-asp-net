using HexagonalArchitecture.Adapter.Web.Rest.Model;
using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Infrastructure;

namespace HexagonalArchitecture.Adapter.Web.Rest.Mapping;

public class CustomerToModelMapper : IFunction<Customer, CustomerModel>
{
    public CustomerModel Apply(Customer? input)
    {
        if (input is null) return null;

        return CustomerModel.From(
            input.UserName.Value,
            input.EmailAddress.Value,
            input.FirstName.Value,
            input.LastName.Value,
            input.BirthOn.Value
        );
    }
}