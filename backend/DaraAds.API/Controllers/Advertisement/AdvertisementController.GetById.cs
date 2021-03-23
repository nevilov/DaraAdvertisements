using System.Net;
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
        /// Получить все объявления
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(GetPages.Response), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll(
            [FromQuery] GetAllRequest request,
            CancellationToken cancellationToken)
        {
            return Ok(await _service.GetPages(new GetPages.Request
            {
                Limit = request.Limit,
                Offset = request.Offset
            }, cancellationToken));
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

        public sealed class GetAllRequest
        {
            /// <summary>
            /// Количество возвращаемых объявлений
            /// </summary>
            public int Limit { get; set; } = 10;

            /// <summary>
            /// Смещение начиная с котрого возвращаются объявления
            /// </summary>
            public int Offset { get; set; } = 0;
        }
    }
}
