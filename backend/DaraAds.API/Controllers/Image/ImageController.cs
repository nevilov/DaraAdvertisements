using DaraAds.Application.Services.Image.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DaraAds.API.Controllers.Image
{
    [ApiController]
    [Authorize(Roles = "User")]
    [Route("api/images")]
    public partial class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }
    }
}