using DaraAds.Domain.Shared.Exceptions;

namespace DaraAds.Application.Services.Message.Contracts.Exceptions
{
    public class ChatNotFoundExceptions : NotFoundException
    {
        public ChatNotFoundExceptions(string message) : base(message)
        {
        }
    }
}
