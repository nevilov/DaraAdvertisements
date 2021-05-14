using System.Threading;
using System.Threading.Tasks;
using DaraAds.Application.Services.Advertisement.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DaraAds.API.Controllers.Advertisement
{
    public partial class AdvertisementController
    {
        /// <summary>
        /// Удалить картинку из объявления (Пользователь)
        /// </summary>
        /// <param name="id">Идентификатор объявления</param>
        /// <param name="imageId">Идентификатор картинки</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("{id}/images/{imageId}")]
        [Authorize(Roles = "User, Moderator")]
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