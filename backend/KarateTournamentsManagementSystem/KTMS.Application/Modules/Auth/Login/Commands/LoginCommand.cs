using MediatR;

namespace KTMS.Application.Modules.Auth.Login.Commands
{
    public class LoginCommand : IRequest<LoginCommandDto>
    {
        /// <summary>
        /// User's email.
        /// </summary>
        public string Email { get; init; }

        /// <summary>
        /// User's password.
        /// </summary>
        public string Password { get; init; }
    }
}
