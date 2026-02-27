using KTMS.Application.Modules.Tournaments.Commands.CreateTournaments;
using KTMS.Application.Modules.Tournaments.Commands.DeleteTournaments;
using KTMS.Application.Modules.Tournaments.Commands.UpdateTournaments;
using KTMS.Application.Modules.Tournaments.Queries.GetTournaments;
using KTMS.Application.Modules.Tournaments.Queries.GetTournamentsById;
using KTMS.Application.Modules.Tournaments.Queries.GetTournamentsFiltered;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace KTMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TournamentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TournamentsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("CreateTournaments")]
        public async Task<IActionResult> CreateTournaments([FromBody] CreateTournamentsCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);

        }

        [HttpPut("UpdateTournamnets")]
        public async Task<IActionResult> UpdateTournaments(int id, [FromBody] UpdateTournamentsCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("Id does not match");
            }
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpDelete("DeleteTournaments")]
        public async Task<IActionResult> DeleteTournaments(int Id)
        {
            var command = new DeleteTournamentsCommand { Id = Id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpGet("GetTournaments")]
        public async Task<IActionResult> GetTournaments()
        {
            var query = new GetTournamentsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpGet("GetTournamentsById/{Id}")]
        public async Task<IActionResult> GetTournamentsById(int Id)
        {
            var query = new GetTournamentsByIdQuery(Id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("GetTournamentsFiltered")]
        public async Task<IActionResult> GetTournamentsFiltered([FromQuery] GetTournamentsFilteredQuery query)
        { 
            var result= await _mediator.Send(query);
            return Ok(result);

        }
    }
}

