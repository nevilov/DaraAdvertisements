using DaraAds.Application.Services.Favorite.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.API.Controllers.Favorite
{
    public partial class FavoriteController
    {
        /// <summary>
        /// Удалить объявление из избранного
        /// </summary>
        /// <param name="advertisementId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("remove/{advertisementId}")]
        public async Task<IActionResult> RemoveFromFavorite([FromRoute] int advertisementId, CancellationToken cancellationToken)
        {
            await _service.RemoveFromFavorite(new RemoveFromFavorite.Request
            {
                AdvertisementId = advertisementId
            }, cancellationToken);
            
            return NoContent();
        }
    }
}
