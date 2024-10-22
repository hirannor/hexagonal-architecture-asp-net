using System.Transactions;
using HexagonalArchitecture.Application.Error;
using HexagonalArchitecture.Application.Port;
using HexagonalArchitecture.Application.UseCase;
using HexagonalArchitecture.Domain.Command;

namespace HexagonalArchitecture.Application;

public class CustomerPasswordService(
    IAuthentication authentication,
    ICustomerDisplay customers,
    ILogger<CustomerPasswordService> logger,
    IEventPublishing events) : IChangePassword
{
    public async Task ChangeBy(ChangePassword cmd)
    {
        logger.LogInformation("Attempting to change password for user: {Username}", cmd.Username);

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var domain = await customers.DisplayBy(cmd.Username);

        if (domain is null)
        {
            logger.LogWarning("Customer not found with username: {Username}", cmd.Username);
            throw new CustomerNotFound($"Customer has not been found with {cmd.Username}.");
        }

        logger.LogInformation("Changing password for user: {Username}", cmd.Username);

        var result = await authentication.ChangePassword(cmd);
        if (!result.IsSuccess)
        {
            logger.LogError("Failed to change password for user: {Username}", cmd.Username);
            throw new AuthenticationFailed("Failed to change the password in the authentication system.");
        }

        logger.LogInformation("Successfully changed password for user: {Username}", cmd.Username);
        
        events.Publish(domain.ListEvents());
        domain.ClearEvents();
        
        scope.Complete();
    }
}