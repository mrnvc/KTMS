using KTMS.Application.Abstractions;
using KTMS.Application.Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KTMS.Application.Modules.Contestants.Commands.UpdateContestants
{
    public class UpdateContestantsHandler : IRequestHandler<UpdateContestantsCommand, int>
    {
        private readonly IAppDbContext _dbContext;

        public UpdateContestantsHandler(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(UpdateContestantsCommand request, CancellationToken cancellationToken)
        {
            var dto = request.UpdateContestantsDto;

            var contestant = await _dbContext.Contestants
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (contestant == null)
            {
                throw new Exception("This contestant does not exist.");
            }

            var email = dto.Email.Trim().ToLower();
            var username = dto.Username.Trim().ToLower();
            var phoneNumber = dto.PhoneNumber.Trim();

            var currentEmail = contestant.User.Email.Trim().ToLower();
            var currentUsername = contestant.User.Username.Trim().ToLower();
            var currentPhoneNumber = contestant.User.PhoneNumber.Trim();

            if (email != currentEmail)
            {
                var emailExists = await _dbContext.Users
                    .AnyAsync(u => u.Id != contestant.UserId && u.Email.ToLower() == email, cancellationToken);

                if (emailExists)
                    throw new KTMSConflictException("Email is already registered.");
            }

            if (username != currentUsername)
            {
                var usernameExists = await _dbContext.Users
                    .AnyAsync(u => u.Id != contestant.UserId && u.Username.ToLower() == username, cancellationToken);

                if (usernameExists)
                    throw new KTMSConflictException("Username is already taken.");
            }

            if (phoneNumber != currentPhoneNumber)
            {
                var phoneNumberExists = await _dbContext.Users
                    .AnyAsync(u => u.Id != contestant.UserId && u.PhoneNumber == phoneNumber, cancellationToken);

                if (phoneNumberExists)
                    throw new KTMSConflictException("Phone number is already registered.");
            }

            contestant.User.Name = dto.Name.Trim();
            contestant.User.Surname = dto.Surname.Trim();
            contestant.User.PhoneNumber = phoneNumber;
            contestant.User.Email = email;
            contestant.User.Username = username;
            contestant.User.DateOfBirth = dto.DateOfBirth;
            contestant.User.CityId = dto.CityId;
            contestant.User.GenderId = dto.GenderId;

            contestant.BeltId = dto.BeltId;
            contestant.ClubId = dto.ClubId;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return contestant.Id;
        }
    }
}