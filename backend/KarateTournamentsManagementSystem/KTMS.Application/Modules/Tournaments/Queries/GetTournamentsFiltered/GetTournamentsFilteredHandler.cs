using KTMS.Application.Abstractions;
using KTMS.Application.Modules.Tournaments.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTMS.Application.Modules.Tournaments.Queries.GetTournamentsFiltered
{
    public class GetTournamentsFilteredHandler: IRequestHandler<GetTournamentsFilteredQuery, List<TournamentsDto>>
    {
        private readonly IAppDbContext _dbContext; 
        public GetTournamentsFilteredHandler(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<TournamentsDto>>Handle(GetTournamentsFilteredQuery request, CancellationToken cancellationToken)
        {
            var query = _dbContext.Tournaments
                .Include(t => t.Location)
                .ThenInclude(l => l.City)
                .ThenInclude(c => c.Country).AsQueryable();
            if(!string.IsNullOrEmpty(request.Title))
                query=query.Where(t=>t.Title.ToLower().Contains(request.Title.ToLower()));

            if (!string.IsNullOrEmpty(request.City))
                query = query.Where(t => t.Location.City.Name.ToLower().Contains(request.City.ToLower()));

            if(!string.IsNullOrEmpty (request.Country))
                query=query.Where(t=>t.Location.Country.Name.ToLower().Contains(request.Country.ToLower()));

            if(!string.IsNullOrEmpty(request.Adress))
                query=query.Where(t=>t.Location.Address.ToLower().Contains(request.Adress.ToLower()));

            if(request.Date.HasValue)
                query=query.Where(t=>t.Date==request.Date.Value);

            if(request.Year.HasValue)
                query=query.Where(t=>t.Date.Year==request.Year.Value);

            if(request.Month.HasValue)
                query=query.Where(t=>t.Date.Month==request.Month.Value);

            if(request.Day.HasValue)
                query=query.Where(t=>t.Date.Day==request.Day.Value);

                var result= await query
                    .Select(t => new TournamentsDto
                    { Location = $"{t.Location.Address},{t.Location.Country.Name}, {t.Location.City.Name}",
                        Title = t.Title,
                        Date = t.Date,
                        StartTime = t.StartTime,
                        EndTime = t.EndTime,
                        Description = t.Description,
                        RegistrationFee = t.RegistrationFee,
                        Status = t.Status

                    }).ToListAsync(cancellationToken);
            if (!result.Any())
                throw new Exception("No tournaments match the provided filter criteria");
            return result;
        }
    }
}
