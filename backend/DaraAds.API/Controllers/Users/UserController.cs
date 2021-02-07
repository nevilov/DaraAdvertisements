using DaraAds.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DaraAds.Domain;
using DaraAds.Domain.Dto.Users.Requests;
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
}
