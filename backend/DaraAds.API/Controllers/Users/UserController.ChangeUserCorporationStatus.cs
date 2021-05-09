using DaraAds.API.Dto.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.API.Controllers.Users
{
    public partial class UserController
    {
        [HttpPatch("changeUserStatus")]
        [Authorize(Roles = "Moderator")]
        public async Task<IActionResult> ChangeUserCorporationStatus(ChangeUserCorporationStatus request, CancellationToken cancellationToken)
        {
            await _userService.ChangeUserCorporationStatus(request.UserId, request.IsCorporation, cancellationToken);
            return Ok();
        }
    }
}
