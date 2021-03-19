using System.Threading;
using System.Threading.Tasks;
using DaraAds.Application.Services.Advertisement.Contracts;
using DaraAds.Application.Services.Advertisement.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DaraAds.API.Controllers.Advertisement
{
    public partial class AdvertisementController : ControllerBase
    {
        [Authorize(Roles = "User")]
        [HttpDelete("{id:int}")]
            public async Task<IActionResult> Delete(
            [FromRoute] int id,
            CancellationToken cancellationToken
            )
        {
            await _service.Delete(new Delete.Request
            {
                Id = id
            }, cancellationToken);
            return NoContent();
        }
    }
}