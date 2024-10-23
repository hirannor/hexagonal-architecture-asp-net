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
        using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        EmailAddress email = EmailAddress.From(cmd.EmailAddress);

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

        Customer domain = Customer.Register(cmd);
        await customers.Save(domain);

        Result result = await authentication.Register(cmd);

        if (!result.IsSuccess)
        {
            logger.LogError("User registration failed");
            throw new RegistrationFailed(result.Errors);
        }

        logger.LogInformation("Customer with {username} and {email} was registered successfully", cmd.Username, email);

        eventPublishing.Publish(domain.ListEvents());
        domain.ClearEvents();

        scope.Complete();
    }
}