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
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(GetPages.Response), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll(
            [FromQuery] GetAllRequest request,
            [FromServices] IAdvertisementService adService,
            CancellationToken cancellationToken)
        {
            return Ok(await adService.GetPages(new GetPages.Request
            {
                Limit = request.Limit,
                Offset = request.Offset
            }, cancellationToken));
        }

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
