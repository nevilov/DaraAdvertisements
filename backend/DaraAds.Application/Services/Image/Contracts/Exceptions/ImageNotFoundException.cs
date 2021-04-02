using DaraAds.Domain.Shared.Exceptions;

namespace DaraAds.Application.Services.Image.Contracts.Exceptions
{
    public class ImageNotFoundException : DomainException
    {
        public ImageNotFoundException(int id) : base($"Изображение с ID [{id}] не было найдено.")
        {
        }
    }
}