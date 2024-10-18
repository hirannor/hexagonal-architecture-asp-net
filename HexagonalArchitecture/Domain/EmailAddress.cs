namespace HexagonalArchitecture.Domain;

public record EmailAddress
{
    public string value { get; }

    private EmailAddress(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentNullException(nameof(value));
        }

        this.value = value;
    }

    public static EmailAddress From(string emailAddress)
    {
        return new EmailAddress(emailAddress);
    }
}