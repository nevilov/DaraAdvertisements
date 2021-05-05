using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.API.Controllers.Notification
{
    public partial class NotificationController
    {
        [HttpPost("send")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SendNotifications(string subject, string text, CancellationToken cancellationToken)
        {
            await _notificationService.Send(subject, text, cancellationToken);
            return Ok();
        }
    }
}
