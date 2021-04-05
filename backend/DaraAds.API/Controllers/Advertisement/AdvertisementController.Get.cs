using System.Net;
using System.Threading;
using System.Threading.Tasks;
using DaraAds.Application.Common;
using DaraAds.API.Controllers.Abuse;
using DaraAds.API.Dto.Advertisement;
using DaraAds.Application.Services.Advertisement.Contracts;
using DaraAds.Application.Services.Advertisement.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DaraAds.API.Controllers.Advertisement
{
    public partial class AdvertisementController
    {
        /// <summary>
        /// Получить все объявления
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(GetPages.Response), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll(
            [FromQuery] GetAllAdvertisementRequest request,
            CancellationToken cancellationToken)
        {

            var response = await _service.GetPages(new GetPages.Request
            {
                Limit = request.Limit,
                Offset = request.Offset,
                SortOrder = request.SortOrder,
                SearchString = request.SearchString,
                CategoryId = request.CategoryId
            }, cancellationToken);

            return Ok(response);
        }

        /// <summary>
        /// Получить объявление по Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="service"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Get.Response), (int)HttpStatusCode.OK)]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(
            [FromRoute] int id,
            [FromServices] IAdvertisementService service,
            CancellationToken cancellationToken
        )
        {
            return Ok(await service.Get(new Get.Request
            {
                Id = id
            }, cancellationToken));
        }
    }
}
