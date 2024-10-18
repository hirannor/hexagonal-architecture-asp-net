using HexagonalArchitecture.Adapter.Web.Rest.Mapping;
using HexagonalArchitecture.Adapter.Web.Rest.Model;
using HexagonalArchitecture.Application.UseCase;
using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Domain.Command;
using HexagonalArchitecture.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace HexagonalArchitecture.Adapter.Web.Rest;

[Route("api/users")]
[ApiController]
public class UserManagementController(
    IUserDetailsModification userDetailsModification,
    IUserCreation userCreation,
    IUserDisplay userDisplay,
    IUserDeletion userDeletion)
    : ControllerBase
{
    private readonly IFunction<User, UserModel> _mapUserToModel = UserMappingFactory.CreateUserToModelMapper();

    private readonly IFunction<CreateUserModel, CreateUser> _mapCreateUserModelToDomain =
        UserMappingFactory.CreateUserModelToDomainMapper();

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserModel>> ChangeUserDetails(string id, [FromBody] ChangeUserDetailsModel model)
    {
        var cmd = UserMappingFactory.CreateChangeUserDetailsModelToDomainMapper(id)
            .Apply(model);

        var domain = await userDetailsModification.ChangeBy(cmd);

        var response = _mapUserToModel.Apply(domain);

        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserModel>> Create([FromBody] CreateUserModel model)
    {
        var cmd = _mapCreateUserModelToDomain.Apply(model);
        var domain = await userCreation.CreateBy(cmd);
        var ret = _mapUserToModel.Apply(domain);

        return CreatedAtAction(nameof(DisplayBy), new { Id = ret.UserId }, ret);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<UserModel>>> DisplayAll()
    {
        var users = await userDisplay.DisplayAll();
        var model = new List<UserModel>();

        users.ForEach(user => model.Add(_mapUserToModel.Apply(user)));

        return Ok(model);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Task<UserModel>>> DeleteBy(string id)
    {
        var userId = UserId.From(id);
        var domain = await userDisplay.DisplayBy(userId);

        if (domain is null)
        {
            var details = CreateUserNotFoundProblemDetailsFor(userId);
            return NotFound(details);
        }

        await userDeletion.DeleteBy(userId);

        return NoContent();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserModel>> DisplayBy(string id)
    {
        var userId = UserId.From(id);
        var domain = await userDisplay.DisplayBy(userId);

        if (domain is null)
        {
            var details = CreateUserNotFoundProblemDetailsFor(userId);
            return NotFound(details);
        }

        var model = _mapUserToModel.Apply(domain);

        return Ok(model);
    }

    private ProblemDetails CreateUserNotFoundProblemDetailsFor(UserId userId)
    {
        return ProblemDetailsFactory.CreateProblemDetails(
            HttpContext,
            404,
            $"User not found with id: {userId}"
        );
    }
}