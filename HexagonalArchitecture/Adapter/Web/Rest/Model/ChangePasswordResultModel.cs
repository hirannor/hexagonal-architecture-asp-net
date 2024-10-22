namespace HexagonalArchitecture.Adapter.Web.Rest.Model;

public record ChangePasswordResultModel(string Message)
{
    public static ChangePasswordResultModel From(string message)
    {
        return new ChangePasswordResultModel(message);
    }
}