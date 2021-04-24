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
        [HttpPost("block/{userId}")]
        [Authorize(Roles = "Moderator, Admin")]
        public async Task<IActionResult> BlockUser(string userId, DateTime untillDate, CancellationToken cancellationToken)
        {
            var result = await _identityService.BlockUser(userId, untillDate, cancellationToken);
            return Ok();
        }
    }
}
