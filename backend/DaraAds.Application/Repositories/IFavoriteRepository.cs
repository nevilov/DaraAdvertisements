using DaraAds.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DaraAds.Application.Helpers;

namespace DaraAds.Application.Repositories
{
    public interface IFavoriteRepository : IRepository<Favorite, int>
    {
        public Task<PagedList<Favorite>> FindFavorites(string userId, int offset, int limit ,string sortBy, string sortDirection, CancellationToken cancellationToken);
    }
}
