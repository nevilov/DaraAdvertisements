using System.Threading;
using System.Threading.Tasks;
using DaraAds.Application.Services.Advertisement.Contracts;
using DaraAds.Application.Services.Advertisement.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DaraAds.API.Controllers.Advertisement
{
    public partial class AdvertisementController : ControllerBase
    {
        /// <summary>
        /// Удалить объявление. (Пользователь)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize(Roles = "User, Moderator")]
        [HttpDelete("{id:int}")]
            public async Task<IActionResult> Delete(
            [FromRoute] int id,
            CancellationToken cancellationToken
            )
        {
            await _service.Delete(new Delete.Request
            {
                Id = id
            }, cancellationToken);
            return NoContent();
        }
    }
}