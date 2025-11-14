using KTMS.Application.Abstractions;
using KTMS.Application.Modules.Judges.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KTMS.Application.Modules.Judges.Queries.GetJudgesById
{
    public class GetJudgesByIdHandler : IRequestHandler<GetJudgesByIdQuery, JudgeDto>
    {
        private readonly IAppDbContext _dbContext;

        public GetJudgesByIdHandler(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<JudgeDto> Handle(GetJudgesByIdQuery request, CancellationToken cancellationToken)
        {
            var judge = await _dbContext.Judges
                .Include(j => j.User)
                .FirstOrDefaultAsync(j => j.Id == request.Id, cancellationToken);

            if (judge == null)
            {
                throw new Exception("Judge not found");
            }

            return new JudgeDto
            {
                Id = judge.Id,
                UserId = judge.UserId,
                Name = judge.User.Name,
                Surname = judge.User.Surname,
                PhoneNumber = judge.User.PhoneNumber,
                Email = judge.User.Email,
                License = judge.License,
                Rank = judge.Rank ?? string.Empty
            };
        }
    }
}
