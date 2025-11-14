using KTMS.Application.Abstractions;
using KTMS.Application.Common.Paging;
using KTMS.Application.Modules.Clubs.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KTMS.Application.Modules.Clubs.Queries.GetPagedClubs
{
    public class GetPagedClubsHandler : IRequestHandler<GetPagedClubsQuery, PageResult<ClubDto>>
    {
        private readonly IAppDbContext _dbContext;

        public GetPagedClubsHandler(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PageResult<ClubDto>> Handle(GetPagedClubsQuery request, CancellationToken cancellationToken)
        {
            var query = _dbContext.Clubs
                                  .Include(c => c.City)
                                  .Include(c => c.Country)
                                  .Select(c => new ClubDto
                                  {
                                      City = c.City.Name,
                                      Country = c.Country.Name,
                                      Name = c.Name,
                                      Address = c.Address,
                                      Email = c.Email,
                                      PhoneNumber = c.PhoneNumber,
                                      Status = c.Status
                                  });

            return await PageResult<ClubDto>.FromQueryableAsync(query, request.Paging, cancellationToken);
        }
    }
}
