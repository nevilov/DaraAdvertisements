using System.Threading;
using System.Threading.Tasks;
using DaraAds.API.Dto.Advertisement;
using DaraAds.Application.Services.Advertisement.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DaraAds.API.Controllers.Advertisement
{
    public partial class AdvertisementController
    {
        [HttpPatch("{id}/images")]
        public async Task<IActionResult> AddImage(
            int id,
            IFormFile image, 
            CancellationToken cancellationToken)
        {

            await _service.AddImage(new AddImage.Request
            {
                Id = id,
                Image = image
            }, cancellationToken);
            
            return NoContent();
        }
    }
}