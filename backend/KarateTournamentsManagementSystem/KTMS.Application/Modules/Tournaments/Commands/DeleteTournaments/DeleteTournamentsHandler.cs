using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KTMS.Application.Abstractions;
using Microsoft.EntityFrameworkCore;
using MediatR;


namespace KTMS.Application.Modules.Tournaments.Commands.DeleteTournaments
{
    public class DeleteTournamentsHandler : IRequestHandler<DeleteTournamentsCommand, bool>
    {
        private readonly IAppDbContext _dbContext;
        public DeleteTournamentsHandler(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> Handle(DeleteTournamentsCommand request, CancellationToken cancellationToken)
        {
            var tournament = await _dbContext.Tournaments.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
            if (tournament == null)
            {
                throw new Exception("Tournamnet not found");
            }
            _dbContext.Tournaments.Remove(tournament);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;

        }
    }

}
