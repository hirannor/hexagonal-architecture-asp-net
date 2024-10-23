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
    DateOfBirth birthOn,
    Address? address) : IAggregateRoot
{
    public CustomerId CustomerId { get; } = customerId;

    public Username UserName { get; } = username;

    public EmailAddress EmailAddress { get; set; } = emailAddress;

    public FirstName FirstName { get; set; } = firstName;

    public LastName LastName { get; set; } = lastName;

    public DateOfBirth BirthOn { get; set; } = birthOn;

    public Address? Address { get; set; } = address;

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
        DateOfBirth birthOn,
        Address address)
    {
        return Empty()
            .WithUserId(id)
            .WithUsername(username)
            .WithEmailAddress(emailAddress)
            .WithFirstName(firstName)
            .WithLastName(lastName)
            .WithBirthOn(birthOn)
            .WithAddress(address)
            .Create();
    }

    public static Customer Register(RegisterCustomer cmd)
    {
        var email = EmailAddress.From(cmd.EmailAddress);
        var username = Username.From(cmd.Username);

        var customer = Empty()
            .WithUserId(CustomerId.Generate())
            .WithUsername(username)
            .WithEmailAddress(email)
            .WithFirstName(FirstName.From(cmd.FirstName))
            .WithLastName(LastName.From(cmd.LastName))
            .WithBirthOn(DateOfBirth.From(cmd.BirthOn))
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
        if (cmd.FirstName is not null)
            FirstName = FirstName.From(cmd.FirstName);

        if (cmd.LastName is not null)
            LastName = LastName.From(cmd.LastName);

        if (cmd.BirthOn.HasValue)
            BirthOn = DateOfBirth.From(cmd.BirthOn.Value);

        if (ShouldBuildAddress(cmd))
        {
            AddressBuilder addressBuilder = Address.Empty();

            addressBuilder.WithCountry(Country.From(cmd.Country))
                .WithPostalCode(PostalCode.From(cmd.PostalCode))
                .WithCity(City.From(cmd.City))
                .WithStreet(Street.From(cmd.StreetName, cmd.StreetNumber));

            Address = addressBuilder.Build();
        }

        _domainEvents.Add(PersonalDetailsChanged.Issue());
        return this;
    }

    public void ClearEvents()
    {
        _domainEvents.Clear();
    }

    public IReadOnlyList<DomainEvent> ListEvents()
    {
        return [.._domainEvents];
    }
    
    private bool ShouldBuildAddress(ChangePersonalDetails cmd)
    {
        bool shouldBuildAddress = !string.IsNullOrEmpty(cmd.Country) ||
                                  !string.IsNullOrEmpty(cmd.PostalCode) ||
                                  !string.IsNullOrEmpty(cmd.City) ||
                                  !string.IsNullOrEmpty(cmd.StreetName) ||
                                  !string.IsNullOrEmpty(cmd.StreetNumber);
        return shouldBuildAddress;
    }
}