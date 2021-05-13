using System.Linq;

namespace DaraAds.Application.Helpers
{
    public interface ISortHelper<TEntity>
    {
        IQueryable<TEntity> ApplySort(IQueryable<TEntity> entity, string sortBy, string sortDirection);
        
    }
}