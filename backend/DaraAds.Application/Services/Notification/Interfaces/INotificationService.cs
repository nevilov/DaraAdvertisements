using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.Application.Services.Notification.Interfaces
{
    public interface INotificationService
    {
        Task Send(string Text, CancellationToken cancellationToken);
    }
}
