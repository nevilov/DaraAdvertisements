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
        /// <param name="email"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("send/changeEmailToken")]
        [Authorize]
        public async Task<IActionResult> SendEmailChangeToken(string email, CancellationToken cancellationToken)
        {
            await _identityService.SendEmailChangeToken(email, cancellationToken);
            return Ok();
        }

        [HttpPost("changeEmail")]
        public async Task<IActionResult> ChangeEmail([FromQuery]ChangeEmailRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _identityService.ChangeEmail(new ChangeEmail.Request
            {
                UserId = request.UserId,
                Token = request.Token,
                NewEmail = request.NewEmail
            }, cancellationToken);
            return Ok(request);
        }
    }
}