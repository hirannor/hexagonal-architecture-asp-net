namespace HexagonalArchitecture.Application.Error;

public class AuthenticationFailed : Exception
{
    public IEnumerable<string> Errors { get; }

    public AuthenticationFailed(string message)
        : base(message)
    {
        Errors = new List<string> { message };
    }

    public AuthenticationFailed(IEnumerable<string> errors)
        : base("User authentication failed with multiple errors.")
    {
        Errors = errors.ToList();
    }

    public AuthenticationFailed(IEnumerable<string> errors, Exception inner)
        : base("User authentication failed with multiple errors.", inner)
    {
        Errors = errors.ToList();
    }
}