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
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public class User
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Password { get; set; }
        }

        public static List<User> Users = new();

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public sealed class UserLoginRequest
        {
            public string Name { get; set; }

            public string Password { get; set; }
        }

        [HttpPost("login")]
        public IActionResult Login(UserLoginRequest request)
        {
            var user = Users.FirstOrDefault(u => u.Name == request.Name && u.Password == request.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.Name),
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            if (user.Name.Contains("admin"))
            {
                claims.Add(new Claim(ClaimTypes.Role, "admin"));
            }

            var token = new JwtSecurityToken
            (
                claims: claims,
                expires: DateTime.UtcNow.AddDays(60),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"])),
                    SecurityAlgorithms.HmacSha256)
            );

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }

        public sealed class UserRegisterRequest
        {
            [Required(ErrorMessage = "Имя пользователя - обязательно")]
            public string Name { get; set; }

            [MaxLength(30)]
            [MinLength(6)] 
            public string Password { get; set; }
        }

        [HttpPost("register")]
        public IActionResult Register(UserRegisterRequest request)
        {
            var newUser = new User
            {
                Id = Users.Count + 1,
                Name = request.Name,
                Password = request.Password
            };

            Users.Add(newUser);

            return Created($"api/v1/users/{newUser.Id}", newUser);
        }
        
        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public User Get(int id)
        {
            return Users.FirstOrDefault(user => user.Id == id);
        }
        
    }
}
