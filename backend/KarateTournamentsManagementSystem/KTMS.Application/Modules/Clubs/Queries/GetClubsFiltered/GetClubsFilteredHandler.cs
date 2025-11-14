using KTMS.Application.Abstractions;
using KTMS.Application.Common.Paging;
using KTMS.Application.Modules.Clubs.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KTMS.Application.Modules.Clubs.Queries.GetClubsFiltered
{
    public class GetClubsFilteredHandler : IRequestHandler<GetClubsFilteredQuery, PageResult<ClubDto>>
    {
        private readonly IAppDbContext _dbContext;

        public GetClubsFilteredHandler(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PageResult<ClubDto>> Handle(GetClubsFilteredQuery request, CancellationToken cancellationToken)
        {

            var query = _dbContext.Clubs
                                  .Include(c => c.Country)
                                  .Include(c => c.City)
                                  .AsQueryable();

            if (!string.IsNullOrEmpty(request.Name)) {
                query = query.Where(c => c.Name.Contains(request.Name));
            }

            if (request.CityId.HasValue) {
                query = query.Where(c => c.CityId == request.CityId.Value);
            }

            if(request.CountryId.HasValue) {
                query = query.Where(c => c.CountryId == request.CountryId.Value);
            }

            if (!string.IsNullOrEmpty(request.Address)) {
                query = query.Where(c => c.Address.Contains(request.Address));
            }

            if (!string.IsNullOrEmpty(request.Email))
            {
                query = query.Where(c => c.Email.Contains(c.Email));
            }

            if (!string.IsNullOrEmpty(request.PhoneNumber))
            {
                query = query.Where(c => c.PhoneNumber.Contains(c.PhoneNumber));
            }

            if (request.Status.HasValue)
            {
                query = query.Where(c => c.Status == request.Status.Value);
            }

            var dtoQuery = query.Select(c => new ClubDto
            {
                Name = c.Name,
                City = c.City.Name,
                Country = c.Country.Name,
                Address = c.Address,
                Email = c.Email,
                Status = c.Status
            });

            return await PageResult<ClubDto>.FromQueryableAsync(dtoQuery, request.Paging, cancellationToken);
        }
    }
}
