using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Infrastructure;

namespace HexagonalArchitecture.Adapter.Persistence.EntityFramework.Mapping;

public class UserModeller : IModeller<UserModel>
{
    private readonly User _domain;

    private UserModeller(User domain)
    {
        _domain = domain;
    }

    public static UserModeller ApplyChangesFrom(User domain)
    {
        return new UserModeller(domain);
    }

    public UserModel To(UserModel input)
    {
        if (input is null) return null;

        input.EmailAddress = _domain.EmailAddress.value;
        input.FullName = _domain.FullName;
        input.Age = _domain.Age.value;

        return input;
    }
}