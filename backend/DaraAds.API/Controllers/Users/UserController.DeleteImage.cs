using System.Threading;
using System.Threading.Tasks;
using DaraAds.Application.Services.User.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DaraAds.API.Controllers.Users
{
    public partial class UserController
    {
        [Authorize]
        [HttpDelete("images/{imageId}")]
        public async Task<IActionResult> DeleteImage(
            string imageId, 
            CancellationToken cancellationToken)
        {
            await _userService.DeleteImage(new DeleteImage.Request
            {
                ImageId = imageId
            }, cancellationToken);
            
            return NoContent();
        }
    }
}