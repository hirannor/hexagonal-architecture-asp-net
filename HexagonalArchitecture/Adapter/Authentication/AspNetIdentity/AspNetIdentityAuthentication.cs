using HexagonalArchitecture.Application.Port;
using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Domain.Command;
using Microsoft.AspNetCore.Identity;

namespace HexagonalArchitecture.Adapter.Authentication.AspNetIdentity;

public class AspNetIdentityAuthentication(
    SignInManager<ApplicationUserModel> signInManager,
    UserManager<ApplicationUserModel> userManager)
    : IAuthentication
{

    private const string EmailAddressAndPasswordIsEmpty = "Email address and password cannot be empty.";
    private const string UserNotFound = "User not found.";
    private const string InvalidLoginAttempt = "Invalid login attempt. Please check your credentials.";
    private const string SignInNotAllowed = "Sign in not allowed. Verify your account first.";
    private const string AccountIsLocked = "This account is locked out.";
        
    public async Task<Result> Register(RegisterUser cmd)
    {
        if (string.IsNullOrWhiteSpace(cmd.EmailAddress.Value) || string.IsNullOrWhiteSpace(cmd.Password))
        {
            throw new ArgumentException(EmailAddressAndPasswordIsEmpty);
        }

        var emailAddressValue = cmd.EmailAddress.Value;
        var user = ApplicationUserModel.From(emailAddressValue, emailAddressValue);

        var result = await userManager.CreateAsync(user, cmd.Password);

        return result.Succeeded ? Result.Success() : Result.Failure(result.Errors.Select(e => e.Description).ToList());
    }

    public async Task<Result<AuthUser>> Login(SignInUser cmd)
    {
        if (string.IsNullOrWhiteSpace(cmd.EmailAddress.Value) || string.IsNullOrWhiteSpace(cmd.Password))
        {
            throw new ArgumentException(EmailAddressAndPasswordIsEmpty);
        }
    
        var user = await userManager.FindByNameAsync(cmd.EmailAddress.Value);
    
        if (user == null)
        {
            return Result<AuthUser>.Failure([UserNotFound]);
        }
    
        var signInResult = await signInManager.PasswordSignInAsync(
            cmd.EmailAddress.Value,
            cmd.Password,
            isPersistent: false,
            lockoutOnFailure: false
        );
    
        return signInResult switch
        {
            { Succeeded: true } => Result<AuthUser>.Success(new AuthUser(user.UserName, user.Email)),
            { IsLockedOut: true } => Result<AuthUser>.Failure([AccountIsLocked]),
            { IsNotAllowed: true } => Result<AuthUser>.Failure([SignInNotAllowed]),
            _ => Result<AuthUser>.Failure([InvalidLoginAttempt]),
        };
    }
}