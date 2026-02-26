using KTMS.Application.Abstractions.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTMS.Infrastructure.Caching
{
    public sealed class CatalogCacheVersionService : ICatalogCacheVersionService
    {
        public async Task<long> GetVersionAsync(CancellationToken cancellationToken = default)
        {
            return 1;
        }

        public async Task<long> BumpVersionAsync(CancellationToken cancellationToken = default)
        {
            return 1;
        }
    }
}
