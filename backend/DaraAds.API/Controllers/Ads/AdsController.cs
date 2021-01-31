using DaraAds.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DaraAds.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DaraAds.API.Controllers.Ads
{
    [ApiController]
    [Route("api/[controller]")]
    public partial class AdsController : ControllerBase
    {
        private readonly DaraAdsDbContext _context;

        public AdsController(DaraAdsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Advertisement>>> GetAds()
        {
            return await _context.Advertisements.ToListAsync();
        }

    }
}
