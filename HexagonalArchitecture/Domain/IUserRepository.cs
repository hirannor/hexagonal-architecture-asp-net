namespace HexagonalArchitecture.Domain
{
    public interface IUserRepository : IDisposable
    {
        Task Insert(User domain);

        Task<List<User>> ListAll();

        Task<User> FindById(UserId id);

        Task DeleteBy(UserId id);
    }
}