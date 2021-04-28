using DaraAds.Domain.Shared.Exceptions;

namespace DaraAds.Application.Services.Chat.Contracts.Exceptions
{
    class ChatException : ConflictException
    {
        public ChatException(string message) : base(message)
        {
        }
    }
}
