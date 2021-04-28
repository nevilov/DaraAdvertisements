using DaraAds.API.Dto.Users;
using DaraAds.Application.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.API.Controllers.Users
{
    public partial class UserController
    {
        /// <summary>
        /// Заблокировать пользователя (Администратор, Модератор)
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("block")]
        [Authorize(Roles = "Moderator, Admin")]
        public async Task<IActionResult> BlockUser(BlockUserRequest request, CancellationToken cancellationToken)
        {
            await _identityService.BlockUser(request.UserId, request.UntilDate, cancellationToken);
            return Ok();
        }
    }


}
