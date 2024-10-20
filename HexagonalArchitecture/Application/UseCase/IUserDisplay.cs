using HexagonalArchitecture.Domain;

namespace HexagonalArchitecture.Application.UseCase
{
    public interface IUserDisplay
    {
        Task<List<User>> DisplayAll();

        Task<User> DisplayBy(UserId id);
        
        Task<User> DisplayBy(EmailAddress emailAddress);
    }
}