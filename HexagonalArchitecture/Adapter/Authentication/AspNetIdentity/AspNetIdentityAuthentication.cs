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

    public async Task<Result> ChangeEmailAddress(ChangeEmailAddress cmd)
    {
        if (string.IsNullOrWhiteSpace(cmd.OldEmailAddress) || string.IsNullOrWhiteSpace(cmd.NewEmailAddress))
        {
            return Result.Failure(["Old email address and new email address cannot be empty."]);
        }

        var user = await userManager.FindByNameAsync(cmd.Username);

        if (user == null)
        {
            return Result.Failure([UserNotFound]);
        }

        if (user.Email != cmd.OldEmailAddress)
        {
            return Result.Failure(["Old email address does not match the current one."]);
        }

        var token = await userManager.GenerateChangeEmailTokenAsync(user, cmd.NewEmailAddress);
        var result = await userManager.ChangeEmailAsync(user, cmd.NewEmailAddress, token);

        if (!result.Succeeded) return Result.Failure(result.Errors.Select(e => e.Description).ToList());

        await userManager.UpdateAsync(user);
        return Result.Success();
    }

    public async Task<Result> ChangePassword(ChangePassword cmd)
    {
        if (string.IsNullOrWhiteSpace(cmd.OldPassword) || string.IsNullOrWhiteSpace(cmd.NewPassword))
        {
            return Result.Failure(["Old password or new password cannot be empty."]);
        }

        var user = await userManager.FindByNameAsync(cmd.Username);

        if (user == null)
        {
            return Result.Failure([UserNotFound]);
        }

        var result = await userManager.ChangePasswordAsync(user, cmd.OldPassword, cmd.NewPassword);

        return result.Succeeded
            ? Result.Success()
            : Result.Failure(result.Errors.Select(e => e.Description).ToList());
    }

    public async Task<Result<AuthUser>> Login(SignInCustomer cmd)
    {
        if (string.IsNullOrWhiteSpace(cmd.Username) || string.IsNullOrWhiteSpace(cmd.Password))
        {
            throw new ArgumentException("Username or password cannot be empty!");
        }

        var user = await userManager.FindByNameAsync(cmd.Username);

        if (user == null)
        {
            return Result<AuthUser>.Failure([UserNotFound]);
        }

        var signInResult = await signInManager.PasswordSignInAsync(
            cmd.Username,
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

    public async Task<Result> Register(RegisterCustomer cmd)
    {
        if (string.IsNullOrWhiteSpace(cmd.EmailAddress) || string.IsNullOrWhiteSpace(cmd.Password))
        {
            throw new ArgumentException(EmailAddressAndPasswordIsEmpty);
        }

        var user = ApplicationUserModel.From(cmd.Username, cmd.EmailAddress);

        var result = await userManager.CreateAsync(user, cmd.Password);

        return result.Succeeded ? Result.Success() : Result.Failure(result.Errors.Select(e => e.Description).ToList());
    }
}