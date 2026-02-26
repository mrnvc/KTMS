using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTMS.Domain.Exceptions
{
    public sealed class KTMSNotFoundException : Exception
    {
        public KTMSNotFoundException(string message) : base(message)
        {
        }

        public KTMSNotFoundException(string entityName, object key)
            : base($"{entityName} with key '{key}' was not found.")
        {
        }
    }
}
