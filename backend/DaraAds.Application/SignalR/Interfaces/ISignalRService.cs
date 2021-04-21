using DaraAds.Application.SignalR.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.Application.SignalR.Interfaces
{
    public interface ISignalRService
    {
        public Task SendMessage(Message message, CancellationToken cancellationToken);
    }
}
