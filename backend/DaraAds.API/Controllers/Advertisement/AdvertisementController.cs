using DaraAds.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaraAds.API.Controllers.Advertisement
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdvertisementController : ControllerBase
    {
        private readonly DaraAdsDbContext _context;

        public AdvertisementController(DaraAdsDbContext context)
        {
            _context = context;
        }
        
    }
}
