using HexagonalArchitecture.Adapter.Web.Rest.Model;
using HexagonalArchitecture.Infrastructure;

namespace HexagonalArchitecture.Adapter.Web.Rest.Mapping.User;

internal class UserToModelMapper : IFunction<Domain.User, UserModel>
{
    public UserModel Apply(Domain.User input)
    {
        if (input is null) return null;

        return UserModel.From(input.UserId.Value, input.EmailAddress.Value, input.FullName, input.Age.value);
    }
}