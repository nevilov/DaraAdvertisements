using DaraAds.Application.Services.Favorite.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaraAds.API.Controllers.Favorite
{
    [ApiController]
    [Route("api/[controller]")]
    public partial class FavoriteController
    {
        private IFavoriteService _service;
        public FavoriteController(IFavoriteService service)
        {
            _service = service;
        }
    }
}
