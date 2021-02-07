using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DaraAds.Domain.Dto.Users.Requests;
using DaraAds.Application.Services.User.Interfaces;
using System.Threading;
using DaraAds.Application.Services.User.Contracts;
using System.Net;

namespace DaraAds.API.Controllers.Users
{
    public partial class UserController : ControllerBase
    {
        [HttpPost("login")]
        [ProducesResponseType(typeof(Login.Response), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Login(
            [FromBody] UserLoginRequest request,
            [FromServices] IUserService service,
            CancellationToken cancellationToken)
        {
            return Ok(await service.Login(new Login.Request
            {
                Email = request.Email,
                Password = request.Password
            }, cancellationToken)); ;
        }
    }
}
