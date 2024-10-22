namespace HexagonalArchitecture.Adapter.Web.Rest.Model;

public record ChangePersonalDetailsModel(string FirstName, string LastName, DateOnly BirthOn)
{
    public static ChangePersonalDetailsModel From(string firstName, string lastName, DateOnly birthOn)
    {
        return new ChangePersonalDetailsModel(firstName, lastName, birthOn);
    }
}