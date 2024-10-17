using HexagonalArchitecture.Domain.Command;
using HexagonalArchitecture.Infrastructure;

namespace HexagonalArchitecture.Adapter.Web.Rest.Mapping;

internal class CreateUserModelToDomainMapper : IFunction<CreateUserModel, CreateUser>
{
    public CreateUser Apply(CreateUserModel input)
    {
        if (input is null) return null;

        return CreateUser.Create(input.EmailAddress, input.FullName, input.Age);
    }
}