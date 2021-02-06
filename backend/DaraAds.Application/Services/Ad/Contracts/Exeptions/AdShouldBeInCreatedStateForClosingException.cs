using DaraAds.Domain.Shared.Exceptions;

namespace DaraAds.Application.Services.Ad.Contracts.Exeptions
{
    public sealed class AdShouldBeInCreatedStateForClosingException : EntityNotValidStateException
    {
        public AdShouldBeInCreatedStateForClosingException(int adId)
            : base(
                $"Объявление с ID [{adId}] должно быть в статусе {Domain.Advertisement.Statuses.Created} для закрытия.")
        {
        }
    }
}