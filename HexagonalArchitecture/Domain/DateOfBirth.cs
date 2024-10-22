namespace HexagonalArchitecture.Domain;

public record DateOfBirth(DateOnly Value)
{
    public static DateOfBirth From(DateOnly value)
    {
        return new DateOfBirth(value);
    }
}