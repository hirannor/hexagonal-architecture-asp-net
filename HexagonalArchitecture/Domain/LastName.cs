namespace HexagonalArchitecture.Domain;

public record LastName
{
    public string Value { get; }

    private LastName(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentNullException(nameof(value));
        }

        Value = value;
    }

    public static LastName From(string lastName)
    {
        return new LastName(lastName);
    }
}