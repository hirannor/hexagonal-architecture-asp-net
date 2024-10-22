using HexagonalArchitecture.Adapter.Web.Rest.Model;
using HexagonalArchitecture.Domain.Command;
using HexagonalArchitecture.Infrastructure;

namespace HexagonalArchitecture.Adapter.Web.Rest.Mapping;

public class ChangePersonalDetailsModelToCommandMapper(string username)
    : IFunction<ChangePersonalDetailsModel, ChangePersonalDetails>
{
    public ChangePersonalDetails Apply(ChangePersonalDetailsModel? input)
    {
        if (input is null) return null;

        return ChangePersonalDetails.Issue(username, input.FirstName, input.LastName, input.BirthOn);
    }
}