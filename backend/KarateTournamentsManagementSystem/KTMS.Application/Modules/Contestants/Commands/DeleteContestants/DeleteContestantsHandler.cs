using KTMS.Application.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTMS.Application.Modules.Contestants.Commands.DeleteContestants
{
    public class DeleteContestantsHandler: IRequestHandler<DeleteContestantsCommand, bool>
    {
        private readonly IAppDbContext _dbContext;
        public DeleteContestantsHandler(IAppDbContext dbContext) 
        { 
            _dbContext = dbContext;
        }
        public async Task<bool>Handle(DeleteContestantsCommand request, CancellationToken cancellationToken) 
        {
            var contestant=await _dbContext.Contestants.FirstOrDefaultAsync(c=>c.Id==request.Id, cancellationToken);
            if (contestant == null) 
            {
                throw new Exception("Contestant not found");
            }
            _dbContext.Contestants.Remove(contestant);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
