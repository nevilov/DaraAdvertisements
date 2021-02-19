using DaraAds.Domain.Shared.Exceptions;

namespace DaraAds.Application.Services.User.Contracts.Extantions
{
    public sealed class NoRightsException : NoRightException
    {
        public NoRightsException(string message) : base(message)
        {
        }
    }
}