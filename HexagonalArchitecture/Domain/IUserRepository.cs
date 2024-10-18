namespace HexagonalArchitecture.Domain
{
    public interface IUserRepository : IDisposable
    {
        Task Insert(User domain);

        Task<List<User>> ListAll();

        Task<User> FindBy(UserId id);

        Task Delete(UserId id);
    }
}