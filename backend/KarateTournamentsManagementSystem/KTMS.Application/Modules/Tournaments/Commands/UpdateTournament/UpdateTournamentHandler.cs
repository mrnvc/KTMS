using KTMS.Application.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTMS.Application.Modules.Tournaments.Commands.UpdateTournament
{
    public class UpdateTournamentHandler : IRequestHandler<UpdateTournamentCommand, int>
    {
        private readonly IAppDbContext _dbContext;
        public UpdateTournamentHandler(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> Handle(UpdateTournamentCommand request, CancellationToken cancellationToken)
        {
            var dto = request.UpdateTournamentDto;
            var tournament = await _dbContext.Tournaments.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
            if (tournament == null)
            {
                throw new Exception("This tournament does not exist");

            }
                tournament.LocationId = dto.LocationId.Value;
                tournament.Date = dto.Date.Value;  
                tournament.StartTime = dto.StartTime.Value;
                tournament.EndTime = dto.EndTime.Value;
                tournament.Status = dto.Status;
            
            await _dbContext.SaveChangesAsync(cancellationToken);
            return tournament.Id;


        }
    }
}
