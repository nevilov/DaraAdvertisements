using DaraAds.API.Dto.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.API.Controllers.Users
{
    public partial class UserController
    {
        /// <summary>
        /// Подписаться/Отписаться на уведомления
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPatch("notifications")]
        [Authorize]
        public async Task<IActionResult> Notifications(SubscribeToNotificationRequest request, CancellationToken cancellationToken)
        {
            await _userService.Notifications(request.IsSubscribe, cancellationToken);
            return Ok();
        }
    }
}
