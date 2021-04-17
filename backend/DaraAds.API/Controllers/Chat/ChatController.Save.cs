using DaraAds.Application.Services.Chat.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.API.Controllers.Chat
{
    public partial class ChatController
    {
        /// <summary>
        /// Создать чат
        /// </summary>
        /// <param name="advertisementId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> Save(int advertisementId, CancellationToken cancellationToken)
        {
            await _chatService.CreateChat(new Save.Request
            {
                AdvertisementId = advertisementId
            }, cancellationToken);

            return Ok();
        }
    }
}
