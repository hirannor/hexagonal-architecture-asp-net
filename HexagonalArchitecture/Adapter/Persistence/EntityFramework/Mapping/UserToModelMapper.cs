using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Infrastructure;

namespace HexagonalArchitecture.Adapter.Persistence.EntityFramework.Mapping;

public class UserToModelMapper : IFunction<User, UserModel>
{
    public UserModel Apply(User input)
    {
        if (input is null) return null;

        return new UserModel(input.UserId.Value, input.EmailAddress.value, input.FullName, input.Age.value);
    }
}