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
            Email = request.Email,
            NewRole = request.NewRole
            }, cancellationToken);

            return Ok();
        }
    }

    public class ChangeRoleRequest
    {
        public string Email { get; set; }

        public string NewRole { get; set; }
    }
}
