using KTMS.Application.Abstractions;
using KTMS.Application.Modules.Cities.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KTMS.Application.Modules.Cities.Queries.GetCities
{
    public class GetCitiesHandler : IRequestHandler<GetCitiesQuery, List<CityDto>>
    {
        private readonly IAppDbContext _dbContext;

        public GetCitiesHandler(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<CityDto>> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Cities
                                    .Include(c => c.Country)
                                    .Select(c => new CityDto
                                    {
                                        Id = c.Id,
                                        CityName = c.Name,
                                        Country = c.Country.Name
                                    })
                                    .ToListAsync(cancellationToken);
        }
    }
}
