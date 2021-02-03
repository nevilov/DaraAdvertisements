using DaraAds.Core.Dto.Requests;
using DaraAds.Core.Entities;
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

        public static List<User> Users = new();

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpPost("register")]
        public IActionResult Register(UserRegisterRequest request)
        {
            var newUser = new User
            {
                Id = Users.Count + 1,
                Name = request.Name,
                LastName = request.LastName,
                Email = request.Email,
                Phone = request.Phone,
                PasswordHash = request.Password,
            };

            Users.Add(newUser);

            return Created($"api/v1/users/{newUser.Id}", newUser.ToDto());
        }        
    }
}
