using KTMS.Application.Abstractions;
using KTMS.Application.Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KTMS.Application.Modules.Judges.Commands.UpdateJudge
{
    public class UpdateJudgeHandler : IRequestHandler<UpdateJudgeCommand, int>
    {
        private readonly IAppDbContext _dbContext;

        public UpdateJudgeHandler(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(UpdateJudgeCommand request, CancellationToken cancellationToken)
        {
            var dto = request.UpdateJudgeDto;

            var judge = await _dbContext.Judges
                .Include(j => j.User)
                .FirstOrDefaultAsync(j => j.Id == request.Id, cancellationToken);

            if (judge == null)
            {
                throw new Exception("Judge not found.");
            }

            var email = dto.Email.Trim().ToLower();
            var username = dto.Username.Trim().ToLower();
            var phoneNumber = dto.PhoneNumber.Trim();

            var currentEmail = judge.User.Email.Trim().ToLower();
            var currentUsername = judge.User.Username.Trim().ToLower();
            var currentPhoneNumber = judge.User.PhoneNumber.Trim();

            if (email != currentEmail)
            {
                var emailExists = await _dbContext.Users
                    .AnyAsync(u => u.Id != judge.UserId && u.Email.ToLower() == email, cancellationToken);

                if (emailExists)
                    throw new KTMSConflictException("Email is already registered.");
            }

            if (username != currentUsername)
            {
                var usernameExists = await _dbContext.Users
                    .AnyAsync(u => u.Id != judge.UserId && u.Username.ToLower() == username, cancellationToken);

                if (usernameExists)
                    throw new KTMSConflictException("Username is already taken.");
            }

            if (phoneNumber != currentPhoneNumber)
            {
                var phoneNumberExists = await _dbContext.Users
                    .AnyAsync(u => u.Id != judge.UserId && u.PhoneNumber == phoneNumber, cancellationToken);

                if (phoneNumberExists)
                    throw new KTMSConflictException("Phone number is already registered.");
            }

            judge.User.Name = dto.Name.Trim();
            judge.User.Surname = dto.Surname.Trim();
            judge.User.PhoneNumber = phoneNumber;
            judge.User.Email = email;
            judge.User.Username = username;
            judge.User.DateOfBirth = dto.DateOfBirth;
            judge.User.CityId = dto.CityId;
            judge.User.GenderId = dto.GenderId;

            judge.License = dto.License.Trim();
            judge.Rank = string.IsNullOrWhiteSpace(dto.Rank) ? null : dto.Rank.Trim();

            await _dbContext.SaveChangesAsync(cancellationToken);

            return judge.Id;
        }
    }
}