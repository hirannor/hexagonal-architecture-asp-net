using HexagonalArchitecture.Application.Error;
using HexagonalArchitecture.Application.Port;
using HexagonalArchitecture.Application.UseCase;
using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Domain.Command;

namespace HexagonalArchitecture.Application;

public class UserRegistrationService(
    ILogger<UserRegistrationService> logger,
    IAuthentication authentication,
    IUserRepository users
) : IUserRegistration
{
    private const string RegisterUserCmdIsNull = "RegisterUser command should not be null!";

    public async Task<Result> Register(RegisterUser cmd)
    {
        if (cmd == null)
        {
            logger.LogError(RegisterUserCmdIsNull);
            ArgumentNullException.ThrowIfNull(RegisterUserCmdIsNull);
        }

        var existingUser = await users.FindBy(cmd.EmailAddress);
        
        if (existingUser is not null)
        {
            logger.LogError("Email address: {email} already in use", cmd.EmailAddress.Value);
            throw new UserWithEmailAddressAlreadyExist(
                $"Email address: {cmd.EmailAddress.Value} already in use", cmd.EmailAddress.Value);
        }

        var registrationResult = await authentication.Register(cmd);
        
        if (!registrationResult.IsSuccess)
        {
            logger.LogError("User registration failed");
            throw new UserRegistrationFailed("User registration failed");
        }

        var domainUser = User.Create(CreateUser.Issue(cmd.EmailAddress, cmd.FullName, cmd.Age));
        await users.Insert(domainUser);

        logger.LogInformation("User {email} was registered successfully", cmd.EmailAddress.Value);
        
        return Result.Success();
    }
}