using DaraAds.Domain.Shared.Exceptions;

namespace DaraAds.Application.Services.User.Contracts.Extantions
{
    public class DuplicateException: ConflictException
    {
        public DuplicateException(string message) : base(message)
        {
        }
    }
}