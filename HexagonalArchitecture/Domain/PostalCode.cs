namespace HexagonalArchitecture.Domain;

public class PostalCode
{
    public string Code { get; }

    private PostalCode(string code)
    {
        Code = code;
    }

    public static PostalCode From(string code)
    {
        return new PostalCode(code);
    }
}