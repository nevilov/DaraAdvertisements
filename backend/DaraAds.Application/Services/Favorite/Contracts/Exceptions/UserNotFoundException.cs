using DaraAds.Domain.Shared.Exceptions;

namespace DaraAds.Application.Services.Favorite.Contracts.Exceptions
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(string message) : base(message)
        {
        }
    }
}
