using DaraAds.Application.Services.User.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.API.Controllers.Users
{
    public partial class UserController
    {
        /// <summary>
        /// Получить пользователя по Id или по claims
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("get")]
        public async Task<IActionResult> GetUser(string? Id, CancellationToken cancellationToken)
        {
            var result = await _userService.GetUser(new Get.Request 
            { 
                Id = Id
            }, cancellationToken);
            return Ok(result);;
        }
    }
}
