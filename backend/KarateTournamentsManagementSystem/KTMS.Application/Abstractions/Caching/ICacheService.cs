using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTMS.Application.Abstractions.Caching
{
    public interface ICacheService
    {
        Task<T?> GetOrCreateAsync<T>(
            string key,
            Func<CancellationToken, Task<T>> factory,
            TimeSpan ttl,
            CancellationToken cancellationToken = default) where T : class;
    }
}
