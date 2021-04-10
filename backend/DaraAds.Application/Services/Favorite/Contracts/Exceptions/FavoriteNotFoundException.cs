using DaraAds.Domain.Shared.Exceptions;

namespace DaraAds.Application.Services.Favorite.Contracts.Exceptions
{
    public class FavoriteNotFoundException : NotFoundException
    {
        public FavoriteNotFoundException(string message) : base(message)
        {
        }
    }
}
