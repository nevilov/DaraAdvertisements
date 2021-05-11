using DaraAds.API.Dto;
using DaraAds.Application.Services.Favorite.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using DaraAds.API.Dto.Favorite;

namespace DaraAds.API.Controllers.Favorite
{
    public partial class FavoriteController
    {
        /// <summary>
        /// Получить избранные объявления пользователя
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("get")]
        [Authorize]
        public async Task<IActionResult> GetFavorite([FromQuery] GetRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.GetFavorites(new GetFavorites.Request
            {   
                Limit = request.Limit,
                Offset = request.Offset,
                SortBy = request.SortBy,
                SortDirection = request.SortDirection,
            }, cancellationToken);

            return Ok(result);
        }
    }
}
