namespace HexagonalArchitecture.Adapter.Web.Rest.Model;

public record ChangeEmailAddressResultModel(string Message)
{
    public static ChangeEmailAddressResultModel From(string message)
    {
        return new ChangeEmailAddressResultModel(message);
    }
}