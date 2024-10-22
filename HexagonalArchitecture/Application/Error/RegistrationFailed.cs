namespace HexagonalArchitecture.Application.Error;

public class RegistrationFailed : Exception
{
    public IEnumerable<string> Errors { get; }

    public RegistrationFailed(string message)
        : base(message)
    {
        Errors = new List<string> { message };
    }

    public RegistrationFailed(IEnumerable<string> errors)
        : base("User registration failed with multiple errors.")
    {
        Errors = errors.ToList();
    }

    public RegistrationFailed(IEnumerable<string> errors, Exception inner)
        : base("User registration failed with multiple errors.", inner)
    {
        Errors = errors.ToList();
    }
}