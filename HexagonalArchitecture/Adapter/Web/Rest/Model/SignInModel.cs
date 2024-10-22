namespace HexagonalArchitecture.Adapter.Web.Rest.Model;

public record SignInModel(string Username, string Password)
{
    public static SignInModel From(string username, string password)
    {
        return new SignInModel(username, password);
    }
}