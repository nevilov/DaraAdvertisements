using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using DaraAds.API.Dto.Image;
using DaraAds.Application.Services.Image.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DaraAds.API.Controllers.Image
{
    public partial class ImageController
    {
        [HttpPost]
        [Display(Name="Image")]
        public async Task<IActionResult> Upload(
            ImageUploadRequest request, 
            CancellationToken cancellationToken)
        {
            var response = await _imageService.Upload(new Upload.Request
            {
                Image = request.Image    
            }, cancellationToken);

            return Ok(response);
        }
    }
}