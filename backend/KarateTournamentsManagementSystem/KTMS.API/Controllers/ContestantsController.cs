using KTMS.Application.Modules.Contestants.Commands.CreateContestants;
using KTMS.Application.Modules.Contestants.Commands.DeleteContestants;
using KTMS.Application.Modules.Contestants.Commands.UpdateContestants;
using KTMS.Application.Modules.Contestants.Queries.GetContestants;
using KTMS.Application.Modules.Contestants.Queries.GetContestantsById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace KTMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContestantsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ContestantsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("CreateContestants")]
        public async Task<IActionResult> CreateContestants([FromBody] CreateContestantsCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPut("UpdateContestants")]
        public async Task<IActionResult> UpdateContestants(int id, [FromBody] UpdateContestantsCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("Id does not match");
            }
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("DeleteContestants")]
        
        public async Task<IActionResult> DeleteContestants(int id)
        {
            var command = new DeleteContestantsCommand { Id = id};
            var result=await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("GetContestants")]

        public async Task<IActionResult> GetContestants()
        {
            var query = new GetContestantsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("GetContestantsById/{Id}")]

        public async Task<IActionResult> GetContestantsById(int Id)
        {
            var query=new GetContestantsByIdQuery(Id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
