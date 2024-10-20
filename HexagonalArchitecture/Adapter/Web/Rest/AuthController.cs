using HexagonalArchitecture.Adapter.Web.Rest.Mapping;
using HexagonalArchitecture.Adapter.Web.Rest.Model;
using HexagonalArchitecture.Application.UseCase;
using HexagonalArchitecture.Domain.Command;
using HexagonalArchitecture.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HexagonalArchitecture.Adapter.Web.Rest;

[ApiController]
[AllowAnonymous] 
[Route("api/[controller]")]
public class AuthController( IUserSignIn auth, JwtTokenGenerator jwtToken)
    : ControllerBase
{
    private readonly IFunction<SignInUserModel, SignInUser> _mapSignInUserModelToCommand = new SignInUserModelToCommandMapper();
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Auth([FromBody] SignInUserModel model)
    {
        var cmd = _mapSignInUserModelToCommand.Apply(model);
        var authUser = await auth.SignIn(cmd);
        
        var tokenValue = jwtToken.Generate(authUser);
        
        return Ok(JwtTokenModel.From(tokenValue));
    }
    
}
