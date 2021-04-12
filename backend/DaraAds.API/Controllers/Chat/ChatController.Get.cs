using DaraAds.Application.Services.Chat.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.API.Controllers.Chat
{
    public partial class ChatController
    {
        [HttpGet("get/{advertisementId}")]
        [Authorize]
        public async Task<IActionResult> GetChats(int advertisementId, CancellationToken cancellationToken)
        {
            var result = await _chatService.GetChats(new Get.Request
            {
                AdvertisementId = advertisementId
            }, cancellationToken);

            return Ok(result);
        }
    }
}
