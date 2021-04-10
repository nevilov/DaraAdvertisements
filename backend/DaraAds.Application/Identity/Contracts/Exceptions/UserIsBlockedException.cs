using DaraAds.Domain.Shared.Exceptions;

namespace DaraAds.Application.Identity.Contracts.Exceptions
{
    public class UserIsBlockedException : BlockedException
    {
        public UserIsBlockedException(string message) : base(message)
        {
        }
    }
}
