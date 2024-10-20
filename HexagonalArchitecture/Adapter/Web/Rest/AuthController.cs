using HexagonalArchitecture.Adapter.Web.Rest.Mapping.Auth;
using HexagonalArchitecture.Adapter.Web.Rest.Model;
using HexagonalArchitecture.Application.UseCase;
using HexagonalArchitecture.Domain.Command;
using HexagonalArchitecture.Infrastructure;
using Microsoft.AspNetCore.Mvc;

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
        await registration.Register(cmd);
        
        return Ok(new { Message = "Registration was successful" });
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Login([FromBody] SignInUserModel model)
    {
        var cmd = _mapSignInUserModelToCommand.Apply(model);
        var authUser = await auth.SignIn(cmd);
        
        var tokenValue = jwtToken.Generate(authUser);
        
        return Ok(JwtTokenModel.From(tokenValue));
    }
    
}
