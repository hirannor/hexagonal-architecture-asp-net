namespace HexagonalArchitecture.Adapter.Web.Rest.Model;

public record StreetModel(string? StreetName, string? StreetNumber)
{
    public static StreetModel From(string? streetName, string? streetNumber)
    {
        return new StreetModel(streetName, streetNumber);
    }
}