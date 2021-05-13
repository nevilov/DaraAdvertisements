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
        /// <summary>
        /// Обновить доменного пользователя
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPatch("update/domainuser")]
        [Authorize]
        public async Task<IActionResult> UpdateDomainUser(DomainUserUpdateRequest request, CancellationToken cancellationToken)
        {
            await _userService.Update(new Update.Request
            {
                Name = request.Name,
                LastName = request.LastName,

                Phone = request.Phone,
            }, cancellationToken);
            return NoContent();
        }
    }
    
}