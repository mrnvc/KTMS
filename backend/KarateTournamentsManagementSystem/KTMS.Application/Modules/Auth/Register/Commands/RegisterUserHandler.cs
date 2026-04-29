using KTMS.Application.Abstractions;
using KTMS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KTMS.Application.Modules.Auth.Register.Commands
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, int>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IPasswordHasher<KTMSUserEntity> _passwordHasher;

        public RegisterUserHandler(
            IAppDbContext dbContext,
            IPasswordHasher<KTMSUserEntity> passwordHasher)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
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
                Password = string.Empty,
                IsEnabled = true,
                IsDeleted = false,
                Status = true
            };

            user.Password = _passwordHasher.HashPassword(user, request.User.Password);


            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
