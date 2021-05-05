using DaraAds.Domain.Shared.Exceptions;

namespace DaraAds.Application.Services.User.Contracts.Exceptions
{
    public sealed class NoRightsException : Domain.Shared.Exceptions.NoRightsException
    {
        public NoRightsException(string message) : base(message)
        {
        }
    }
}