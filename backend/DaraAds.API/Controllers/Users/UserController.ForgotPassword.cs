using DaraAds.API.Dto.Users;
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
        [HttpGet("forgotPassword/{email}")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(string email, CancellationToken cancellationToken)
        {
            var result = await _identityService.SendResetPasswordToken(new SendResetPasswordToken.Request 
            { 
                Email = email
            }, cancellationToken);

            return Ok();
        }

        /// <summary>
        /// Восстановить пароль
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("resetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordRequest request, CancellationToken cancellationToken)
        {
            var result = await _identityService.ResetPassword(new ResetUserPassword.Request
            {
                UserId = request.UserId,
                Token = request.Token,
                NewPassword = request.NewPassword
            }, cancellationToken);

            if (!result.isSuccess)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result);
        }
    }
}
