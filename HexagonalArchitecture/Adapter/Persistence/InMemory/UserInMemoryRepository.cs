using HexagonalArchitecture.Domain;

namespace HexagonalArchitecture.Adapter.Persistence.InMemory;

public class UserInMemoryRepository : IUserRepository
{
    private Dictionary<string, UserModel> _users = new();

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public Task<User> ChangeUserDetails(User domain)
    {
        throw new NotImplementedException();
    }

    public Task<List<User>> ListAll()
    {
        throw new NotImplementedException();
    }

    public Task DeleteBy(UserId id)
    {
        throw new NotImplementedException();
    }

    public Task<User> FindBy(EmailAddress emailAddress)
    {
        throw new NotImplementedException();
    }

    public Task<User> FindBy(UserId id)
    {
        throw new NotImplementedException();
    }

    public Task Insert(User domain)
    {
        throw new NotImplementedException();
    }
}