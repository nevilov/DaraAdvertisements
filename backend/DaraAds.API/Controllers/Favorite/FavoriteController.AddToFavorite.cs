using DaraAds.Application.Services.Favorite.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.API.Controllers.Favorite
{
    public partial class FavoriteController : ControllerBase
    {
        /// <summary>
        /// Добавить объявление в избранное
        /// </summary>
        /// <param name="advertisementId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("add")]
        public async Task<IActionResult> AddToFavorite(int advertisementId, CancellationToken cancellationToken)
        {
            var result = await _service.AddToFavorite(new CreateFavorite.Request
            {
                AdvertisementId = advertisementId
            }, cancellationToken);

            return Created(result.ToString(), new { });
        }
    }
}
