namespace HexagonalArchitecture.Adapter.Web.Rest.Model;

public record AddressModel(StreetModel? Street, string? PostalCode, string? City, string? Country)
{
    public static AddressModel From(StreetModel? street, string? postalCode, string? city, string? country)
    {
        return new AddressModel(street, postalCode, city, country);
    }
}