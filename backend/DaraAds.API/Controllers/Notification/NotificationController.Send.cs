using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.API.Controllers.Notification
{
    public partial class NotificationController
    {
        [HttpPost("send")]
        public async Task<IActionResult> SendNotifications(string Text, CancellationToken cancellationToken)
        {
            await _notificationService.Send(Text, cancellationToken);
            return Ok();
        }
    }
}
