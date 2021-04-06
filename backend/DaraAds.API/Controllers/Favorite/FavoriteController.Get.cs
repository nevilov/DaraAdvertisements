using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaraAds.API.Controllers.Favorite
{
    public partial class FavoriteController
    {
        [HttpGet("get")]
        public async Task<IActionResult> GetFavorite([FromQuery] string )
    }
}
