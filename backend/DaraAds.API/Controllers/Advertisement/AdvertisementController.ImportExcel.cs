using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.API.Controllers.Advertisement
{
    public partial class AdvertisementController
    {
        /// <summary>
        /// Импорт объявлений с Excel файла
        /// </summary>
        /// <param name="excel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("import")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> ImportExcel([Required]IFormFile excel, CancellationToken cancellationToken)
        {
            await _service.ImportExcelProducer(excel, cancellationToken);
            return Ok();
        }
    }
}
