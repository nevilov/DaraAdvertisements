using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.API.Controllers.Advertisement
{
    public partial class AdvertisementController
    {
        [HttpPost("import")]
        public async Task<IActionResult> ImportExcel(IFormFile request, CancellationToken cancellationToken)
        {
            await _service.ImportExcelProducer(request.OpenReadStream(), cancellationToken);
            return Ok();
        }
    }
}
