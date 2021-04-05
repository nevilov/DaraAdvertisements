using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace DaraAds.API.Dto.Advertisement
{
    public sealed class AdvertisementAddImageRequest
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public IFormFile Image { get; set; }
    }
}