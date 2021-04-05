using System.ComponentModel.DataAnnotations;
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
        /// <summary>
        /// Добавить аватар
        /// </summary>
        /// <param name="image">Картинка</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPatch("images")]
        public async Task<IActionResult> AddImage(
            [Required] IFormFile image, 
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