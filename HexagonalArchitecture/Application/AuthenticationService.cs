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

    public Task<Result<AuthUser>> SignIn(SignInUser cmd)
    {
        if (cmd == null)
        {
            logger.LogError(SignInUserCmdIsNull);
            ArgumentNullException.ThrowIfNull(SignInUserCmdIsNull);
        }
        
        return authentication.Login(cmd);
    }
}