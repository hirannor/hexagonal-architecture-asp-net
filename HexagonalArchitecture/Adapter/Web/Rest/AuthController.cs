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
[Route("/api/[controller]")]
public class AuthController(ICustomerSignIn auth, JwtTokenGenerator jwtToken)
    : ControllerBase
{
    private readonly IFunction<SignInModel, SignInCustomer> _mapSignInModelToCommand = new SignInModelToCommandMapper();

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<JwtTokenModel>> Auth([FromBody] SignInModel model)
    {
        var cmd = _mapSignInModelToCommand.Apply(model);
        var authUser = await auth.SignIn(cmd);

        var tokenValue = jwtToken.Generate(authUser);

        return Ok(JwtTokenModel.From(tokenValue));
    }
}