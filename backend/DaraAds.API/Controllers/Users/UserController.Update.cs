using System.Threading;
using System.Threading.Tasks;
using DaraAds.API.Dto.Users;
using DaraAds.Application.Services.User.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DaraAds.API.Controllers.Users
{
    public partial class UserController
    {
        [HttpPatch("update")]
        [Authorize]
        public async Task<IActionResult> UpdateDomainUser(DomainUserUpdateRequest request, CancellationToken cancellationToken)
        {
            await _userService.Update(new Update.Request
            {
                Id = request.Id,
                Name = request.Name,
                LastName = request.LastName,
                Avatar = request.Avatar,
                Phone = request.Phone,
            }, cancellationToken);

            return Ok("Пользователь обновлен");
        }
    }
    
}