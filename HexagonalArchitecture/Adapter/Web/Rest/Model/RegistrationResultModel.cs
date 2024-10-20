namespace HexagonalArchitecture.Adapter.Web.Rest.Model;

public record RegistrationResultModel(string Message)
{
    public static RegistrationResultModel From(string message)
    {
        return new RegistrationResultModel(message);
    }
}