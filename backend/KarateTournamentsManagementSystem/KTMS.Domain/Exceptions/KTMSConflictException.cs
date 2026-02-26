using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTMS.Domain.Exceptions
{
    public sealed class KTMSConflictException : Exception
    {
        public KTMSConflictException(string message) : base(message)
        {
        }
    }
}
