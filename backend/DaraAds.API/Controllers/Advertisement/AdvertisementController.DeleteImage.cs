using System.Threading;
using System.Threading.Tasks;
using DaraAds.Application.Services.Advertisement.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DaraAds.API.Controllers.Advertisement
{
    public partial class AdvertisementController
    {
        [HttpDelete("{id}/images/{imageId}")]
        public async Task<IActionResult> DeleteImage(
            int id, string imageId, 
            CancellationToken cancellationToken)
        {
            await _service.DeleteImage(new DeleteImage.Request
            {
                Id = id,
                ImageId = imageId
            }, cancellationToken);
            
            return NoContent();
        }
    }
}