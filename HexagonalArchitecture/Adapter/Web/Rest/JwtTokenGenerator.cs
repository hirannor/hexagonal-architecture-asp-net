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
        Claim[] claims =
        {
            new(JwtRegisteredClaimNames.Sub, user.UserName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        SymmetricSecurityKey key =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? string.Empty));
        SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}