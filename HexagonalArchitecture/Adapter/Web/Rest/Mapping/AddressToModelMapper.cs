using HexagonalArchitecture.Adapter.Web.Rest.Model;
using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Infrastructure;

namespace HexagonalArchitecture.Adapter.Web.Rest.Mapping;

public class AddressToModelMapper : IFunction<Address, AddressModel>
{
    private readonly IFunction<Street, StreetModel> _mapStreetToModel;

    public AddressToModelMapper()
    {
        _mapStreetToModel = new StreetToModelMapper();
    }

    public AddressModel Apply(Address? input)
    {
        if (input is null) return null;

        return AddressModel.From(
            _mapStreetToModel.Apply(input.Street),
            input.PostalCode.Code,
            input.City.Name,
            input.Country.Name
        );
    }
}