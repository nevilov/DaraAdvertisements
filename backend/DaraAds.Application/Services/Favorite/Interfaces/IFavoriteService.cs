using DaraAds.Application.Services.Favorite.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.Application.Services.Favorite.Interfaces
{
    public interface IFavoriteService
    {
        public Task<CreateFavorite.Response> CreateFavorite(CreateFavorite.Request request, CancellationToken cancellationToken);
    }
}
