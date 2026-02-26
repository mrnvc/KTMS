using KTMS.Application.Abstractions;
using KTMS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KTMS.Application.Modules.Auth.Register.Commands
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, int>
    {
        private readonly IAppDbContext _dbContext;

        public RegisterUserHandler(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var existingMail = await _dbContext.Users
                .FirstOrDefaultAsync((System.Linq.Expressions.Expression<Func<KTMSUserEntity, bool>>)(u => u.Email == request.User.Email), cancellationToken);
            var existingUsername = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Username == request.User.Username, cancellationToken);  

            if (existingMail != null)
                throw new InvalidOperationException("Email already registered.");
            if (existingUsername != null)
                throw new InvalidOperationException("Username already taken.");

            //hash the password
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.User.Password);

            var user = new KTMSUserEntity
            {
                RoleId = request.User.RoleId,
                CityId = request.User.CityId,
                GenderId = request.User.GenderId,
                Name = request.User.Name,
                Surname = request.User.Surname,
                PhoneNumber = request.User.PhoneNumber,
                DateOfBirth = request.User.DateOfBirth,
                Username = request.User.Username,
                Email = request.User.Email,
                Password = hashedPassword
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
