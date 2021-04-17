using DaraAds.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.Application.Repositories
{
    public interface IFavoriteRepository : IRepository<Favorite, int>
    {
        public Task<IEnumerable<Favorite>> FindFavorites(string userId, int offset, int limit, CancellationToken cancellationToken);
    }
}
