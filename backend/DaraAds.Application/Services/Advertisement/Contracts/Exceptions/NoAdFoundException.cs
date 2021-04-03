using DaraAds.Domain.Shared.Exceptions;

namespace DaraAds.Application.Services.Advertisement.Contracts.Exceptions
{
    public sealed class NoAdFoundException : NotFoundException
    {
        public NoAdFoundException(int adId) 
            : base($"Объявление с таким ID [{adId}] не было найдено.")
        {
        }
    }
}