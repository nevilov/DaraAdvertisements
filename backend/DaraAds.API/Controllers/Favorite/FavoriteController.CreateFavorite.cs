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
        [Authorize(Roles = "User")]
        [Route("create")]
        [HttpPost]
        public async Task<IActionResult> CreateFavorite(int advertisementId, CancellationToken cancellationToken)
        {
            var result = await _service.CreateFavorite(new CreateFavorite.Request
            {
                AdvertisementId = advertisementId
            }, cancellationToken);

            return Created(result.ToString(), new { });
        }
    }
}
