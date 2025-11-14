using KTMS.Application.Abstractions;
using KTMS.Application.Modules.Clubs.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KTMS.Application.Modules.Clubs.Queries.GetClubs
{
    public class GetClubsHandler : IRequestHandler<GetClubsQuery, List<ClubDto>>
    {
        private readonly IAppDbContext _dbContext;

        public GetClubsHandler(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ClubDto>> Handle(GetClubsQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Clubs
                        .Include(c => c.City)
                        .Include(c => c.Country)
                       .Select(c => new ClubDto
                       {
                           Name = c.Name,
                           City = c.City != null ? c.City.Name : "City does not exist!",
                           Country = c.Country != null ? c.Country.Name : "Country does not exist!",
                           Address = c.Address,
                           Email = c.Email,
                           PhoneNumber = c.PhoneNumber,
                           Status = c.Status
                       })
                       .ToListAsync(cancellationToken);
        }
    }
}
