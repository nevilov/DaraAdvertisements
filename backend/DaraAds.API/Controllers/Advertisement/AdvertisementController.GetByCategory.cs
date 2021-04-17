using DaraAds.Application.Services.Advertisement.Contracts;
using DaraAds.Application.Services.Advertisement.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using DaraAds.API.Dto.Advertisement;

namespace DaraAds.API.Controllers.Advertisement
{
    public partial class AdvertisementController : ControllerBase
    {
        /// <summary>
        /// Получить объявления по категории
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("category")]
        public async Task<IActionResult> GetByCategory([FromQuery] GetByCategoryRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _service.GetPagedByCategory(new GetPagedByCategory.Request
            {
               CategoryId = request.CategoryId,
               Limit = request.Limit,
               Offset = request.Offset
            }, cancellationToken);

            return Ok(result);
        }
    }
}
