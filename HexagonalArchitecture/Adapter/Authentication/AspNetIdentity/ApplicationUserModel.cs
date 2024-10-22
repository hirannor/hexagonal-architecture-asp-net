using Microsoft.AspNetCore.Identity;

namespace HexagonalArchitecture.Adapter.Authentication.AspNetIdentity;

public class ApplicationUserModel : IdentityUser
{
    public static ApplicationUserModel From(string userName, string emailAddress)
    {
        var user = new ApplicationUserModel
        {
            UserName = userName,
            Email = emailAddress
        };

        return user;
    }
}