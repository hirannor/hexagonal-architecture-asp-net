namespace HexagonalArchitecture.Application.Error;

public class UserAuthenticationFailed : Exception
{
    public IEnumerable<string> Errors { get; }

    public UserAuthenticationFailed(string message)
        : base(message)
    {
        Errors = new List<string> { message };
    }

    public UserAuthenticationFailed(IEnumerable<string> errors)
        : base("User authentication failed with multiple errors.")
    {
        Errors = errors.ToList();
    }

    public UserAuthenticationFailed(IEnumerable<string> errors, Exception inner)
        : base("User authentication failed with multiple errors.", inner)
    {
        Errors = errors.ToList();
    }
}