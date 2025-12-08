using KTMS.Application.Abstractions;
using KTMS.Application.Modules.Users.Commands.CreateUser;
using KTMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTMS.Application.Modules.Tournaments.Commands.CreateTournament
{
   public class CreateTournamentHandler: IRequestHandler<CreateTournamentCommand, int>
    {
        private readonly IAppDbContext _dbContext;

        public CreateTournamentHandler(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> Handle(CreateTournamentCommand request, CancellationToken cancellationToken)
        {
            var dto = request.CreateTournamentDto;
            var tournament = new Tournament
            {
                LocationId = dto.LocationId,
                Title = dto.Title,
                Date = dto.Date,
                StartTime= dto.StartTime,
                EndTime= dto.EndTime,
                Description=   dto.Description,
                RegistrationFee= dto.RegistrationFee,
                Status= dto.Status,

            };
            _dbContext.Tournaments.Add(tournament);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return tournament.Id;

        }
    }
}
