using KTMS.Application.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KTMS.Application.Modules.Judges.Commands.UpdateJudge
{
    public class UpdateJudgeHandler : IRequestHandler<UpdateJudgeCommand, int>
    {
        private readonly IAppDbContext _dbContext;

        public UpdateJudgeHandler(IAppDbContext dbContext) { 
            _dbContext = dbContext;
        }

        public async Task<int> Handle(UpdateJudgeCommand request, CancellationToken cancellationToken)
        {
            var judge = await _dbContext.Judges.FirstOrDefaultAsync(j => j.Id == request.Id, cancellationToken);
            
            if (judge == null)
            {
                throw new Exception("Judge not found");
            }

            var dto = request.UpdateJudgeDto;

            judge.License = dto.License;
            judge.Rank = dto.Rank;

            await _dbContext.SaveChangesAsync(cancellationToken);
            return judge.Id;
        }
    }
}
