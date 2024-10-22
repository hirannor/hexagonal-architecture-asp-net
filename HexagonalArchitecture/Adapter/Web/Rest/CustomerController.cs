using HexagonalArchitecture.Adapter.Web.Rest.Mapping;
using HexagonalArchitecture.Adapter.Web.Rest.Model;
using HexagonalArchitecture.Application.UseCase;
using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HexagonalArchitecture.Adapter.Web.Rest;

[Authorize]
[Route("/api/customers/{username}")]
[ApiController]
public class CustomerController(
    ICustomerDisplay customers,
    IChangePassword password,
    IChangePersonalDetails personalDetails,
    IChangeEmailAddress emailAddress)
    : ControllerBase
{
    private readonly IFunction<Customer, CustomerModel> _mapCustomerToModel = new CustomerToModelMapper();

    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ChangePersonalDetailsModel>> ChangePersonalDetails(string username, [FromBody] ChangePersonalDetailsModel model)
    {
        var mapChangePersonalDetailsModelToCommand = new ChangePersonalDetailsModelToCommandMapper(username);
        var cmd = mapChangePersonalDetailsModelToCommand.Apply(model);

        var domain = await personalDetails.ChangeBy(cmd);
        var changedCustomerModel = _mapCustomerToModel.Apply(domain);

        return Ok(changedCustomerModel);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CustomerModel>> DisplayBy(string username)
    {
        var domain = await customers.DisplayBy(username);

        if (domain is null)
        {
            var details = UsernameNotFound(username);
            return NotFound(details);
        }

        var model = _mapCustomerToModel.Apply(domain);

        return Ok(model);
    }

    [HttpPut("password")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ChangePasswordResultModel>> ChangePassword(string username, [FromBody] ChangePasswordModel model)
    {
        var mapChangePasswordModelToCommand = new ChangePasswordModelToCommandMapper(username);
        var cmd = mapChangePasswordModelToCommand.Apply(model);

        await password.ChangeBy(cmd);

        return Ok(ChangePasswordResultModel.From("Password has been changed successfully!"));
    }

    [HttpPut("email-address")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ChangeEmailAddressResultModel>> ChangeEmailAddress(string username, [FromBody] ChangeEmailAddressModel model)
    {
        var mapChangeEmailAddressModelToCommand = new ChangeEmailAddressModelToCommandMapper(username);
        var cmd = mapChangeEmailAddressModelToCommand.Apply(model);

        await emailAddress.ChangeBy(cmd);

        return Ok(ChangeEmailAddressResultModel.From("Email address has been changed successfully!"));
    }

    private ProblemDetails UsernameNotFound(string usernameValue)
    {
        return ProblemDetailsFactory.CreateProblemDetails(
            HttpContext,
            404,
            $"Customer not found with username: {usernameValue}"
        );
    }
}