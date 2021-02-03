using DaraAds.Core.Dto.Requests;
using DaraAds.Core.Entities;
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

namespace DaraAds.API.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly DaraAdsDbContext _userContext;

        public UserController(IConfiguration configuration, DaraAdsDbContext userContext)
        {
            _configuration = configuration;
            _userContext = userContext;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterRequest request)
        {
            if(request == null)
            {
                return BadRequest();
            }

            var newUser = new User
            {
                Id = _userContext.Users.Count() + 1,
                Name = request.Name,
                LastName = request.LastName,
                Email = request.Email,
                Phone = request.Phone,
                PasswordHash = request.Password,
            };

            _userContext.Users.Add(newUser);
            await _userContext.SaveChangesAsync();

            return Created($"api/v1/users/{newUser.Id}", newUser.ToDto());
        }        
    }
}
