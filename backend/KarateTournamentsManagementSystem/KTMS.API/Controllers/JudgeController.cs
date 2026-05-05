using KTMS.Application.Modules.Judges.Commands.CreateJudge;
using KTMS.Application.Modules.Judges.Commands.DeleteJudge;
using KTMS.Application.Modules.Judges.Commands.UpdateJudge;
using KTMS.Application.Modules.Judges.Dtos;
using KTMS.Application.Modules.Judges.Queries.GetJudges;
using KTMS.Application.Modules.Judges.Queries.GetJudgesById;
using KTMS.Application.Modules.Judges.Queries.GetJudgesFiltered;
using KTMS.Application.Modules.Judges.Queries.GetPagedJudges;
using KTMS.Application.Common.Exceptions;

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
            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (KTMSConflictException ex)
            {
                return Conflict(new
                {
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
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

        [HttpPut("UpdateJudge/{id}")]
        public async Task<IActionResult> UpdateJudge(int id, [FromBody] UpdateJudgeCommand command)
        {
            try
            {
                command.Id = id;

                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (KTMSConflictException ex)
            {
                return Conflict(new
                {
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpDelete("DeleteJudge/{id}")]
        public async Task<IActionResult> DeleteJudge(int id)
        {
            var command = new DeleteJudgeCommand { Id = id };

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpGet("GetJudgesFiltered")]
        public async Task<ActionResult<List<JudgeFilterDto>>> GetJudgesFiltered([FromQuery] GetJudgesFilteredQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("Paged")]
        public async Task<ActionResult<List<JudgeDto>>> GetPagedJudges([FromQuery] GetPagedJudgesQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
