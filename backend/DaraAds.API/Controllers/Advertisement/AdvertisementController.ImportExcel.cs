using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.API.Controllers.Advertisement
{
    public partial class AdvertisementController
    {
        /// <summary>
        /// Импорт объявлений с Excel файла
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("import")]
        [AllowAnonymous]
        public async Task<IActionResult> ImportExcel(IFormFile request, CancellationToken cancellationToken)
        {
            await _service.ImportExcelProducer(request.OpenReadStream(), cancellationToken);
            return Ok();
        }
    }
}
