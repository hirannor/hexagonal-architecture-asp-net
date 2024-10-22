namespace HexagonalArchitecture.Domain;

public record CustomerId(string Value)
{
    public static CustomerId From(Guid id)
    {
        return new CustomerId(id.ToString());
    }

    public static CustomerId From(string id)
    {
        return new CustomerId(id);
    }

    public static CustomerId Generate()
    {
        return new CustomerId(Guid.NewGuid().ToString());
    }
}