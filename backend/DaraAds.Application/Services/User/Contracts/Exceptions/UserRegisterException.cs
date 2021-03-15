using DaraAds.Domain.Shared.Exceptions;

namespace DaraAds.Application.Services.User.Contracts.Exceptions
{
    public class UserRegisterException : DomainException
    {
        public UserRegisterException(string message) : base(message)
        {
        }
    }
}