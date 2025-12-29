using KTMS.Application.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KTMS.Application.Modules.Auth.Login.Commands
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, LoginResponseDto>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IJwtService _jwtService;

        public LoginUserHandler(IAppDbContext dbContext, IJwtService jwtService)
        {
            _dbContext = dbContext;
            _jwtService = jwtService;
        }

        public async Task<LoginResponseDto> Handle( LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(
                    u => u.Email == request.User.Email,
                    cancellationToken);

            if (user == null)
                throw new UnauthorizedAccessException("Invalid email or password");

            bool valid = BCrypt.Net.BCrypt.Verify(request.User.Password, user.Password);

            if (!valid)
                throw new UnauthorizedAccessException("Invalid email or password");

            var token = _jwtService.GenerateToken(user);

            return new LoginResponseDto
            {
                Token = token,
                Email = user.Email,
                Name = user.Name
            };
        }
    }
}
