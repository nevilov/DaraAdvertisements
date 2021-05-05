using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.API.Controllers.Users
{
    public partial class UserController
    {
        [HttpPatch("corporation")]
        [Authorize(Roles = "Moderator")]
        public async Task<IActionResult> ChangeUserCorporationStatus(string userId, bool isCorporation, CancellationToken cancellationToken)
        {
            await _userService.ChangeUserCorporationStatus(userId, isCorporation, cancellationToken);
            return Ok();
        }
    }
}
