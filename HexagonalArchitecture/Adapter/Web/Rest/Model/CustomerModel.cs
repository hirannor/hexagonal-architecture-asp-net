using System.Text.Json.Serialization;

namespace HexagonalArchitecture.Adapter.Web.Rest.Model;

public class CustomerModel
{
    public string Username { get; init; }
    public string EmailAddress { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public DateOnly BirthOn { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public AddressModel? Address { get; init; }

    public CustomerModel(
        string username,
        string emailAddress,
        string firstName,
        string lastName,
        DateOnly birthOn,
        AddressModel? address)
    {
        Username = username;
        EmailAddress = emailAddress;
        FirstName = firstName;
        LastName = lastName;
        BirthOn = birthOn;
        Address = address;
    }

    public static CustomerModel From(
        string username,
        string emailAddress,
        string firstName,
        string lastName,
        DateOnly birthOn,
        AddressModel? address)
    {
        return new CustomerModel(username, emailAddress, firstName, lastName, birthOn, address);
    }
}