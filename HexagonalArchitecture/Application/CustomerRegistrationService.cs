using System.Transactions;
using HexagonalArchitecture.Application.Error;
using HexagonalArchitecture.Application.Port;
using HexagonalArchitecture.Application.UseCase;
using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Domain.Command;

namespace HexagonalArchitecture.Application;

internal class CustomerRegistrationService(
    ILogger<CustomerRegistrationService> logger,
    IAuthentication authentication,
    ICustomerRepository customers,
    IEventPublishing eventPublishing
) : ICustomerRegistration
{
    public async Task Register(RegisterCustomer cmd)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var email = EmailAddress.From(cmd.EmailAddress);

        if (await customers.FindBy(email) is not null)
        {
            logger.LogError("Email address: {email} already in use", email.Value);
            throw new EmailAddressAlreadyExist(
                $"Email address: {email.Value} already in use");
        }

        if (await customers.FindBy(cmd.Username) is not null)
        {
            logger.LogError("Username: {username} already in use", cmd.Username);
            throw new EmailAddressAlreadyExist(
                $"Username: {cmd.Username} already in use");
        }

        var domain = Customer.Register(cmd);
        await customers.Save(domain);

        var registrationResult = await authentication.Register(cmd);

        if (!registrationResult.IsSuccess)
        {
            logger.LogError("User registration failed");
            throw new RegistrationFailed(registrationResult.Errors);
        }

        logger.LogInformation("Customer with {username} and {email} was registered successfully", cmd.Username, email);
        
        eventPublishing.Publish(domain.ListEvents());
        domain.ClearEvents();
        
        scope.Complete();
    }
}