using DaraAds.API.Dto.Advertisement;

namespace DaraAds.API.Controllers.Advertisement
{
    public static class AdvertisementExtensions
    {
        public static AdvertisementDto ToDto(Domain.Advertisement advertisement) {
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
