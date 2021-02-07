using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using DaraAds.API.Controllers.Users;
using DaraAds.Domain;
using DaraAds.Domain.Dto.Advertisement;
using Microsoft.AspNetCore.Http;
using DaraAds.Application.Services.Ad.Interfaces;
using System.Threading;
using System.ComponentModel.DataAnnotations;
using DaraAds.Application.Services.Ad.Contracts;

namespace DaraAds.API.Controllers.Ads
{
    
    public partial class AdsController : ControllerBase
    {
       [HttpPost]
       [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(
            AdvertisementCreateRequest request,
            [FromServices] IAdService service,
            CancellationToken cancellationToken
        )
        {
            var response = await service.Create(new Create.Request
            {
                Title = request.Title,
                Description = request.Description,
                Price = request.Price,
                Cover = request.Cover
            }, cancellationToken);

            return Created($"api/v1/advertisements/{response.Id}", new { });
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
        }
    }
}
