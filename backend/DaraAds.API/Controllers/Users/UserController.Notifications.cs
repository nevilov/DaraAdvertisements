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
        /// <param name="isSubscribe"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPatch("notifications")]
        [Authorize]
        public async Task<IActionResult> Notifications(bool isSubscribe, CancellationToken cancellationToken)
        {
            await _userService.Notifications(isSubscribe, cancellationToken);
            return Ok();
        }
    }
}
