using KTMS.Application.Modules.Tournaments.Commands.CreateTournament;
using KTMS.Application.Modules.Tournaments.Commands.DeleteTournaments;
using KTMS.Application.Modules.Tournaments.Commands.UpdateTournament;
using KTMS.Application.Modules.Tournaments.Queries.GetTournaments;
using KTMS.Application.Modules.Tournaments.Queries.GetTournamentsById;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace KTMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TournamentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TournamentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("CreateTournament")]
        public async Task<IActionResult> CreateTournament([FromBody] CreateTournamentCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);

        }

        [HttpPut("UpdateTournamnet")]
        public async Task<IActionResult> UpdateTournament(int id, [FromBody] UpdateTournamentCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("Id does not match");
            }
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpDelete("DeleteTournament")]
        public async Task <IActionResult> DeleteTournament(int Id)
        {
            var command=new DeleteTournamentCommand {Id = Id};
            var result= await _mediator.Send(command);
            return Ok(result);
        }
        [HttpGet("GetTournaments")]
        public async Task<IActionResult> GetTournaments() 
        {
            var query=new GetTournamentsQuery();
            var result= await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet("GetTournamentById/{Id}")]
        public async Task<IActionResult>GetTournamentById(int Id)
        {
            var query=new GetTournamentsByIdQuery(Id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
