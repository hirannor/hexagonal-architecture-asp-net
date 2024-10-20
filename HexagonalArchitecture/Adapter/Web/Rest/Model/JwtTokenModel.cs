namespace HexagonalArchitecture.Adapter.Web.Rest.Model;

public record JwtTokenModel(string value)
{
    public static JwtTokenModel From(string value)
    {
        return new JwtTokenModel(value);
    }
    
}