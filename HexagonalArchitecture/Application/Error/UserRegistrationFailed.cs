namespace HexagonalArchitecture.Application.Error;

public class UserRegistrationFailed : Exception
{
    public string EmailAddress { get; }

    public UserRegistrationFailed(string message)
        : base(message)
    {
    }

    public UserRegistrationFailed(string message, Exception inner)
        : base(message, inner)
    {
    }
}