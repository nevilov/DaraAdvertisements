using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using DaraAds.API.Dto.Users;
using DaraAds.Application.Identity.Contracts;

namespace DaraAds.API.Controllers.Users
{
    public partial class UserController : ControllerBase
    {
        /// <summary>
        /// Вход
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginRequest request,
            CancellationToken cancellationToken)
        {
            var token = await _identityService.CreateToken(new CreateToken.Request
            {
                Login = request.Login,
                Password = request.Password
            }, cancellationToken);


            return Ok(token);
        }
    }
}
