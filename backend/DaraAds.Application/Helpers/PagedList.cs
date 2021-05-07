using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DaraAds.Application.Helpers
{
    public class PagedList<TEntity> : List<TEntity>
    {
        public int Total { get; set; }
        
        private PagedList(IEnumerable<TEntity> items, int total)
        {
            Total = total;
            AddRange(items);
        }

        public static async Task<PagedList<TEntity>> ToPagedListAsync(IQueryable<TEntity> entity, int limit, int offset, CancellationToken cancellationToken)
        {
            var total = await entity.CountAsync(cancellationToken);
            var items = await entity.Skip(offset).Take(limit).ToListAsync(cancellationToken);

            return new PagedList<TEntity>(items, total);
        }
    }
}