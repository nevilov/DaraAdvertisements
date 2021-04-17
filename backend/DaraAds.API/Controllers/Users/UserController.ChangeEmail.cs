using System.Threading;
using System.Threading.Tasks;
using DaraAds.API.Dto.Users;
using DaraAds.Application.Identity.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DaraAds.API.Controllers.Users
{
    public partial class UserController
    {
        /// <summary>
        /// Отправить токен для изменения почты
        /// </summary>
        /// <param name="newEmail"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("send/changeEmailToken")]
        [Authorize]
        public async Task<IActionResult> SendEmailChangeToken(string newEmail, CancellationToken cancellationToken)
        {
            await _identityService.SendEmailChangeToken(newEmail, cancellationToken);
            return Ok();
        }
        
        /// <summary>
        /// Подтвердить изменение почты
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("confirmChangeEmail")]
        public async Task<IActionResult> ConfirmChangeEmail([FromQuery]ChangeEmailRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _identityService.ConfirmChangeEmail(new ConfirmChangeEmail.Request
            {
                UserId = request.UserId,
                Token = request.Token,
                NewEmail = request.NewEmail
            }, cancellationToken);

            if (!result.isSuccess)
            {
                return BadRequest("Токен или маил не верные");
            }

            return Ok($"Почта изменена на {request.NewEmail}");
        }
    }
}