using DaraAds.Domain.Shared.Exceptions;

namespace DaraAds.Application.Services.Advertisement.Contracts.Exeptions
{
    public sealed class NoAdFoundException : NotFoundException
    {
        public NoAdFoundException(int adId) 
            : base($"Объявление с таким ID [{adId}] не было найдено.")
        {
        }
    }
}