using KTMS.Application.Abstractions;
using KTMS.Domain.Entities;
using MediatR;

namespace KTMS.Application.Modules.Judges.Commands.CreateJudge
{
    public class CreateJudgeHandler : IRequestHandler<CreateJudgeCommand, int>
    {
        private readonly IAppDbContext _dbContext;

        public CreateJudgeHandler(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(CreateJudgeCommand request, CancellationToken cancellationToken)
        {
            var dto = request.CreateJudgeDto;

            var judge = new Judge
            {
                UserId = dto.UserId,
                License = dto.License,
                Rank = dto.Rank
            };

            _dbContext.Judges.Add(judge);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return judge.Id;
        }
    }
}
