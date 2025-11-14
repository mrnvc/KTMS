using KTMS.Application.Modules.Users.Commands.CreateUser;
using KTMS.Application.Modules.Users.Commands.DeleteUser;
using KTMS.Application.Modules.Users.Commands.UpdateUser;
using KTMS.Application.Modules.Users.Queries.GetUsers;
using KTMS.Application.Modules.Users.Queries.GetUsersById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KTMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpGet("GetUsers")]
        public async Task<ActionResult> GetUsers()
        {
            var query = new GetUserQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("GetUserById/{id}")]
        public async Task<ActionResult> GetUserById(int id)
        {
            var query = new GetUsersByIdQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPut("UpdateUser")]
        public async Task<ActionResult> UpdateUser(int id, [FromBody] UpdateUserCommand command)
        {
            command.Id = id;

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpDelete("DeleteUser/{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var command = new DeleteUserCommand { Id = id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
