using DaraAds.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DaraAds.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using DaraAds.Core.Dto.Advertisement;
using DaraAds.API.Controllers.Users;

namespace DaraAds.API.Controllers.Ads
{
    
    public partial class AdsController : ControllerBase
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostTodoItem(AdvertisementDto newAdvertisement)
        {
            if (newAdvertisement == null)
            {
                return BadRequest();
            }

            var userDto = HttpContext.User.ToDto();

            var user = _context.Users.FirstOrDefault(u => u.Id == userDto.Id);
            if (user == null)
            {
                return BadRequest($"Не существует пользователя с Id: {userDto.Id}");
            }

            var Ads = new Advertisement
            {
                Id = _context.Advertisements.Count() + 1,
                Title = newAdvertisement.Title,
                Description = newAdvertisement.Description,
                Price = newAdvertisement.Price,
                Cover = newAdvertisement.Cover,
                Category = newAdvertisement.Category,
                SubCategory = newAdvertisement.SubCategory,
                OwnerUser = user
            };

            _context.Advertisements.Add(Ads);
            await _context.SaveChangesAsync();

            
            return Created($"api/Ads/{Ads.Id}", AdsExtensions.ToDto(Ads));
        }

    }
}
