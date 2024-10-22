namespace HexagonalArchitecture.Domain;

public class CustomerBuilder
{
    private CustomerId? _id;
    private Username? _username;
    private EmailAddress? _emailAddress;
    private FirstName? _firstName;
    private LastName? _lastName;
    private DateOfBirth? _birthOn;

    public CustomerBuilder UserId(CustomerId customerId)
    {
        _id = customerId;
        return this;
    }

    public CustomerBuilder Username(Username username)
    {
        _username = username;
        return this;
    }

    public CustomerBuilder EmailAddress(EmailAddress emailAddress)
    {
        _emailAddress = emailAddress;
        return this;
    }

    public CustomerBuilder FirstName(FirstName firstName)
    {
        _firstName = firstName;
        return this;
    }

    public CustomerBuilder LastName(LastName lastName)
    {
        _lastName = lastName;
        return this;
    }

    public CustomerBuilder BirthOn(DateOfBirth birthOn)
    {
        _birthOn = birthOn;
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
            _birthOn);
    }
}