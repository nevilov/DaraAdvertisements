using System.Threading;
using System.Threading.Tasks;
using DaraAds.API.Dto.Image;
using DaraAds.Application.Services.Advertisement.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DaraAds.API.Controllers.Advertisement
{
    public partial class AdvertisementController : ControllerBase
    {
        /// <summary>
        /// Получить обложку по идентификатору объявления
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("getCover/{Id}")]
        public async Task<IActionResult> GetCover(
            [FromRoute] int Id,
            CancellationToken cancellationToken)
        {
            var response = await _service.GetImageByAdvertisement(new GetImageByAdvertisement.Request
            {
                Id = Id
            }, cancellationToken);

            return Ok(response);
        }
    }
}