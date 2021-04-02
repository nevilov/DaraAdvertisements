using System.Threading;
using System.Threading.Tasks;
using DaraAds.API.Dto.Image;
using DaraAds.Application.Services.Image.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DaraAds.API.Controllers.Image
{
    public partial class ImageController
    {
        [AllowAnonymous]
        [HttpGet("{id}")]  
        public async Task<IActionResult> Get(
            [FromRoute] ImageGetRequest request, 
            CancellationToken cancellationToken)
        {
            var response = await _imageService.GetImageUrl(new Get.Request
            {
                Id = request.Id
            }, cancellationToken);
            
            return Ok(response);
        }
    }
}