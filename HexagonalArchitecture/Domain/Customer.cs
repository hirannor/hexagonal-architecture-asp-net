using HexagonalArchitecture.Domain.Command;
using HexagonalArchitecture.Domain.Event;
using HexagonalArchitecture.Infrastructure;
using HexagonalArchitecture.Infrastructure.Eventing;

namespace HexagonalArchitecture.Domain;

public class Customer(
    CustomerId customerId,
    Username username,
    EmailAddress emailAddress,
    FirstName firstName,
    LastName lastName,
    DateOfBirth birthOn) : IAggregateRoot
{
    public CustomerId CustomerId { get; } = customerId;

    public Username UserName { get; } = username;

    public EmailAddress EmailAddress { get; set; } = emailAddress;

    public FirstName FirstName { get; set; } = firstName;

    public LastName LastName { get; set; } = lastName;

    public DateOfBirth BirthOn { get; set; } = birthOn;

    private readonly List<DomainEvent> _domainEvents = [];

    public static CustomerBuilder Empty()
    {
        return new CustomerBuilder();
    }

    public static Customer From(
        CustomerId id,
        Username username,
        EmailAddress emailAddress,
        FirstName firstName,
        LastName lastName,
        DateOfBirth birthOn)
    {
        return Empty()
            .UserId(id)
            .Username(username)
            .EmailAddress(emailAddress)
            .FirstName(firstName)
            .LastName(lastName)
            .BirthOn(birthOn)
            .Create();
    }

    public static Customer Register(RegisterCustomer cmd)
    {
        var email = EmailAddress.From(cmd.EmailAddress);
        var username = Username.From(cmd.Username);

        var customer = Empty()
            .UserId(CustomerId.Generate())
            .Username(username)
            .EmailAddress(email)
            .FirstName(FirstName.From(cmd.FirstName))
            .LastName(LastName.From(cmd.LastName))
            .BirthOn(DateOfBirth.From(cmd.BirthOn))
            .Create();

        customer._domainEvents.Add(CustomerRegistered.Issue(username, email));

        return customer;
    }

    public Customer ChangeBy(ChangeEmailAddress cmd)
    {
        EmailAddress = EmailAddress.From(cmd.NewEmailAddress);

        _domainEvents.Add(EmailAddressChanged.Issue());

        return this;
    }

    public Customer ChangeBy(ChangePersonalDetails cmd)
    {
        FirstName = FirstName.From(cmd.FirstName);
        LastName = LastName.From(cmd.LastName);
        BirthOn = DateOfBirth.From(cmd.BirthOn);

        _domainEvents.Add(PersonalDetailsChanged.Issue());

        return this;
    }

    public void ClearEvents()
    {
        _domainEvents.Clear();
    }

    public List<DomainEvent> ListEvents()
    {
        return [.._domainEvents];
    }
}