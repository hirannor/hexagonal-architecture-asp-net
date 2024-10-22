using HexagonalArchitecture.Adapter.Web.Rest.Model;
using HexagonalArchitecture.Domain.Command;
using HexagonalArchitecture.Infrastructure;

namespace HexagonalArchitecture.Adapter.Web.Rest.Mapping;

public class ChangePasswordModelToCommandMapper(string username) : IFunction<ChangePasswordModel, ChangePassword>
{
    public ChangePassword Apply(ChangePasswordModel? input)
    {
        if (input is null) return null;

        return ChangePassword.Issue(username, input.OldPassword, input.NewPassword);
    }
}