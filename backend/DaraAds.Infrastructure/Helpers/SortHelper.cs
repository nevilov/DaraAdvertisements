using System.Linq;
using DaraAds.Application.Helpers;
using Microsoft.EntityFrameworkCore;

namespace DaraAds.Infrastructure.Helpers
{
    public class SortHelper<TEntity> : ISortHelper<TEntity>
    {
        public IQueryable<TEntity> ApplySort(IQueryable<TEntity> entity, string sortOrder)
        {
            var descending = false;
            if (sortOrder.EndsWith("_desc"))
            {
                sortOrder = sortOrder.Substring(0, sortOrder.Length - 5);
                descending = true;
            }
            
            return descending
                ? entity.OrderByDescending(a => EF.Property<object>(a, sortOrder))
                : entity.OrderBy(a => EF.Property<object>(a, sortOrder));
        }
    }
}