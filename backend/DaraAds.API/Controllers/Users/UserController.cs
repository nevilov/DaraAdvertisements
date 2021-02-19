using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using DaraAds.Application.Services.User.Interfaces;
using System.Threading;
using DaraAds.Application.Services.User.Contracts;

namespace DaraAds.API.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class UserController : ControllerBase
    {
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Register(
            [FromBody] UserRegisterRequest request,
            [FromServices] IUserService service,
            CancellationToken cancellationToken)
        {
            var response = await service.Register(new Register.Request
            {
                Email = request.Email,
                Name = request.Name,
                LastName = request.LastName,
                Password = request.Password
            }, cancellationToken);

            return Created($"api/v1/users/{response.UserId}", new { });
        }        
    }
    
    public sealed class UserRegisterRequest
    {
        [Required(ErrorMessage = "Имя пользователя - обязательно")]
        public string Name { get; set; }

        public string LastName { get; set; }

        [Required(ErrorMessage = "Email пользователя - обязательно")]
        public string Email { get; set; }

        public string Phone { get; set; }

        [MaxLength(30)]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
