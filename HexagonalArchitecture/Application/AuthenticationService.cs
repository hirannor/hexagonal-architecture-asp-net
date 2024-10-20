using HexagonalArchitecture.Application.Error;
using HexagonalArchitecture.Application.Port;
using HexagonalArchitecture.Application.UseCase;
using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Domain.Command;

namespace HexagonalArchitecture.Application;

public class AuthenticationService(
    ILogger<AuthenticationService> logger, 
    IAuthentication authentication
    ) : IUserSignIn
{
    private const string SignInUserCmdIsNull = "SignInUserCmdIsNull command should be not null!";

    public async Task<AuthUser> SignIn(SignInUser cmd)
    {
        if (cmd is null)
        {
            logger.LogError(SignInUserCmdIsNull);
            ArgumentNullException.ThrowIfNull(SignInUserCmdIsNull);
        }
        
        logger.LogInformation("Attempting to sign in with: {cmd.EmailAddress.Value}", cmd.EmailAddress.Value);
        var result = await authentication.Login(cmd);

        if (!result.IsSuccess)
        {
            logger.LogError("User registration failed!");
            throw new UserAuthenticationFailed(result.Errors);
        }
        
        logger.LogInformation("Sign in with: {cmd.EmailAddress.Value} was successful!", cmd.EmailAddress.Value);

        return result.Value;
    }
}