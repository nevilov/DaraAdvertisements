using DaraAds.Domain.Shared.Exceptions;

namespace DaraAds.Application.Services.User.Contracts.Extantions
{
    public sealed class NotRightExctention : NoRightException
    {
        public NotRightExctention(string message) : base(message)
        {
        }
    }
}