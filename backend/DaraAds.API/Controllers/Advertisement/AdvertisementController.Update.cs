using DaraAds.Application.Services.Advertisement.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using DaraAds.Application.Services.Advertisement.Contracts;
using System.ComponentModel.DataAnnotations;
using DaraAds.API.Dto.Advertisement;
using Microsoft.AspNetCore.Authorization;

namespace DaraAds.API.Controllers.Advertisement
{
    public partial class AdvertisementController : ControllerBase
    {
        /// <summary>
        /// Обновить объявление. (Пользователь)
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(Roles = "User, Moderator")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
            CancellationToken cancellationToken,
            [FromRoute] int id,
            AdvertisementUpdateRequest request)
        {
            var response = await _service.Update(new Update.Request
            {
                Id = id,
                Title = request.Title,
                Description = request.Description,
                Price = request.Price,
                Cover = request.Cover,
                CategoryId = request.CategoryId,
                Location = request.Location,
                GeoLon = request.GeoLon,
                GeoLat = request.GeoLat
            }, cancellationToken);
            return Ok(response.Id);
        }
    }
}
