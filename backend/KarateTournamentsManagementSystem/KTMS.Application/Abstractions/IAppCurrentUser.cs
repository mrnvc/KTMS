using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTMS.Application.Abstractions
{
    public interface IAppCurrentUser
    {
        /// <summary>
        /// User identifier (UserId).
        /// </summary>
        int? UserId { get; }

        /// <summary>
        /// User Email. (optional)
        /// </summary>
        string? Email { get; }

        /// <summary>
        /// Indicates whether the user is logged in.
        /// </summary>
        bool IsAuthenticated { get; }

        /// <summary>
        /// Indicates whether the user is an administrator.
        /// </summary>
        bool IsAdmin { get; }

        /// <summary>
        /// Indicates whether the user is a manager.
        /// </summary>
        bool IsCoach { get; }

        /// <summary>
        /// Indicates whether the user is a regular employee.
        /// </summary>
        bool IsContestant { get; }
    }
}
