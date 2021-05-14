using System.Threading;
using System.Threading.Tasks;
using DaraAds.API.Dto.Advertisement;
using DaraAds.Application.Services.Advertisement.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DaraAds.API.Controllers.Advertisement
{
    public partial class AdvertisementController : ControllerBase
    {
       /// <summary>
       /// Создать объявление. (Пользователь)
       /// </summary>
       /// <param name="request"></param>
       /// <param name="cancellationToken"></param>
       /// <returns></returns>
       [Authorize(Roles = "User")]
       [HttpPost]
       [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(
            AdvertisementCreateRequest request,
            CancellationToken cancellationToken
        )
        {
            var response = await _service.Create(new Create.Request
            {
                Title = request.Title,
                Description = request.Description,
                Price = request.Price,
                Cover = request.Cover,
                CategoryId = request.CategoryId,
                Location = request.Location,
                GeoLat = request.GeoLat,
                GeoLon = request.GeoLon
            }, cancellationToken);

            return Created($"api/advertisements/{response.Id}", new {
                redirectId = response.Id
            });
        }
    }
}
