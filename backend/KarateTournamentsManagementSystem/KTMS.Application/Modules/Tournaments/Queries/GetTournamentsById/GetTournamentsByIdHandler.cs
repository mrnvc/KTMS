using KTMS.Application.Abstractions;
using KTMS.Application.Modules.Tournaments.Dtos;
using KTMS.Application.Modules.Tournaments.Queries.GetTournamentsById;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace KTMS.Application.Modules.Tournaments.Queries.GetTournamentsById
{
    public class GetTournamentsByIdHandler : IRequestHandler<GetTournamentsByIdQuery, TournamentsDto>
    {
        private readonly IAppDbContext _dbContext;
        public GetTournamentsByIdHandler(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<TournamentsDto> Handle(GetTournamentsByIdQuery request, CancellationToken cancellationToken)
        {
            var tournament = await _dbContext.Tournaments
                 .Include(t => t.Location)
                 .ThenInclude(l => l.City)
                 .ThenInclude(c => c.Country).FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
            if (tournament == null)
            {
                throw new Exception("Tournament not found");
            }
            return new TournamentsDto
            {
                Location = tournament.Location != null &&
                tournament.Location.City != null
                && tournament.Location.Country != null
                    ? $"{tournament.Location.Address}, {tournament.Location.Country.Name}, {tournament.Location.City.Name}"
                    : "Location data missing",
                Title = tournament.Title,
                Date = tournament.Date,
                StartTime = tournament.StartTime,
                EndTime = tournament.EndTime,
                Description = tournament.Description,
                RegistrationFee = tournament.RegistrationFee,
                Status = tournament.Status
            };
        }
    }

}
