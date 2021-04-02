using System.Threading;
using System.Threading.Tasks;
using DaraAds.API.Dto.Image;
using DaraAds.Application.Services.Image.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DaraAds.API.Controllers.Image
{
    public partial class ImageController
    {
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            [FromRoute] ImageDeleteRequest request,
            CancellationToken cancellationToken
        )
        {
            await _imageService.Delete(new Delete.Request
            {
                Id = request.Id
            }, cancellationToken);
            
            return NoContent();
        }
    }
}