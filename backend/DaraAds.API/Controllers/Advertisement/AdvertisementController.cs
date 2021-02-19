using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DaraAds.API.Controllers.Advertisement
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public partial class AdvertisementController : ControllerBase
    {
    }
}
