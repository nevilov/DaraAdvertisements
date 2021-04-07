using DaraAds.Domain.Shared.Exceptions;

namespace DaraAds.Application.Services.Favorite.Contracts.Exceptions
{
    public class AdvertisementNotFoundException : NotFoundException
    {
        public AdvertisementNotFoundException(string message) : base(message)
        {
        }
    }
}
