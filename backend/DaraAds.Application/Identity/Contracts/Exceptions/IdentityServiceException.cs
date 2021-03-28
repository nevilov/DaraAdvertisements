using DaraAds.Domain.Shared.Exceptions;

namespace DaraAds.Application.Identity.Contracts.Exceptions
{
    public class IdentityServiceException : DomainException
    {
        public IdentityServiceException(string message) : base(message)
        {
        }
    }
}