using System.Linq;
using DaraAds.Application.Common;
using DaraAds.Application.Helpers;
using Microsoft.EntityFrameworkCore;

namespace DaraAds.Infrastructure.Helpers
{
    public class SortHelper<TEntity> : ISortHelper<TEntity>
    {
        public IQueryable<TEntity> ApplySort(IQueryable<TEntity> entity, string sortBy, string sortDirection)
        {
            var isAscDirection = sortDirection == SortConstants.SortDirection;

            return isAscDirection
                ? entity.OrderBy(a => EF.Property<object>(a, sortBy))
                : entity.OrderByDescending(a => EF.Property<object>(a, sortBy));
        }
    }
}