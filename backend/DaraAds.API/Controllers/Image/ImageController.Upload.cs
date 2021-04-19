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
        /// <summary>
        /// Загрузить картинку
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Display(Name="Image")]
        public async Task<IActionResult> Upload(
            [FromForm]  ImageUploadRequest request,
            CancellationToken cancellationToken)
        {
            var response = await _imageService.Upload(new UploadImage.Request
            {
                Image = request.Image    
            }, cancellationToken);

            return Created($"api/images/{response.Id}", new { response.Id });
        }
    }
}