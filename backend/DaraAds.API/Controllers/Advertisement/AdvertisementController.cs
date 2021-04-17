using DaraAds.Application.Services.Advertisement.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DaraAds.API.Controllers.Advertisement
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public partial class AdvertisementController : ControllerBase
    {
        private readonly IAdvertisementService _service;
        public AdvertisementController(IAdvertisementService service)
        {
            _service = service;
        }
    }
}
