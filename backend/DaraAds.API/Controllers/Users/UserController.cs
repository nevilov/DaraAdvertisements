using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using DaraAds.Application.Services.User.Interfaces;
using System.Threading;
using DaraAds.API.Dto.Users;
using DaraAds.Application.Services.User.Contracts;
using DaraAds.Application.Identity.Interfaces;

namespace DaraAds.API.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IIdentityService _identityService;

        public UserController(IUserService userService, IIdentityService identityService)
        {
            _userService = userService;
            _identityService = identityService;
        }

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
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
