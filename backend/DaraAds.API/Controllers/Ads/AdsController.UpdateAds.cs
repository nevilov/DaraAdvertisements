using DaraAds.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using DaraAds.API.Controllers.Users;
using DaraAds.Domain;

namespace DaraAds.API.Controllers.Ads
{
    public partial class AdsController : ControllerBase
    {
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateAds(int id, Advertisement newAdvertisement)
        {
            var userDto = HttpContext.User.ToDto();
            var user = _context.Users.FirstOrDefault(u => u.Id == userDto.Id);
            if (user == null)
            {
                return BadRequest($"Не существует пользователя с Id: {userDto.Id}");
            }

            var advertisement = _context.Advertisements.FirstOrDefault(adv => adv.Id == id);

            if (advertisement == null)
            {
                return NotFound($"Не существует объявления с Id:{id}");
            }

            if (advertisement.OwnerUser.Id != user.Id)
            {
                return Forbid("Нет прав на обновление данного объявления");
            }

            if (advertisement == null)
            {
                return NotFound();
            }

            if (id != newAdvertisement.Id)
            {
                return BadRequest();
            }

            _context.Entry(newAdvertisement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdvExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool AdvExists(int id)
        {
            return _context.Advertisements.Any(e => e.Id == id);
        }
    }
}
