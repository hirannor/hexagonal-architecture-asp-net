namespace HexagonalArchitecture.Adapter.Web.Rest.Model;

public record SignInUserModel(string emailAddress, string password)
{
    public static SignInUserModel From(string emailAddress, string password)
    {
        return new SignInUserModel(emailAddress, password);
    }
    
}