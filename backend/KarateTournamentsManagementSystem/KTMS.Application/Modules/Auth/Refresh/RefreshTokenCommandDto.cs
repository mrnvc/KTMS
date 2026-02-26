using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTMS.Application.Modules.Auth.Refresh
{
    public sealed class RefreshTokenCommandDto
    {
        /// <summary>
        /// New access token that the client should use for authentication.
        /// </summary>
        public string AccessToken { get; init; }

        /// <summary>
        /// New refresh token that replaces the previous one.
        /// </summary>
        public string RefreshToken { get; init; }

        /// <summary>
        /// Expiration date of the access token in UTC format.
        /// </summary>
        public DateTime AccessTokenExpiresAtUtc { get; init; }

        /// <summary>
        /// Expiration date of the refresh token in UTC format.
        /// </summary>
        public DateTime RefreshTokenExpiresAtUtc { get; init; }
    }
}
