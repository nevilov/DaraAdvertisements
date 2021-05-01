using DaraAds.Domain.Shared.Exceptions;

namespace DaraAds.Application.Services.Advertisement.Contracts.Exceptions
{
    public sealed class NoUserForAdCreationException : NoRightsException
    {
        public NoUserForAdCreationException(string message) : base(message)
        {
        }
    }
}