using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTMS.Application.Abstractions.Caching
{
    public interface ICatalogCacheVersionService
    {
        Task<long> GetVersionAsync(CancellationToken cancellationToken = default);
        Task<long> BumpVersionAsync(CancellationToken cancellationToken = default);
    }
}
