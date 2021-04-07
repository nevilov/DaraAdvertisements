using DaraAds.Application.Identity.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.API.Controllers.Users
{
    public partial class UserController
    {
        /// <summary>
        /// Забыл пароль
        /// </summary>
        /// <param name="email"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("reset/password/{email}")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(string email, CancellationToken cancellationToken)
        {
            var result = await _identityService.SendResetPasswordToken(new SendResetPasswordToken.Request 
            { 
                Email = email
            }, cancellationToken);

            return Ok("Ссылка для подтверждения пароля отправлена на почту");
        }
    }
}
