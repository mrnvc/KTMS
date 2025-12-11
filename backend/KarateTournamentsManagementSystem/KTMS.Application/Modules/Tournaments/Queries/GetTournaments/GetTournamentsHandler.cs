using KTMS.Application.Abstractions;
using KTMS.Application.Modules.Tournaments.Dtos;
using KTMS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace KTMS.Application.Modules.Tournaments.Queries.GetTournaments
{
    public class GetTournamentsHandler : IRequestHandler<GetTournamentsQuery, List<TournamentsDto>>

    {
        private readonly IAppDbContext _dbContext;
        public GetTournamentsHandler(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<TournamentsDto>> Handle(GetTournamentsQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Tournaments
                .Include(t => t.Location)
                .ThenInclude(l => l.City)
                .ThenInclude(c => c.Country)
                .Select(t => new TournamentsDto
                {
                    Location = $"{t.Location.Address},{t.Location.Country.Name}, {t.Location.City.Name}",
                    Title = t.Title,
                    Date = t.Date,
                    StartTime = t.StartTime,
                    EndTime = t.EndTime,
                    Description = t.Description,
                    RegistrationFee = t.RegistrationFee,
                    Status = t.Status

                }).ToListAsync(cancellationToken);
        }

    }

}
