using HexagonalArchitecture.Adapter.Web.Rest.Model;
using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Infrastructure;

namespace HexagonalArchitecture.Adapter.Web.Rest.Mapping;

internal class UserToModelMapper : IFunction<User, UserModel>
{
    public UserModel Apply(User input)
    {
        if (input is null) return null;

        return UserModel.From(input.UserId.Value, input.EmailAddress.value, input.FullName, input.Age.value);
    }
}