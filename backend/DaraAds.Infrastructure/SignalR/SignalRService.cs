using DaraAds.Application.SignalR.Contracts;
using DaraAds.Application.SignalR.Interfaces;
using DaraAds.Infrastructure.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.Infrastructure.SignalR
{
    public class SignalRService : ISignalRService
    {
        private readonly IHubContext<ChatHub> _hubContext;
        
        public SignalRService(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendMessage(Message message, CancellationToken cancellationToken)
        {
            await _hubContext.Clients.Users(message.SenderId, message.RecipientId)
                .SendAsync("message", new
                {
                    message.SenderId,
                    message.SenderName,
                    message.RecipientId,
                    message.RecipientName,
                    message.Text,
                    message.CreatedDate
                }, cancellationToken);
        }
    }
}
