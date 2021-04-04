using System.Threading;
using System.Threading.Tasks;
using DaraAds.Application.Services.User.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DaraAds.API.Controllers.Users
{
    public partial class UserController
    {
        [Authorize]
        [HttpPatch("images")]
        public async Task<IActionResult> AddImage(
            IFormFile image, 
            CancellationToken cancellationToken)
        {

            await _userService.AddImage(new AddImage.Request
            {
                Image = image
            }, cancellationToken);
            
            return NoContent();
        }
    }
}