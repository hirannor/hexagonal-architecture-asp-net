using HexagonalArchitecture.Adapter.Web.Rest.Model;
using HexagonalArchitecture.Domain.Command;
using HexagonalArchitecture.Infrastructure;

namespace HexagonalArchitecture.Adapter.Web.Rest.Mapping.User;

public static class UserMappingFactory
{
    public static IFunction<ChangeUserDetailsModel, ChangeUserDetails> CreateChangeUserDetailsModelToDomainMapper(
        string userId)
    {
        return new ChangeUserDetailsModelToDomainMapper(userId);
    }

    public static IFunction<CreateUserModel, CreateUser> CreateUserModelToDomainMapper()
    {
        return new CreateUserModelToDomainMapper();
    }

    public static IFunction<Domain.User, UserModel> CreateUserToModelMapper()
    {
        return new UserToModelMapper();
    }
}