using DaraAds.Domain.Shared.Exceptions;

namespace DaraAds.Application.Services.S3.Contracts.Exceptions
{
    public class DeletingImageException : DomainException
    {
        public DeletingImageException(string message) : base(message) { }
        
    }   
}