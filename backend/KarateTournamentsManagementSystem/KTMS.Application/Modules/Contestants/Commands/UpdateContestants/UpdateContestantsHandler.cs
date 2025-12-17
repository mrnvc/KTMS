using KTMS.Application.Abstractions;
using KTMS.Application.Modules.Contestants.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTMS.Application.Modules.Contestants.Commands.UpdateContestants
{
    public class UpdateContestantsHandler: IRequestHandler<UpdateContestantsCommand, int>
    {
        private readonly IAppDbContext _dbContext;
        public UpdateContestantsHandler(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> Handle (UpdateContestantsCommand request, CancellationToken cancellationToken)
        {
            var dto = request.UpdateContestantsDto;
            var contestant = await _dbContext.Contestants.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
            if (contestant == null)
            {
                throw new Exception("This contestant does not exist");
            }
            contestant.UserId = dto.UserId;
            contestant.BeltId = dto.BeltId;
            contestant.ClubId = dto.ClubId;

            await _dbContext.SaveChangesAsync(cancellationToken);
            return contestant.Id;


        }
    }
}
