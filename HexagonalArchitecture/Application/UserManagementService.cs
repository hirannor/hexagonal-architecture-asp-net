using HexagonalArchitecture.Application.Error;
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

        public async Task<User> CreateBy(CreateUser cmd)
        {
            if (cmd == null)
            {
                logger.LogError(CreateUserCommandIsNull);
                ArgumentNullException.ThrowIfNull(CreateUserCommandIsNull);
            }

            var foundUser = await users.FindBy(EmailAddress.From(cmd.EmailAddress));

            if (foundUser is not null)
            {
                logger.LogError("Email address: {email} already in use", cmd.EmailAddress);
                throw new CustomerWithEmailAddressAlreadyExist(
                    $"Email address: {cmd.EmailAddress} already in use", cmd.EmailAddress);
            }

            var domain = User.Create(cmd);

            logger.LogInformation("Attempting to insert user with {id}", domain.userId);
            await users.Insert(domain);
            logger.LogInformation("User with {id} was created successfully", domain.userId);

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

            await users.Delete(id);

            logger.LogInformation("User with id: {id} was deleted successfully", id);
        }

        public async Task<List<User>> DisplayAll()
        {
            logger.LogInformation("Retrieving all users");
            return await users.ListAll();
        }

        public async Task<User> DisplayBy(UserId id)
        {
            if (id == null)
            {
                logger.LogError(UserIdentifierCannotBeNull);
                ArgumentNullException.ThrowIfNull(UserIdentifierCannotBeNull);
            }

            logger.LogInformation("Retrieving user with {id}", id);

            return await users.FindBy(id);
        }
    }
}