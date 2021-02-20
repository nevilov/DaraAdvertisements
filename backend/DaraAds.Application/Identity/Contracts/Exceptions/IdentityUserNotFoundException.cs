using DaraAds.Domain.Shared.Exceptions;

namespace Advertisement.Application.Identity.Contracts.Exceptions
{
    public class IdentityUserNotFoundException : NotFoundException
    {
        public IdentityUserNotFoundException(string message) : base(message)
        {
        }
    }
}