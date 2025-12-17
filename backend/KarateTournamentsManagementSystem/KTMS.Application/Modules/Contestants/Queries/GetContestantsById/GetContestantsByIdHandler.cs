using KTMS.Application.Abstractions;
using KTMS.Application.Modules.Contestants.Dtos;
using KTMS.Application.Modules.Tournaments.Queries.GetTournamentsById;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTMS.Application.Modules.Contestants.Queries.GetContestantsById
{
    public class GetContestantsByIdHandler: IRequestHandler<GetContestantsByIdQuery, ContestantsDto>
    {
        private readonly IAppDbContext _dbContext;
        public GetContestantsByIdHandler(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ContestantsDto> Handle(GetContestantsByIdQuery request, CancellationToken cancellationToken)
        {
            var contestant= await _dbContext.Contestants
                .Include(c => c.Belt)
                .Include(c => c.Club)
                .ThenInclude(cl => cl.City)
                .ThenInclude(C => C.Country)
                .Include(c => c.User)
                .ThenInclude(u => u.Role)
                .FirstOrDefaultAsync(c=> c.Id == request.Id, cancellationToken);
            if(contestant== null)
            {
                throw new Exception("This contestant does not exist");
            }
            return new ContestantsDto
            {
                Belt = contestant.Belt !=null? contestant.Belt.Name : "required data",
                Club = contestant.Club!=null && contestant.Club.City!=null && contestant.Club.Country!=null?
                $"{contestant.Club.Name}, {contestant.Club.City.Name}, {contestant.Club.Country.Name}":"data required",
                User = contestant.User!=null &&contestant.User.Role!=null?
                $"{contestant.User.Name} {contestant.User.Surname}, {contestant.User.Role.Title}" : "required data"
            };
        }
    }
}
