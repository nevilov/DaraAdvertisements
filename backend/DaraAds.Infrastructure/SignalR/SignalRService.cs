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
            await _hubContext.Clients.Users(message.SellerId, message.CustomerId)
                .SendAsync("message", new
                {
                    message.SellerId,
                    message.SenderName,
                    message.CustomerId,
                    message.CustomerName,
                    message.Text,
                    message.CreatedDate
                }, cancellationToken);
        }
    }
}
