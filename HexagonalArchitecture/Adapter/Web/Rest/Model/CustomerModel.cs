namespace HexagonalArchitecture.Adapter.Web.Rest.Model;

public record CustomerModel(
    string Username,
    string EmailAddress,
    string FirstName,
    string LastName,
    DateOnly BirthOn)
{
    public static CustomerModel From(
        string username,
        string emailAddress,
        string firstName,
        string lastName,
        DateOnly birthOn)
    {
        return new CustomerModel(username, emailAddress, firstName, lastName, birthOn);
    }
}