using DaraAds.Application.Common;
using DaraAds.Application.Identity.Contracts.Exceptions;
using DaraAds.Application.Identity.Interfaces;
using DaraAds.Application.Services.Notification.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.Application.Services.Notification.Implementations
{
    public class NotificationService : INotificationService
    {

        public async Task Send(string Text, CancellationToken cancellationToken)
        {

        }
    }
}
