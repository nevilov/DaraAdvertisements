using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace DaraAds.API.Dto.Image
{
    public sealed class ImageUploadRequest
    { 
        [Required] 
        public IFormFile Image { get; set; }
    }
}