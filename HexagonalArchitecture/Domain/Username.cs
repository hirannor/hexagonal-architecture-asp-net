namespace HexagonalArchitecture.Domain;

public record Username
{
    public string Value { get; }

    private Username(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentNullException(nameof(value));
        }

        Value = value;
    }

    public static Username From(string lastName)
    {
        return new Username(lastName);
    }
}