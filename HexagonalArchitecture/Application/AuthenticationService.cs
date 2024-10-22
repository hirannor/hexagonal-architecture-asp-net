using HexagonalArchitecture.Application.Error;
using HexagonalArchitecture.Application.Port;
using HexagonalArchitecture.Application.UseCase;
using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Domain.Command;

namespace HexagonalArchitecture.Application;

internal class AuthenticationService(
    ILogger<AuthenticationService> logger,
    IAuthentication authentication
) : ICustomerSignIn
{
    public async Task<AuthUser> SignIn(SignInCustomer cmd)
    {
        logger.LogInformation("Attempting to sign in with username: {cmd}", cmd.Username);
        var result = await authentication.Login(cmd);

        if (!result.IsSuccess)
        {
            logger.LogError("User registration failed!");
            throw new AuthenticationFailed(result.Errors);
        }

        logger.LogInformation("Sign in with username: {md.Username} was successful!", cmd.Username);

        return result.Value;
    }
}