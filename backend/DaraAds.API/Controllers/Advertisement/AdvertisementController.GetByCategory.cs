using DaraAds.Application.Services.Advertisement.Contracts;
using DaraAds.Application.Services.Advertisement.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
namespace DaraAds.API.Controllers.Advertisement
{
    public partial class AdvertisementController : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet("category")]
        public async Task<IActionResult> GetByCategory([FromQuery] GetByCategoryRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _service.GetPagesByCategory(new GetPagesByCategory.Request
            {
               idCategory = request.idCategory,
               Limit = request.Limit,
               Offset = request.Offset
            }, cancellationToken);

            return Ok(result);
        }


        public sealed class GetByCategoryRequest{
         public int idCategory { get; set;}

         /// <summary>
         /// Количество возвращаемых объявлений
         /// </summary>
         public int Limit { get; set; } = 10;

        /// <summary>
        /// Смещение начиная с котрого возвращаются объявления
        /// </summary>
        public int Offset { get; set; } = 0;
         }
    }
}
