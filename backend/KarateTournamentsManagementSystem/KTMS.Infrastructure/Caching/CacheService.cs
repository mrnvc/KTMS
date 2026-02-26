using KTMS.Application.Abstractions.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTMS.Infrastructure.Caching
{
    public sealed class CacheService : ICacheService
    {
        public async Task<T?> GetOrCreateAsync<T>(
       string key,
       Func<CancellationToken, Task<T>> factory,
       TimeSpan ttl,
       CancellationToken cancellationToken = default) where T : class
        {
            // No caching - just call factory directly - zbog ispita smo isključiti REDIS
            return await factory(cancellationToken);
        }
    }
}
