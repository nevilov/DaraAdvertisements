using DaraAds.Domain.Shared.Exceptions;

namespace DaraAds.Application.Services.S3.Contracts.Exceptions
{
    public class SendingImageException : DomainException
    {
        public SendingImageException(string message) : base(message) { }
    }
}