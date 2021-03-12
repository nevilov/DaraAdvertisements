using System.Net;
using System.Threading;
using System.Threading.Tasks;
using DaraAds.Application.Common;
using DaraAds.Application.Services.Advertisement.Contracts;
using DaraAds.Application.Services.Advertisement.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DaraAds.API.Controllers.Advertisement
{
    public partial class AdvertisementController
    {
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(GetPages.Response), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll(
            [FromQuery] GetAllRequest request,
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
            public int Limit { get; set; } = AdvertisementConstants.PaginationLimit;
            
            /// <summary>
            /// Смещение, начиная с котрого возвращаются объявления
            /// </summary>
            public int Offset { get; set; } = AdvertisementConstants.PaginationOffset;
            
            /// <summary>
            /// Критерий сортировки, по умолчанию по Id
            /// </summary>
            public string SortOrder { get; set; } = AdvertisementConstants.SortOrderByIdAsc;
            
            /// <summary>
            /// Строка для поиска объявлений по названию или описанию
            /// </summary>
            public string SearchString { get; set; }
            
            /// <summary>
            /// Идентификатор категории для фильтрации объявлений
            /// </summary>
            public int CategoryId { get; set; }
        }
    }
}
