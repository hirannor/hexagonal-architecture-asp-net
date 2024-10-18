namespace HexagonalArchitecture.Adapter.Web.Rest.Model;

public record UserModel(string UserId, string EmailAddress, string FullName, int Age)
{
    public static UserModel From(
        string userId, 
        string emailAddress, 
        string fullName, 
        int age)
    {
        return new UserModel(userId, emailAddress, fullName, age);
    }
}