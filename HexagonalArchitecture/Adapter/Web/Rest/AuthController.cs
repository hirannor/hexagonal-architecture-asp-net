using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HexagonalArchitecture.Adapter.Web.Rest.Mapping;
using HexagonalArchitecture.Adapter.Web.Rest.Mapping.Auth;
using HexagonalArchitecture.Adapter.Web.Rest.Model;
using HexagonalArchitecture.Application.Port;
using HexagonalArchitecture.Application.UseCase;
using HexagonalArchitecture.Domain.Command;
using HexagonalArchitecture.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HexagonalArchitecture.Adapter.Web.Rest;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IUserRegistration registration, IUserSignIn auth, JwtTokenGenerator jwtToken)
    : ControllerBase
{
    private readonly IFunction<RegisterUserModel, RegisterUser> _mapRegisterUserModelToCommand =
        AuthMappingFactory.CreateRegisterUserModelToCommandMapper();
    
    private readonly IFunction<SignInUserModel, SignInUser> _mapSignInUserModelToCommand =
        AuthMappingFactory.CreateSignInUserModelToCommandMapper();
    
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Register([FromBody] RegisterUserModel model)
    {
        var cmd = _mapRegisterUserModelToCommand.Apply(model);

        var result = await registration.Register(cmd);

        if (result.IsSuccess)
        {
            return Ok(new { Message = "Registration was successful" });
        }

        return BadRequest(result.Errors);
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Login([FromBody] SignInUserModel model)
    {
        var cmd = _mapSignInUserModelToCommand.Apply(model);

        var signInAttempt = await auth.SignIn(cmd);

        if (!signInAttempt.IsSuccess) return Unauthorized();
        
        var user = signInAttempt.Value;
        var token = jwtToken.Generate(user);
        
        return Ok(new { Token = token });
    }
}
