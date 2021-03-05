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
       [HttpPost]
       [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(
            AdvertisementCreateRequest request,
            [FromServices] IAdvertisementService service,
            CancellationToken cancellationToken
        )
        {
            var response = await service.Create(new Create.Request
            {
                Title = request.Title,
                Description = request.Description,
                Price = request.Price,
                Cover = request.Cover,
                CategoryId = request.CategoryId
            }, cancellationToken);

            return Created($"api/advertisements/auto/{response.Id}", new { });
        }

        public sealed class AdvertisementCreateRequest
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
}
