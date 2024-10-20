namespace HexagonalArchitecture.Application.Error;

public class UserRegistrationFailed : Exception
{
    public IEnumerable<string> Errors { get; }

    public UserRegistrationFailed(string message)
        : base(message)
    {
        Errors = new List<string> { message };
    }

    public UserRegistrationFailed(IEnumerable<string> errors)
        : base("User registration failed with multiple errors.")
    {
        Errors = errors.ToList();
    }

    public UserRegistrationFailed(IEnumerable<string> errors, Exception inner)
        : base("User registration failed with multiple errors.", inner)
    {
        Errors = errors.ToList();
    }
}