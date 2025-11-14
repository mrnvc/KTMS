using KTMS.Application.Modules.Clubs.Commands.CreateClub;
using KTMS.Application.Modules.Clubs.Commands.DeleteClub;
using KTMS.Application.Modules.Clubs.Commands.UpdateClub;
using KTMS.Application.Modules.Clubs.Dtos;
using KTMS.Application.Modules.Clubs.Queries;
using KTMS.Application.Modules.Clubs.Queries.GetClubsById;
using KTMS.Application.Modules.Clubs.Queries.GetClubsFiltered;
using KTMS.Application.Modules.Clubs.Queries.GetPagedClubs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Formats.Tar;

namespace KTMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClubController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClubController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateClub")]
        public async Task<IActionResult> CreateClub([FromBody] CreateClubCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("GetClubs")]
        public async Task<IActionResult> GetClubs()
        {
            var query = new GetClubsQuery();
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("GetClubsById/{id}")]
        public async Task<IActionResult> GetClubsById(int id)
        {
            var query = new GetClubsByIdQuery(id);

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPut("UpdateClub")]
        public async Task<IActionResult> UpdateClub(int id, [FromBody] UpdateClubCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("ID does not match");
            }

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("DeleteClub")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            var command = new DeleteClubCommand { Id = id };

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("GetClubsFiltered")]
        public async Task<ActionResult<List<ClubDto>>> GetClubsFiltered([FromQuery] GetClubsFilteredQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("Paged")]
        public async Task<ActionResult<List<ClubDto>>> GetPagedClubs([FromQuery] GetPagedClubsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
