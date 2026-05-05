using KTMS.Application.Abstractions;
using KTMS.Application.Common.Exceptions;
using KTMS.Domain.Entities;
using KTMS.Domain.Entities.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KTMS.Application.Modules.Judges.Commands.CreateJudge
{
    public class CreateJudgeHandler : IRequestHandler<CreateJudgeCommand, int>
    {
        private readonly IAppDbContext _dbContext;

        public CreateJudgeHandler(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(CreateJudgeCommand request, CancellationToken cancellationToken)
        {
            var dto = request.CreateJudgeDto;

            var email = dto.Email.Trim().ToLower();
            var username = dto.Username.Trim().ToLower();
            var phoneNumber = dto.PhoneNumber.Trim();

            var emailExists = await _dbContext.Users
                .AnyAsync(u => u.Email.ToLower() == email, cancellationToken);

            if (emailExists)
                throw new KTMSConflictException("Email is already registered.");

            var usernameExists = await _dbContext.Users
                .AnyAsync(u => u.Username.ToLower() == username, cancellationToken);

            if (usernameExists)
                throw new KTMSConflictException("Username is already taken.");

            var phoneNumberExists = await _dbContext.Users
                .AnyAsync(u => u.PhoneNumber == phoneNumber, cancellationToken);

            if (phoneNumberExists)
                throw new KTMSConflictException("Phone number is already registered.");

            var user = new KTMSUserEntity
            {
                RoleId = 4,
                CityId = dto.CityId,
                GenderId = dto.GenderId,

                Name = dto.Name.Trim(),
                Surname = dto.Surname.Trim(),
                PhoneNumber = phoneNumber,
                Email = email,
                DateOfBirth = dto.DateOfBirth,
                Username = username,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),

                RegistrationDate = DateTime.UtcNow,
                Status = true,
                IsAdmin = false,
                IsCoach = false,
                IsContestant = false,
                IsEnabled = true
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var judge = new Judge
            {
                UserId = user.Id,
                License = dto.License.Trim(),
                Rank = string.IsNullOrWhiteSpace(dto.Rank) ? null : dto.Rank.Trim()
            };

            _dbContext.Judges.Add(judge);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return judge.Id;
        }
    }
}