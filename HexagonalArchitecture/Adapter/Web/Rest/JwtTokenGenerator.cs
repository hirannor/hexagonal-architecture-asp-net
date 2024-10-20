using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HexagonalArchitecture.Domain;
using Microsoft.IdentityModel.Tokens;

namespace HexagonalArchitecture.Adapter.Web.Rest;

public class JwtTokenGenerator(IConfiguration configuration)
{
    public string Generate(AuthUser user)
    {
        if (user is null)
        {
            ArgumentNullException.ThrowIfNull("AuthUser cannot be null!");
        }

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? string.Empty));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"], 
            audience: configuration["Jwt:Audience"], 
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}