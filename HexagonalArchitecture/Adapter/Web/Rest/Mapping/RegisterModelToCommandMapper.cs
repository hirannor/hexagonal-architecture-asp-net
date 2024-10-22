using HexagonalArchitecture.Adapter.Web.Rest.Model;
using HexagonalArchitecture.Domain.Command;
using HexagonalArchitecture.Infrastructure;

namespace HexagonalArchitecture.Adapter.Web.Rest.Mapping;

public class RegisterModelToCommandMapper : IFunction<RegisterCustomerModel, RegisterCustomer>
{
    public RegisterCustomer Apply(RegisterCustomerModel? input)
    {
        if (input is null) return null;

        return RegisterCustomer.Issue(
            input.Username,
            input.EmailAddress,
            input.Password,
            input.FirstName,
            input.LastName,
            input.BirthOn
        );
    }
}