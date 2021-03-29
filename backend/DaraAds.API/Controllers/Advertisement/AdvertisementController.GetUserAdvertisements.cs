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
        [HttpGet("get/useradvertisements")]
        [Authorize]
        public async Task<IActionResult> GetUserAdvertisements([FromQuery] GetUserAdvertisementsRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.GetUserAdvertisements(new GetUserAdvertisements.Request
            {
                Offset = request.Offset,
                Limit = request.Limit
            }, cancellationToken);

            return Ok(result);
        }

    }
}
