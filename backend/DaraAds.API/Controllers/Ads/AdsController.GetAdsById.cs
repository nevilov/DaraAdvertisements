using DaraAds.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DaraAds.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace DaraAds.API.Controllers.Ads
{
    public partial class AdsController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<Advertisement>> GetAdsById(int id)
        {
            var viewAdv = await _context.Advertisements.FindAsync(id);

            if (viewAdv == null)
            {
                return NotFound();
            }

            return viewAdv;
        }
    }
}
