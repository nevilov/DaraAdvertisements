using DaraAds.Domain.Shared.Exceptions;

namespace DaraAds.Application.Services.Chat.Contracts.Exceptions
{
    public class ChatDuplicateException : DuplicateException
    {
        public ChatDuplicateException(string message) : base(message)
        {
        }
    }
}
