
namespace HexagonalArchitecture.Application.Error;

public class UserWithEmailAddressAlreadyExist : Exception
{
    public string EmailAddress { get; }

    public UserWithEmailAddressAlreadyExist(string message, string emailAddress)
        : base(message)
    {
        EmailAddress = emailAddress;
    }

    public UserWithEmailAddressAlreadyExist(string message, string emailAddress, Exception inner)
        : base(message, inner)
    {
        EmailAddress = emailAddress;
    }
}