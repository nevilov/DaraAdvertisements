using DaraAds.Domain.Shared.Exceptions;

namespace DaraAds.Application.Identity.Contracts.Exceptions
{
    public class HaveNoRightException : NoRightException
    {
        public HaveNoRightException(string message) : base(message)
        {
        }
    }
}