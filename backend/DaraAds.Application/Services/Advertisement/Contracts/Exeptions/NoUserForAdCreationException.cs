using DaraAds.Domain.Shared.Exceptions;

namespace DaraAds.Application.Services.Advertisement.Contracts.Exeptions
{
    public sealed class NoUserForAdCreationException : NoRightException
    {
        public NoUserForAdCreationException(string message) : base(message)
        {
        }
    }
}