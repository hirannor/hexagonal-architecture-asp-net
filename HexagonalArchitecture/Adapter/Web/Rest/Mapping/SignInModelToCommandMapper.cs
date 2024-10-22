using HexagonalArchitecture.Adapter.Web.Rest.Model;
using HexagonalArchitecture.Domain.Command;
using HexagonalArchitecture.Infrastructure;

namespace HexagonalArchitecture.Adapter.Web.Rest.Mapping;

public class SignInModelToCommandMapper : IFunction<SignInModel, SignInCustomer>
{
    public SignInCustomer Apply(SignInModel? input)
    {
        if (input is null) return null;

        return SignInCustomer.Issue(input.Username, input.Password);
    }
}