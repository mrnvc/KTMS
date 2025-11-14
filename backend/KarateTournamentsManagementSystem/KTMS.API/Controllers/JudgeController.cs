using KTMS.Application.Modules.Judges.Commands.CreateJudge;
using KTMS.Application.Modules.Judges.Commands.DeleteJudge;
using KTMS.Application.Modules.Judges.Commands.UpdateJudge;
using KTMS.Application.Modules.Judges.Queries.GetJudges;
using KTMS.Application.Modules.Judges.Queries.GetJudgesById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace KTMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JudgeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JudgeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateJudge")]
        public async Task<IActionResult> CreateJudge([FromBody] CreateJudgeCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpGet("GetJudges")]
        public async Task<IActionResult> GetJudges()
        {
            var query = new GetJudgesQuery();

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("GetJudgesById/{id}")]
        public async Task<IActionResult> GetJudgesById(int id)
        {
            var query = new GetJudgesByIdQuery(id);

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPut("UpdateJudge")]
        public async Task<IActionResult> UpdateJudge(int id, [FromBody] UpdateJudgeCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("ID does not match");
            }

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("DeleteJudge")]
        public async Task<IActionResult> DeleteJudge(int id)
        {
            var command = new DeleteJudgeCommand { Id = id };

            var result = await _mediator.Send(command);

            return Ok(result);
        }

    }
}
