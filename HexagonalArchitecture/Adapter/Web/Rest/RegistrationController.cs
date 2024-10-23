using HexagonalArchitecture.Adapter.Web.Rest.Mapping;
using HexagonalArchitecture.Adapter.Web.Rest.Model;
using HexagonalArchitecture.Application.UseCase;
using HexagonalArchitecture.Domain.Command;
using HexagonalArchitecture.Infrastructure;
using HexagonalArchitecture.Infrastructure.Adapter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HexagonalArchitecture.Adapter.Web.Rest;

[ApiController]
[AllowAnonymous]
[Adapter(type: AdapterType.Driver)]
[Route("api/register")]
public class RegistrationController(ICustomerRegistration registration)
    : ControllerBase
{
    private readonly IFunction<RegisterCustomerModel, RegisterCustomer> _mapRegisterUserModelToCommand =
        new RegisterModelToCommandMapper();

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<RegistrationResultModel>> Register([FromBody] RegisterCustomerModel customerModel)
    {
        RegisterCustomer cmd = _mapRegisterUserModelToCommand.Apply(customerModel);
        await registration.Register(cmd);

        return Ok(RegistrationResultModel.From("Registration successful!"));
    }
}