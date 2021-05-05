using DaraAds.Application.Identity.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using DaraAds.API.Dto.Users;

namespace DaraAds.API.Controllers.Users
{
    public partial class UserController
    {
        /// <summary>
        /// Изменить роль пользователя. (Администратор)
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("changerole")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeRole(ChangeRoleRequest request, CancellationToken cancellationToken)
        {
            await _identityService.ChangeRole(new ChangeRole.Request { 
            UserId = request.UserId,
            NewRole = request.NewRole
            }, cancellationToken);

            return Ok();
        }
    }
}
