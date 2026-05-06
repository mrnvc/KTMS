using KTMS.Application.Modules.Genders.Queries.GetGenders;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KTMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GenderController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [AllowAnonymous]

        [HttpGet("GetGenders")]
        public async Task<IActionResult> GetGenders()
        {
            var query = new GetGendersQuery();

            var result = await _mediator.Send(query);

            return Ok(result);
        }
    } 
}
