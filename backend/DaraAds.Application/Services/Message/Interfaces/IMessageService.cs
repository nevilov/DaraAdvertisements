using DaraAds.Application.Services.Message.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.Application.Services.Message.Interfaces
{
    public interface IMessageService
    {
        Task SendMessage(long chatId, string recipientId, string text, CancellationToken cancellationToken);

        Task<GetMessagesByChat.Response> GetMessagesByChat(GetMessagesByChat.Request request, CancellationToken cancellationToken);
    }
}
