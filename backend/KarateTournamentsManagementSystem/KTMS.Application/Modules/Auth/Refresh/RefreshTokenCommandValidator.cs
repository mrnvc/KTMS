using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTMS.Application.Modules.Auth.Refresh
{
    public sealed class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(x => x.RefreshToken)
                .NotEmpty().WithMessage("Refresh token is required.");

            RuleFor(x => x.Fingerprint)
                .MaximumLength(256).WithMessage("Fingerprint can be up to 256 characters long.")
                .When(x => x.Fingerprint is not null);
        }
    }
}
