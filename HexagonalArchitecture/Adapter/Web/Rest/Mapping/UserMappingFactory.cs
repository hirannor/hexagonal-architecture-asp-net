using HexagonalArchitecture.Adapter.Web.Rest.Model;
using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Domain.Command;
using HexagonalArchitecture.Infrastructure;

namespace HexagonalArchitecture.Adapter.Web.Rest.Mapping;

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

    public static IFunction<User, UserModel> CreateUserToModelMapper()
    {
        return new UserToModelMapper();
    }
}