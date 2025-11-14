using KTMS.Application.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KTMS.Application.Modules.Judges.Commands.DeleteJudge
{
    public class DeleteJudgeHandler : IRequestHandler<DeleteJudgeCommand, bool>
    {
        private readonly IAppDbContext _dbContext;

        public DeleteJudgeHandler(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(DeleteJudgeCommand request, CancellationToken cancellationToken)
        {
            var judge = await _dbContext.Judges.FirstOrDefaultAsync(j => j.Id == request.Id, cancellationToken);
            
            if (judge == null)
            {
                throw new Exception("Judge not found");
            }

            _dbContext.Judges.Remove(judge);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
