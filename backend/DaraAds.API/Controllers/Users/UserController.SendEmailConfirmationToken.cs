using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.API.Controllers.Users
{
    public partial class UserController
    {
        /// <summary>
        /// Отправить токен подтверждения почты
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="email"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("send/confirmEmailToken")]
        public async Task<IActionResult> SendEmailConfirmationToken(string userId, string email, CancellationToken cancellationToken)
        {
            await _identityService.SendEmailConfirmationToken(userId, email, cancellationToken);
            return Ok("Код подтверждения на почту выслан еще раз");
        }
    }
}
