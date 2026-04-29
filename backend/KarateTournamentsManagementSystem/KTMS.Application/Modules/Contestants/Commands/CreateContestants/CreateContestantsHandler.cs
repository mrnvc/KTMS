using MediatR;
using KTMS.Application.Abstractions;
using KTMS.Domain.Entities;
using KTMS.Domain.Entities.Identity;

namespace KTMS.Application.Modules.Contestants.Commands.CreateContestants
{
    public class CreateContestantsHandler : IRequestHandler<CreateContestantsCommand, int>
    {
        private readonly IAppDbContext _dbContext;

        public CreateContestantsHandler(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(CreateContestantsCommand request, CancellationToken cancellationToken)
        {
            var dto = request.CreateContestantsDto;

            var email = dto.Email.Trim().ToLower();
            var username = dto.Username.Trim().ToLower();
            var phoneNumber = dto.PhoneNumber.Trim();

            var emailExists = await _dbContext.Users
                .AnyAsync(u => u.Email.ToLower() == email, cancellationToken);

            var usernameExists = await _dbContext.Users
                .AnyAsync(u => u.Username.ToLower() == username, cancellationToken);

            var phoneNumberExists = await _dbContext.Users
                .AnyAsync(u => u.PhoneNumber == phoneNumber, cancellationToken);

            if (emailExists)
                throw new KTMSConflictException("Email is already registered.");

            if (usernameExists)
                throw new KTMSConflictException("Username is already taken.");

            if (phoneNumberExists)
                throw new KTMSConflictException("Phone number is already registered.");

            var user = new KTMSUserEntity
            {
                Name = dto.Name.Trim(),
                Surname = dto.Surname.Trim(),
                PhoneNumber = phoneNumber,
                Email = email,
                DateOfBirth = dto.DateOfBirth,
                Username = username,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),

                RoleId = 5,
                CityId = dto.CityId,
                GenderId = dto.GenderId,

                RegistrationDate = DateTime.Now,
                Status = true,
                IsContestant = true,
                IsAdmin = false,
                IsCoach = false,
                IsEnabled = true
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var contestant = new Contestant
            {
                UserId = user.Id,
                BeltId = dto.BeltId,
                ClubId = dto.ClubId
            };

            _dbContext.Contestants.Add(contestant);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return contestant.Id;
        }
    }
}