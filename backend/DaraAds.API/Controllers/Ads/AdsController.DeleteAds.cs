using DaraAds.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DaraAds.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace DaraAds.API.Controllers.Ads
{
    public partial class AdsController : ControllerBase
    {
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteAds(int id)
        {
            var viewAdv = await _context.Advertisements.FindAsync(id);

            if (viewAdv == null)
            {
                return NotFound();
            }

            _context.Advertisements.Remove(viewAdv);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}