using KTMS.Application.Abstractions;
using KTMS.Application.Modules.Clubs.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KTMS.Application.Modules.Clubs.Queries.GetClubsById
{
    public class GetClubsByIdHandler : IRequestHandler<GetClubsByIdQuery, ClubDto>
    {
        private readonly IAppDbContext _dbContext;

        public GetClubsByIdHandler(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ClubDto> Handle(GetClubsByIdQuery request, CancellationToken cancellationToken)
        {
            var club = await _dbContext.Clubs
                                    .Include(c => c.City)
                                    .Include(c => c.Country)
                                    .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (club == null)
            {
                throw new Exception("Club not found");
            }

            return new ClubDto
            {
                City = club.City.Name,
                Country = club.Country.Name,
                Name = club.Name,
                Address = club.Address,
                Email = club.Email,
                PhoneNumber = club.PhoneNumber,
                Status = club.Status
            };
        }
    }
}
