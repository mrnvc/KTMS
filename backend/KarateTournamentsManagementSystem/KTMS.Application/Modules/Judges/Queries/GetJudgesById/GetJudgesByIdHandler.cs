using KTMS.Application.Abstractions;
using KTMS.Application.Modules.Judges.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KTMS.Application.Modules.Judges.Queries.GetJudgesById
{
    public class GetJudgesByIdHandler : IRequestHandler<GetJudgesByIdQuery, JudgeDetailsDto>
    {
        private readonly IAppDbContext _dbContext;

        public GetJudgesByIdHandler(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<JudgeDetailsDto> Handle(GetJudgesByIdQuery request, CancellationToken cancellationToken)
        {
            var judge = await _dbContext.Judges
                .Include(j => j.User)
                .FirstOrDefaultAsync(j => j.Id == request.Id, cancellationToken);

            if (judge == null)
            {
                throw new Exception("Judge not found.");
            }

            return new JudgeDetailsDto
            {
                Id = judge.Id,

                Name = judge.User.Name,
                Surname = judge.User.Surname,
                PhoneNumber = judge.User.PhoneNumber,
                Email = judge.User.Email,
                DateOfBirth = judge.User.DateOfBirth,
                Username = judge.User.Username,
                CityId = judge.User.CityId,
                GenderId = judge.User.GenderId,
                License = judge.License,
                Rank = judge.Rank
            };
        }
    }
}