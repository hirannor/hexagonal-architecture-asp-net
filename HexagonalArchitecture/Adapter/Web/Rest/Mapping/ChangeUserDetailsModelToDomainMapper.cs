using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Domain.Command;
using HexagonalArchitecture.Infrastructure;

namespace HexagonalArchitecture.Adapter.Web.Rest.Mapping;

internal class ChangeUserDetailsModelToDomainMapper(string userId)
    : IFunction<ChangeUserDetailsModel, ChangeUserDetails>
{
    public ChangeUserDetails Apply(ChangeUserDetailsModel input)
    {
        if (input is null) return null;

        return ChangeUserDetails.From(
            UserId.From(userId),
            EmailAddress.From(input.EmailAddress),
            input.FullName,
            Age.From(input.Age)
        );
    }
}