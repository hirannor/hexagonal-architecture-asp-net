using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Infrastructure;

namespace HexagonalArchitecture.Adapter.Persistence.EntityFramework.Mapping;

public static class UserMapperFactory
{
    public static IFunction<User, UserModel> UserToModelMapper()
    {
        return new UserToModelMapper();
    }

    public static IFunction<UserModel, User> UserModelToDomainMapper()
    {
        return new UserModelToDomainMapper();
    }
}