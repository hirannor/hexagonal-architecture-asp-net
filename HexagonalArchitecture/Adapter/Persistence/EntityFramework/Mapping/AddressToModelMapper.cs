using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Infrastructure;

namespace HexagonalArchitecture.Adapter.Persistence.EntityFramework.Mapping;

public class AddressToModelMapper : IFunction<Address, AddressModel>
{
    public AddressModel Apply(Address? input)
    {
        if (input is null) return null;

        return AddressModel.From(
            input.Street.Name,
            input.Street.Number,
            input.City.Name,
            input.PostalCode.Code,
            input.Country.Name
        );
    }
}