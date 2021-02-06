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
using DaraAds.Domain.Dto.Requests;

namespace DaraAds.API.Controllers.Users
{
    public partial class UserController : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginRequest userModel)
        {
            var user = _userContext.Users.FirstOrDefault(u => u.Email == userModel.Email && u.PasswordHash == userModel.Password);

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
    }
}
