using System.Threading;
using System.Threading.Tasks;
using DaraAds.Application.Services.Advertisement.Contracts;
using DaraAds.Application.Services.Advertisement.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DaraAds.API.Controllers.Advertisement
{
    public partial class AdvertisementController : ControllerBase
    {
        [HttpDelete("{id:int}")]
            public async Task<IActionResult> Delete(
            [FromRoute] int id,
            [FromServices] IAdvertisementService service,
            CancellationToken cancellationToken
            )
        {
            await service.Delete(new Delete.Request
            {
                Id = id
            }, cancellationToken);
            return NoContent();
        }
    }
}