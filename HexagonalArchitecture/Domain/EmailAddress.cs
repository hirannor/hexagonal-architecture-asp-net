namespace HexagonalArchitecture.Domain;

public record EmailAddress
{
    public string Value { get; }

    private EmailAddress(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentNullException(nameof(value));
        }

        Value = value;
    }

    public static EmailAddress From(string emailAddress)
    {
        return new EmailAddress(emailAddress);
    }
}