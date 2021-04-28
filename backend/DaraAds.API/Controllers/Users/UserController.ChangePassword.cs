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
        /// Изменить пароль
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPatch("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request, CancellationToken cancellationToken)
        {
            await _identityService.ChangePassword(new ChangePassword.Request()
            {
                NewPassword = request.NewPassword,
                OldPassword = request.OldPassword
            }, cancellationToken);

            return Ok();
        }
    }
}