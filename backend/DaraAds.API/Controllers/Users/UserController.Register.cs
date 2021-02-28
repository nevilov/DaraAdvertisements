using DaraAds.API.Dto.Users;
using DaraAds.Application.Services.User.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.API.Controllers.Users
{
    public partial class UserController : ControllerBase
    {
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Register(
            [FromBody] UserRegisterRequest request,
            CancellationToken cancellationToken)
        {
            var registrationResult = await _userService.Register(new Register.Request
            {
                Username = request.Username,
                Name = request.Name,
                LastName = request.LastName,
                Email = request.Email,
                Phone = request.Phone,
                Password = request.Password
            }, cancellationToken);

            return Created($"api/v1/users/{registrationResult.UserId}", new { });
        }
    }
}
