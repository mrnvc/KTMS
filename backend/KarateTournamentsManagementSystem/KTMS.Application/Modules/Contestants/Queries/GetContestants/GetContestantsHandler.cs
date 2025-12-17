using KTMS.Application.Abstractions;
using KTMS.Application.Modules.Contestants.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTMS.Application.Modules.Contestants.Queries.GetContestants
{
   public class GetContestantsHandler : IRequestHandler<GetContestantsQuery, List<ContestantsDto>>
    {
        private readonly IAppDbContext _dbContext;
        public GetContestantsHandler(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<ContestantsDto>>Handle(GetContestantsQuery request,CancellationToken cancellationToken)
        {
            return await _dbContext.Contestants
                .Include(c => c.Belt)
                .Include(c => c.Club)
                .ThenInclude(cl => cl.City)
                .ThenInclude(C => C.Country)
                .Include(c => c.User)
                .ThenInclude(u => u.Role)
                .Select(c => new ContestantsDto
                {
                    Belt=c.Belt.Name,
                    Club=$"{c.Club.Name}, {c.Club.City.Name}, {c.Club.Country.Name}",
                    User=$"{c.User.Name} {c.User.Surname}, {c.User.Role.Title}"

                }).ToListAsync(cancellationToken);
        }
    }

}
