using DaraAds.Domain.Shared.Exceptions;

namespace DaraAds.Application.Services.Advertisement.Contracts.Exceptions
{
    public sealed class AdNotFoundException : NotFoundException
    {
        public AdNotFoundException(int adId) 
            : base($"Объявление с таким ID [{adId}] не было найдено.")
        {
        }
    }
}