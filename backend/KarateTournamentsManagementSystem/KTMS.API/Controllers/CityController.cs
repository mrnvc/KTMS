using KTMS.Application.Modules.Cities.Queries.GetCities;
using KTMS.Application.Modules.Cities.Queries.GetCitiesById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KTMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet("GetCities")]
        public async Task<IActionResult> GetCities()
        {
            var query = new GetCitiesQuery();

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("GetCitiesById/{id}")]
        public async Task<IActionResult> GetCitiesById(int id)
        {
            var query = new GetCitiesByIdQuery(id);

            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
