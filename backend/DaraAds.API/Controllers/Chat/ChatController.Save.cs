using DaraAds.Application.Services.Chat.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.API.Controllers.Chat
{
    public partial class ChatController
    {
        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> Save([FromBody]Request request, CancellationToken cancellationToken)
        {
            await _chatService.CreateChat(new Save.Request
            {
                AdvertisementId = request.advertisementId
            }, cancellationToken);

            return Ok();
        }
    }

    public class Request
    {
        public int advertisementId { get; set; }
    }
}
