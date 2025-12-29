using MediatR;

namespace KTMS.Application.Modules.Auth.Login.Commands
{
    public class LoginUserCommand : IRequest<LoginResponseDto>
    {
        public LoginUserDto User { get; set; }
    }
}
