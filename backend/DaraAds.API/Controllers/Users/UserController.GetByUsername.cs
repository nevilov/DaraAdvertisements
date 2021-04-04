using DaraAds.Application.Services.User.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.API.Controllers.Users
{
    public partial class UserController
    {
        [HttpGet("get/{Username}")]
        public async Task<IActionResult> GetByUsername([FromRoute] string Username, CancellationToken cancellationToken)
        {
            if(string.IsNullOrEmpty(Username))
            {
                return BadRequest("Пользователь не может быть пустым");
            }
            var result = await _userService.GetByUsername(new GetByUsername.Request 
            {
                Username = Username
            }, cancellationToken);
            return Ok(result);
        } 
    }
}
