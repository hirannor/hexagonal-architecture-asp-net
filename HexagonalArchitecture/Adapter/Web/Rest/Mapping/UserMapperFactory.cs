using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Domain.Command;
using HexagonalArchitecture.Infrastructure;

namespace HexagonalArchitecture.Adapter.Web.Rest.Mapping;

public static class UserMapperFactory
{
    public static IFunction<User, UserModel> UserToModelMapper()
    {
        return new UserToModelMapper();
    }

    public static IFunction<CreateUserModel, CreateUser> CreateUserModelToDomainMapper()
    {
        return new CreateUserModelToDomainMapper();
    }
}