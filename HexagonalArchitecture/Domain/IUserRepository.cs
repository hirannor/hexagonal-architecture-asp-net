namespace HexagonalArchitecture.Domain
{
    public interface IUserRepository : IDisposable
    {
        Task<List<User>> ListAll();

        Task Delete(UserId id);

        Task<User> FindBy(EmailAddress emailAddress);
        
        Task<User> FindBy(UserId id);
        
        Task Insert(User domain);

    }
}