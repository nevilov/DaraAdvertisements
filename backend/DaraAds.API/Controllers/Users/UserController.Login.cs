using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DaraAds.Application.Services.User.Interfaces;
using System.Threading;
using DaraAds.Application.Services.User.Contracts;
using System.Net;
using DaraAds.Application.Identity.Interfaces;
using DaraAds.Application.Identity.Contracts;

namespace DaraAds.API.Controllers.Users
{
    public partial class UserController : ControllerBase
    {
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
    
    public sealed class UserLoginRequest {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
