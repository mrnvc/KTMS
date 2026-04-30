using KTMS.Application.Modules.Contestants.Dtos;

namespace KTMS.Application.Modules.Contestants.Queries.GetContestantsById
{
    public class GetContestantsByIdHandler : IRequestHandler<GetContestantsByIdQuery, ContestantDetailsDto>
    {
        private readonly IAppDbContext _dbContext;

        public GetContestantsByIdHandler(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ContestantDetailsDto> Handle(GetContestantsByIdQuery request, CancellationToken cancellationToken)
        {
            var contestant = await _dbContext.Contestants
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (contestant == null)
            {
                throw new Exception("This contestant does not exist.");
            }

            return new ContestantDetailsDto
            {
                Id = contestant.Id,

                Name = contestant.User.Name,
                Surname = contestant.User.Surname,
                PhoneNumber = contestant.User.PhoneNumber,
                Email = contestant.User.Email,
                DateOfBirth = contestant.User.DateOfBirth,
                Username = contestant.User.Username,

                CityId = contestant.User.CityId,
                GenderId = contestant.User.GenderId,
                BeltId = contestant.BeltId,
                ClubId = contestant.ClubId
            };
        }
    }
}