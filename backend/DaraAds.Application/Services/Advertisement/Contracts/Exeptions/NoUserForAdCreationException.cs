using DaraAds.Domain.Shared.Exceptions;

namespace DaraAds.Application.Services.Ad.Contracts.Exeptions
{
    public sealed class NoUserForAdCreationException : NoRightException
    {
        public NoUserForAdCreationException(string message) : base(message)
        {
        }
    }
}