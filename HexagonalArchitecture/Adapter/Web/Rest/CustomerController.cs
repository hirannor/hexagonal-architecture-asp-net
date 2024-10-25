﻿using HexagonalArchitecture.Adapter.Web.Rest.Mapping;
using HexagonalArchitecture.Adapter.Web.Rest.Model;
using HexagonalArchitecture.Application.UseCase;
using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Domain.Command;
using HexagonalArchitecture.Infrastructure;
using HexagonalArchitecture.Infrastructure.Adapter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HexagonalArchitecture.Adapter.Web.Rest;

[Authorize]
[ApiController]
[Adapter(type: AdapterType.Driver)]
[Route("/api/customers/{username}")]
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
    public async Task<ActionResult<ChangePersonalDetailsModel>> ChangePersonalDetails(string username,
        [FromBody] ChangePersonalDetailsModel model)
    {
        IFunction<ChangePersonalDetailsModel, ChangePersonalDetails> mapChangePersonalDetailsModelToCommand =
            new ChangePersonalDetailsModelToCommandMapper(username);

        ChangePersonalDetails cmd = mapChangePersonalDetailsModelToCommand.Apply(model);

        Customer domain = await personalDetails.ChangeBy(cmd);
        CustomerModel changedCustomerModel = _mapCustomerToModel.Apply(domain);

        return Ok(changedCustomerModel);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CustomerModel>> DisplayBy(string username)
    {
        Customer? domain = await customers.DisplayBy(username);

        if (domain is null)
        {
            ProblemDetails details = UsernameNotFound(username);
            return NotFound(details);
        }

        CustomerModel model = _mapCustomerToModel.Apply(domain);

        return Ok(model);
    }

    [HttpPut("password")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ChangePasswordResultModel>> ChangePassword(string username,
        [FromBody] ChangePasswordModel model)
    {
        IFunction<ChangePasswordModel, ChangePassword> mapChangePasswordModelToCommand =
            new ChangePasswordModelToCommandMapper(username);
        ChangePassword cmd = mapChangePasswordModelToCommand.Apply(model);

        await password.ChangeBy(cmd);

        return Ok(ChangePasswordResultModel.From("Password has been changed successfully!"));
    }

    [HttpPut("email-address")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ChangeEmailAddressResultModel>> ChangeEmailAddress(string username,
        [FromBody] ChangeEmailAddressModel model)
    {
        IFunction<ChangeEmailAddressModel, ChangeEmailAddress> mapChangeEmailAddressModelToCommand =
            new ChangeEmailAddressModelToCommandMapper(username);
        ChangeEmailAddress cmd = mapChangeEmailAddressModelToCommand.Apply(model);

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