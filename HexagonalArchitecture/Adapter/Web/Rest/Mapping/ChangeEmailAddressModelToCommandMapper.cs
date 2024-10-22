using HexagonalArchitecture.Adapter.Web.Rest.Model;
using HexagonalArchitecture.Domain.Command;
using HexagonalArchitecture.Infrastructure;

namespace HexagonalArchitecture.Adapter.Web.Rest.Mapping;

public class ChangeEmailAddressModelToCommandMapper(string username)
    : IFunction<ChangeEmailAddressModel, ChangeEmailAddress>
{
    public ChangeEmailAddress Apply(ChangeEmailAddressModel? input)
    {
        if (input is null) return null;

        return ChangeEmailAddress.Issue(username, input.oldEmailAddress, input.newEmailAddress);
    }
}