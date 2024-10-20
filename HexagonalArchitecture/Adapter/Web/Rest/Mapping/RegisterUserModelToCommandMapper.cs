using HexagonalArchitecture.Adapter.Web.Rest.Model;
using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Domain.Command;
using HexagonalArchitecture.Infrastructure;

namespace HexagonalArchitecture.Adapter.Web.Rest.Mapping;

public class RegisterUserModelToCommandMapper : IFunction<RegisterUserModel, RegisterUser>
{
    public RegisterUser Apply(RegisterUserModel input)
    {
        if (input is null) return null;

        return RegisterUser.Issue(
            EmailAddress.From(input.EmailAddress),
            input.Password,
            input.FullName,
            Age.From(input.Age)
        );

    }
}