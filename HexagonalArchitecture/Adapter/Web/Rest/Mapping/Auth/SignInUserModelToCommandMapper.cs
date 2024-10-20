using HexagonalArchitecture.Adapter.Web.Rest.Model;
using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Domain.Command;
using HexagonalArchitecture.Infrastructure;

namespace HexagonalArchitecture.Adapter.Web.Rest.Mapping.Auth;

public class SignInUserModelToCommandMapper : IFunction<SignInUserModel, SignInUser>
{
    public SignInUser Apply(SignInUserModel input)
    {        
        if (input is null) return null;
        
        return SignInUser.Issue(EmailAddress.From(input.emailAddress), input.password);

    }
}