namespace HexagonalArchitecture.Adapter.Web.Rest.Model;

public record ChangePersonalDetailsModel(string? FirstName, string? LastName, DateOnly? BirthOn, AddressModel? Address)
{
    public static ChangePersonalDetailsModel From(string? firstName, string? lastName, DateOnly? birthOn,
        AddressModel? address)
    {
        return new ChangePersonalDetailsModel(firstName, lastName, birthOn, address);
    }
}