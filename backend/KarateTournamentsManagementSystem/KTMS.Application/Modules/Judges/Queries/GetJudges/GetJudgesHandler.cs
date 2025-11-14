using KTMS.Application.Abstractions;
using KTMS.Application.Modules.Judges.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KTMS.Application.Modules.Judges.Queries.GetJudges
{
    public class GetJudgesHandler : IRequestHandler<GetJudgesQuery, List<JudgeDto>>
    {
        private readonly IAppDbContext _dbContext;

        public GetJudgesHandler(IAppDbContext dbContext) { 
            _dbContext = dbContext;
        }

        public async Task<List<JudgeDto>> Handle(GetJudgesQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Judges
                        .Include(j => j.User)
                        .Select(j => new JudgeDto
                        {
                            Id = j.Id,
                            UserId = j.UserId,
                            Name = j.User.Name,
                            Surname = j.User.Surname,
                            Email = j.User.Email,
                            PhoneNumber = j.User.PhoneNumber,
                            License = j.License,
                            Rank = j.Rank
                        })
                        .ToListAsync(cancellationToken);
        }
    }
}
