namespace HexagonalArchitecture.Adapter.Web.Rest.Model;

public record ChangeEmailAddressModel(string oldEmailAddress, string newEmailAddress)
{
    public static ChangeEmailAddressModel From(string oldEmailAddress, string newEmailAddress)
    {
        return new ChangeEmailAddressModel(oldEmailAddress, newEmailAddress);
    }
}