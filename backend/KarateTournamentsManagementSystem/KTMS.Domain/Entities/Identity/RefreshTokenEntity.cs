using KTMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTMS.Domain.Entities.Identity
{
    public sealed class RefreshTokenEntity : BaseEntity
    {
        public string TokenHash { get; set; } // Store the HASH, not the plain token
        public DateTime ExpiresAtUtc { get; set; }
        public bool IsRevoked { get; set; }
        public int UserId { get; set; }
        public KTMSUserEntity User { get; set; } = default!;
        public string? Fingerprint { get; set; } // (Optional) e.g., UA/IP hash
        public DateTime? RevokedAtUtc { get; set; }
    }
}
