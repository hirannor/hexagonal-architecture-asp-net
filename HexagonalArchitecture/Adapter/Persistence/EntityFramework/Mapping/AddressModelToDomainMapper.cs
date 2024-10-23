using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Infrastructure;

namespace HexagonalArchitecture.Adapter.Persistence.EntityFramework.Mapping;

public class AddressModelToDomainMapper : IFunction<AddressModel, Address>
{
    public Address Apply(AddressModel? input)
    {
        if (input is null) return null;

        return Address.From(
            Street.From(input.StreetName, input.StreetNumber),
            City.From(input.CityName),
            PostalCode.From(input.PostalCode),
            Country.From(input.CountryName)
        );
    }
}