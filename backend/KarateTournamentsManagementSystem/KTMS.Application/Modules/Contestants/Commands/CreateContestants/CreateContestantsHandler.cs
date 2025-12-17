using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KTMS.Application.Abstractions;
using KTMS.Domain.Entities;

namespace KTMS.Application.Modules.Contestants.Commands.CreateContestants
{
    public class CreateContestantsHandler : IRequestHandler<CreateContestantsCommand, int>
    {
        private readonly IAppDbContext _dbContext;
        public CreateContestantsHandler(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int>Handle(CreateContestantsCommand request, CancellationToken cancellationToken) 
        {
            var dto = request.CreateContestantsDto;
            var contestant = new Contestant
            {
                UserId = dto.UserId,
                BeltId = dto.BeltId,
                ClubId = dto.ClubId
            };
            _dbContext.Contestants.Add(contestant);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return contestant.Id;
        }
    }
}
