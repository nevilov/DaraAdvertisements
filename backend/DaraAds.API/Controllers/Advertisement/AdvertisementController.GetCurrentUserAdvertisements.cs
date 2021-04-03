using DaraAds.API.Dto.Advertisement;
using DaraAds.Application.Services.Advertisement.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.API.Controllers.Advertisement
{
    public partial class AdvertisementController
    {
        /// <summary>
        /// Получить все объявления пользователя
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("get/user")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUserAdvertisements([FromQuery] GetCurrentUserAdvertisementsRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.GetCurrentUserAdvertisements(new GetCurrentUserAdvertisements.Request
            {
                Offset = request.Offset,
                Limit = request.Limit
            }, cancellationToken);

            return Ok(result);
        }

    }
}
