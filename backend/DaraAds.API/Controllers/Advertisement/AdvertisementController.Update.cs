using DaraAds.Application.Services.Advertisement.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using DaraAds.Application.Services.Advertisement.Contracts;
using System.ComponentModel.DataAnnotations;
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
        [Authorize(Roles = "User")]
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
                CategoryId = request.CategoryId
            }, cancellationToken);
            return Ok(response.Id);
        }
    }

    public sealed class AdvertisementUpdateRequest
    {

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(10000)]
        public string Description { get; set; }

        [Required]
        [Range(0, 100_000_000_000)]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(300)]
        public string Cover { get; set; }

        public int CategoryId { get; set; }
    }
}
