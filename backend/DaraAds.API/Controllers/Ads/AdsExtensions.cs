using DaraAds.Core.Dto.Advertisement;
using DaraAds.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaraAds.API.Controllers.Ads
{
    public static class AdsExtensions
    {
        public static AdvertisementDto ToDto(Advertisement advertisement) {
            if (advertisement == null)
            {
                return null;
            }

            return new AdvertisementDto()
            {
                UserId = advertisement.OwnerUser.Id,
                Id = advertisement.Id,
                Title = advertisement.Title,
                Description = advertisement.Description,
                Category = advertisement.Category,
                Cover = advertisement.Cover,
                Price = advertisement.Price,
                SubCategory = advertisement.SubCategory
            };
        
        }
    }
}
