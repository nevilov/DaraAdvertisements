using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using DaraAds.Application.Services.Advertisement.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DaraAds.API.Controllers.Advertisement
{
    public partial class AdvertisementController
    {
        /// <summary>
        /// Добавить картинку в объявление (Пользователь)
        /// </summary>
        /// <param name="id">Идентификатор объявления</param>
        /// <param name="image">Картинка</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPatch("{id}/images")]
        public async Task<IActionResult> AddImage(
            int id,[Required] IFormFile image, 
            CancellationToken cancellationToken)
        {

            await _service.AddImage(new AddImage.Request
            {
                Id = id,
                Image = image
            }, cancellationToken);
            
            return NoContent();
        }
    }
}