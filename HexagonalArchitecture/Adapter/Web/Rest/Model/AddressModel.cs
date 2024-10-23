using System.Text.Json.Serialization;

namespace HexagonalArchitecture.Adapter.Web.Rest.Model;

public class AddressModel(StreetModel? street, string? postalCode, string? city, string? country)
{
    public static AddressModel From(StreetModel? street, string? postalCode, string? city, string? country)
    {
        return new AddressModel(street, postalCode, city, country);
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public StreetModel? Street { get; } = street;


    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? PostalCode { get; } = postalCode;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? City { get; } = city;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

    public string? Country { get; } = country;
}