using HexagonalArchitecture.Adapter.Web.Rest.Model;
using HexagonalArchitecture.Domain.Command;
using HexagonalArchitecture.Infrastructure;

namespace HexagonalArchitecture.Adapter.Web.Rest.Mapping;

public class ChangePersonalDetailsModelToCommandMapper(string username)
    : IFunction<ChangePersonalDetailsModel, ChangePersonalDetails>
{
    public ChangePersonalDetails Apply(ChangePersonalDetailsModel? input)
    {
        if (input is null) return null;

        ChangePersonalDetails.Builder builder = ChangePersonalDetails.Empty(username);

        if (input.BirthOn.HasValue)
        {
            builder.WithBirthOn(input.BirthOn.Value);
        }

        if (input.Address is not null)
        {
            builder
                .WithCountry(input.Address.Country)
                .WithPostalCode(input.Address.PostalCode)
                .WithCity(input.Address.City)
                .WithStreetName(input.Address.Street.StreetName)
                .WithStreetNumber(input.Address.Street.StreetNumber);
        }

        if (input.FirstName is not null)
        {
            builder.WithFirstName(input.FirstName);
        }

        if (input.LastName is not null)
        {
            builder.WithLastName(input.LastName);
        }

        return builder.Issue();
    }
}