using DaraAds.Domain.Shared.Exceptions;

namespace DaraAds.Application.Services.Advertisement.Contracts.Exceptions
{
    public class ImageNotFoundException : DomainException
    {
        public ImageNotFoundException() : base($"Изображение не найдено.")
        {
        }
    }
}