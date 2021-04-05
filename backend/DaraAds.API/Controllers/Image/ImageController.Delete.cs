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
        /// Удалить картинку по идентификатору
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(
            [FromRoute] ImageDeleteRequest request,
            CancellationToken cancellationToken
        )
        {
            await _imageService.Delete(new DeleteImage.Request
            {
                Id = request.Id
            }, cancellationToken);
            
            return NoContent();
        }
    }
}