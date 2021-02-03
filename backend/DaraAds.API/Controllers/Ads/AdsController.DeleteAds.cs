using DaraAds.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DaraAds.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using DaraAds.API.Controllers.Users;

namespace DaraAds.API.Controllers.Ads
{
    public partial class AdsController : ControllerBase
    {
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteAds(int id)
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
                return Forbid("Нет прав на удаление данного объявления");
            }

            if (advertisement == null)
            {
                return NotFound();
            }

            _context.Advertisements.Remove(advertisement);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}