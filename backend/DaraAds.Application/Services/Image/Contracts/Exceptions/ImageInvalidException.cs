using DaraAds.Domain.Shared.Exceptions;

namespace DaraAds.Application.Services.Image.Contracts.Exceptions
{
    public class ImageInvalidException : DomainException
    {
        public ImageInvalidException(string message) : base(message)
        {
        }
    }
}