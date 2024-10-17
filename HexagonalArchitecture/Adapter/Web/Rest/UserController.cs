using HexagonalArchitecture.Adapter.Web.Rest.Mapping;
using HexagonalArchitecture.Application.UseCase;
using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Domain.Command;
using HexagonalArchitecture.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace HexagonalArchitecture.Adapter.Web.Rest
{
    [Route("api/users")]
    [ApiController]
    public class UserController(
        IUserCreation userCreation,
        IUserDisplay userDisplay,
        IUserDeletion userDeletion)
        : ControllerBase
    {
        private readonly IFunction<User, UserModel> _mapUserToModel = UserMapperFactory.UserToModelMapper();

        private readonly IFunction<CreateUserModel, CreateUser> _mapCreateUserModelToDomain =
            UserMapperFactory.CreateUserModelToDomainMapper();

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserModel>> Create([FromBody] CreateUserModel model)
        {
            var cmd = _mapCreateUserModelToDomain.Apply(model);
            var domain = await userCreation.Create(cmd);
            var ret = _mapUserToModel.Apply(domain);

            return CreatedAtAction(nameof(DisplayById), new { ret.Id }, ret);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<UserModel>>> DisplayAll()
        {
            var users = await userDisplay.DisplayAll();
            var model = new List<UserModel>();

            users.ForEach(user => model.Add(_mapUserToModel.Apply(user)));

            return model;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserModel>> DisplayById(string id)
        {
            var userId = UserId.From(id);
            var domain = await userDisplay.DisplayById(userId);

            if (domain is null)
            {
                var details =
                    ProblemDetailsFactory.CreateProblemDetails(HttpContext, 404, $"User not found with id: {userId}");
                return NotFound(details);
            }

            var model = _mapUserToModel.Apply(domain);

            return Ok(model);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Task<UserModel>>> Delete(string id)
        {
            var userId = UserId.From(id);
            var domain = await userDisplay.DisplayById(userId);

            if (domain is null)
            {
                return NotFound($"User not found with id {userId}");
            }

            await userDeletion.DeleteBy(userId);

            return NoContent();
        }
    }
}