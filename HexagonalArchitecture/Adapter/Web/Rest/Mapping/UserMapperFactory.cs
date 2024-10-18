using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Domain.Command;
using HexagonalArchitecture.Infrastructure;

namespace HexagonalArchitecture.Adapter.Web.Rest.Mapping;

public static class UserMapperFactory
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

    public static IFunction<User, UserModel> CreateUserToModelMapper()
    {
        return new UserToModelMapper();
    }
}