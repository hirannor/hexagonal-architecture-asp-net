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
[Route("api/register")]
public class UserRegistrationController(IUserRegistration registration)
    : ControllerBase
{
    private readonly IFunction<RegisterUserModel, RegisterUser> _mapRegisterUserModelToCommand =
       new RegisterUserModelToCommandMapper();
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Register([FromBody] RegisterUserModel model)
    {
        var cmd = _mapRegisterUserModelToCommand.Apply(model);
        await registration.Register(cmd);
        
        return Ok(RegistrationResultModel.From("Registration successful!"));
    }

}
