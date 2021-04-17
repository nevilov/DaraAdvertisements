using DaraAds.Domain.Shared.Exceptions;

namespace DaraAds.Application.Identity.Contracts.Exceptions
{
    public class DuplicateException: ConflictException
    {
        public DuplicateException(string message) : base(message)
        {
        }
    }
}