namespace HexagonalArchitecture.Domain;

public record FirstName
{
    public string Value { get; }

    private FirstName(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentNullException(nameof(value));
        }

        Value = value;
    }

    public static FirstName From(string firstName)
    {
        return new FirstName(firstName);
    }
}