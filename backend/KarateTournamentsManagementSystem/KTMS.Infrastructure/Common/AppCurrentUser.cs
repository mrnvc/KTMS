using KTMS.Application.Abstractions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KTMS.Infrastructure.Common
{
    public sealed class AppCurrentUser(IHttpContextAccessor httpContextAccessor)
    : IAppCurrentUser
    {
        private readonly ClaimsPrincipal? _user = httpContextAccessor.HttpContext?.User;

        public int? UserId =>
            int.TryParse(_user?.FindFirstValue(ClaimTypes.NameIdentifier), out var id)
                ? id
                : null;

        public string? Email =>
            _user?.FindFirstValue(ClaimTypes.Email);

        public bool IsAuthenticated =>
            _user?.Identity?.IsAuthenticated ?? false;

        public bool IsAdmin =>
            _user?.FindFirstValue("is_admin")?.Equals("true", StringComparison.OrdinalIgnoreCase) ?? false;

        public bool IsCoach =>
            _user?.FindFirstValue("is_coach")?.Equals("true", StringComparison.OrdinalIgnoreCase) ?? false;

        public bool IsContestant =>
            _user?.FindFirstValue("is_contestant")?.Equals("true", StringComparison.OrdinalIgnoreCase) ?? false;
    }
}
