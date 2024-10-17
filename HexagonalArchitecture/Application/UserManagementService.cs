using HexagonalArchitecture.Application.UseCase;
using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Domain.Command;

namespace HexagonalArchitecture.Application
{
    public class UserManagementService(
        IUserRepository users,
        ILogger<UserManagementService> logger,
        IEventPublishing eventPublishing) :
        IUserCreation,
        IUserDisplay,
        IUserDeletion
    {
        private const string CreateUserCommandIsNull = "CreateUser command cannot be null!";
        private const string UserIdentifierCannotBeNull = "User identifier cannot be null!";

        public async Task<User> Create(CreateUser cmd)
        {
            if (cmd == null)
            {
                logger.LogError(CreateUserCommandIsNull);
                ArgumentNullException.ThrowIfNull(CreateUserCommandIsNull);
            }

            var domain = User.Create(cmd);

            logger.LogInformation("Attempting to insert user with {id}", domain.Id);
            await users.Insert(domain);
            logger.LogInformation("User with {id} was created successfully", domain.Id);

            eventPublishing.Publish(domain.ListEvents());
            domain.ClearEvents();

            return domain;
        }

        public async Task DeleteBy(UserId id)
        {
            if (id == null)
            {
                logger.LogError(UserIdentifierCannotBeNull);
                ArgumentNullException.ThrowIfNull(UserIdentifierCannotBeNull);
            }

            logger.LogInformation("Attempting to delete user with id: {id}", id);

            await users.DeleteBy(id);

            logger.LogInformation("User with id: {id} was deleted successfully", id);
        }

        public async Task<List<User>> DisplayAll()
        {
            logger.LogInformation("Retrieving all users");
            return await users.ListAll();
        }

        public async Task<User> DisplayById(UserId id)
        {
            if (id == null)
            {
                logger.LogError(UserIdentifierCannotBeNull);
                ArgumentNullException.ThrowIfNull(UserIdentifierCannotBeNull);
            }

            logger.LogInformation("Retrieving user with {id}", id);

            return await users.FindById(id);
        }
    }
}