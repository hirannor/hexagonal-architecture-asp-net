namespace HexagonalArchitecture.Domain;

public record Age()
{
    public int value { get; }

    private Age(int value) : this()
    {
        if (value is < 18 or > 99)
        {
            throw new ArgumentException("Age must be between 18 and 99.");
        }

        this.value = value;
    }

    public static Age From(int value)
    {
        return new Age(value);
    }
}