namespace HexagonalArchitecture.Domain;

public class CustomerBuilder
{
    private CustomerId? _id;
    private Username? _username;
    private EmailAddress? _emailAddress;
    private FirstName? _firstName;
    private LastName? _lastName;
    private DateOfBirth? _birthOn;
    private Address? _address;

    public CustomerBuilder WithUserId(CustomerId customerId)
    {
        _id = customerId;
        return this;
    }

    public CustomerBuilder WithUsername(Username username)
    {
        _username = username;
        return this;
    }

    public CustomerBuilder WithEmailAddress(EmailAddress emailAddress)
    {
        _emailAddress = emailAddress;
        return this;
    }

    public CustomerBuilder WithFirstName(FirstName firstName)
    {
        _firstName = firstName;
        return this;
    }

    public CustomerBuilder WithLastName(LastName lastName)
    {
        _lastName = lastName;
        return this;
    }

    public CustomerBuilder WithBirthOn(DateOfBirth birthOn)
    {
        _birthOn = birthOn;
        return this;
    }

    public CustomerBuilder WithAddress(Address address)
    {
        _address = address;
        return this;
    }

    public Customer Create()
    {
        return new Customer(
            _id,
            _username,
            _emailAddress,
            _firstName,
            _lastName,
            _birthOn,
            _address
        );
    }
}