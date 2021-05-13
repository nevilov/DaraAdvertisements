using DaraAds.Application.Services.Favorite.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.Application.Services.Favorite.Interfaces
{
    public interface IFavoriteService
    {
        public Task<CreateFavorite.Response> AddToFavorite(CreateFavorite.Request request, CancellationToken cancellationToken);

        public Task<GetFavorites.Response> GetFavorites(GetFavorites.Request request, CancellationToken cancellationToken);

        public Task RemoveFromFavorite(RemoveFromFavorite.Request request, CancellationToken cancellationToken);
    }
}
