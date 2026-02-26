using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTMS.Application.Modules.Auth.Refresh
{
    public sealed class RefreshTokenCommand : IRequest<RefreshTokenCommandDto>
    {
        /// <summary>
        /// Refresh token that the client sends for rotation.
        /// </summary>
        public string RefreshToken { get; init; }

        /// <summary>
        /// (Optional) Client "fingerprint" / device identifier for device-bound tokens.
        /// </summary>
        public string? Fingerprint { get; init; }
    }
}
