using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using DaraAds.Application.Services.Advertisement.Contracts;
using DaraAds.Application.Services.Advertisement.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DaraAds.API.Controllers.Advertisement
{
    public partial class AdvertisementController : ControllerBase
    {
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(
            AdvertisementUpdateRequest request,
            [FromRoute] int id,
            [FromServices] IAdvertisementService service,
            CancellationToken cancellationToken
        )
        {
            await service.Update(new Update.Request
            {
                Id = id,
                Title = request.Title,
                Description = request.Description,
                Price = request.Price,
                Cover = request.Cover
            }, cancellationToken);
            return Ok();
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
        }
    }
}
