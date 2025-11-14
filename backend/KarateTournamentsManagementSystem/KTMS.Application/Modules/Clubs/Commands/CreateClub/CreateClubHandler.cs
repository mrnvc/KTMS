using KTMS.Application.Abstractions;
using KTMS.Domain.Entities;
using MediatR;

namespace KTMS.Application.Modules.Clubs.Commands.CreateClub
{
    public class CreateClubHandler : IRequestHandler<CreateClubCommand, int>
    {
        private readonly IAppDbContext _dbContext;

        public CreateClubHandler(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle (CreateClubCommand request, CancellationToken cancellationToken)
        {
            var dto = request.CreateClubDto;

            var club = new Club
            {
                CityId = dto.CityId,
                CountryId = dto.CountryId,
                Name = dto.Name,
                Address = dto.Address,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Status = true
            };

            _dbContext.Clubs.Add(club);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return club.Id;
        }
    }
}
