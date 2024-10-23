using System.Transactions;
using HexagonalArchitecture.Application.Error;
using HexagonalArchitecture.Application.Port;
using HexagonalArchitecture.Application.UseCase;
using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Domain.Command;

namespace HexagonalArchitecture.Application;

internal class CustomerService(
    IAuthentication authentication,
    ICustomerRepository customers,
    ILogger<CustomerService> logger,
    IEventPublishing events) :
    ICustomerDisplay,
    IChangeEmailAddress,
    IChangePersonalDetails
{
    public async Task ChangeBy(ChangeEmailAddress cmd)
    {
        logger.LogInformation("Attempting to change email address for customer with username: {Username}",
            cmd.Username);

        using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        Customer? domain = await customers.FindBy(cmd.Username);

        if (domain is null)
        {
            logger.LogWarning("Customer not found with username: {Username}", cmd.Username);
            throw new CustomerNotFound($"Customer has been not found with {cmd.Username}.");
        }

        EmailAddress newEmail = EmailAddress.From(cmd.NewEmailAddress);

        if (domain.EmailAddress.Equals(newEmail))
        {
            logger.LogWarning("Email address: {NewEmailAddress} has already been taken by user: {Username}",
                cmd.NewEmailAddress, cmd.Username);
            throw new EmailAddressAlreadyExist($"Email address: {cmd.NewEmailAddress} has been already taken.");
        }

        logger.LogInformation("Changing email address in authentication system for user: {Username}", cmd.Username);

        Result result = await authentication.ChangeEmailAddress(cmd);

        if (!result.IsSuccess)
        {
            logger.LogError("Failed to change the email address in the authentication system for user: {Username}",
                cmd.Username);
            throw new AuthenticationFailed("Failed to change the email address in the authentication system.");
        }

        domain.ChangeBy(cmd);
        await customers.Update(domain);

        logger.LogInformation("Successfully changed email address for user: {Username}", cmd.Username);

        events.Publish(domain.ListEvents());
        domain.ClearEvents();

        scope.Complete();
    }

    public async Task<Customer> ChangeBy(ChangePersonalDetails cmd)
    {
        logger.LogInformation("Attempting to change personal details for customer with username: {Username}",
            cmd.Username);

        Customer? domain = await customers.FindBy(cmd.Username);

        if (domain is null)
        {
            logger.LogWarning("Customer not found with username: {Username}", cmd.Username);
            throw new CustomerNotFound($"Customer has been not found with {cmd.Username}.");
        }

        logger.LogInformation("Customer found. Proceeding to change personal details for: {Username}", cmd.Username);

        domain.ChangeBy(cmd);
        await customers.Update(domain);

        logger.LogInformation("Successfully changed personal details for: {Username}", cmd.Username);

        events.Publish(domain.ListEvents());
        domain.ClearEvents();

        return domain;
    }

    public async Task<Customer> DisplayBy(string username)
    {
        logger.LogInformation("Retrieving user with {username}", username);

        return await customers.FindBy(username);
    }

    public async Task<Customer> DisplayBy(CustomerId id)
    {
        logger.LogInformation("Retrieving user with {id}", id);

        return await customers.FindBy(id);
    }

    public async Task<Customer> DisplayBy(EmailAddress emailAddress)
    {
        logger.LogInformation("Retrieving user with {emailAddress}", emailAddress.Value);

        return await customers.FindBy(emailAddress);
    }
}