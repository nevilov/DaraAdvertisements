using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using DaraAds.Application.Identity.Contracts;
using DaraAds.API.Dto.Users;

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
                Email = request.Email,
                Password = request.Password
            }, cancellationToken);

            return Ok(token);
        }
    }
}
