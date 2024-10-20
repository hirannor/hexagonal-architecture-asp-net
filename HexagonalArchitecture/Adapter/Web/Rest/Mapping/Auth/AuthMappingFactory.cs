using HexagonalArchitecture.Adapter.Web.Rest.Model;
using HexagonalArchitecture.Domain.Command;
using HexagonalArchitecture.Infrastructure;

namespace HexagonalArchitecture.Adapter.Web.Rest.Mapping.Auth;

public static class AuthMappingFactory
{
    public static IFunction<RegisterUserModel, RegisterUser> CreateRegisterUserModelToCommandMapper()
    {
        return new RegisterUserModelToCommandMapper();
    }
    
    public static IFunction<SignInUserModel, SignInUser> CreateSignInUserModelToCommandMapper()
    {
        return new SignInUserModelToCommandMapper();
    }
}