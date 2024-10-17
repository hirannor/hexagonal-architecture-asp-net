using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Infrastructure;

namespace HexagonalArchitecture.Adapter.Persistence.EntityFramework.Mapping;

internal class UserModelToDomainMapper : IFunction<UserModel, User>
{
    public User Apply(UserModel input)
    {
        if (input is null) return null;

        return User.From(UserId.From(input.Id), input.EmailAddress, input.FullName, input.Age);
    }
}