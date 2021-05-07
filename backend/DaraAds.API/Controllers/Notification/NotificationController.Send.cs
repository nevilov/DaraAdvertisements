using DaraAds.API.Dto.Notification;
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
        public async Task<IActionResult> SendNotifications(SendNotificationsRequest request, CancellationToken cancellationToken)
        {
            await _notificationService.Send(request.Subject, request.Message, cancellationToken);
            return Ok();
        }
    }
}
