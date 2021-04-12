using DaraAds.API.Dto.Chat;
using DaraAds.Application.Services.Chat.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.API.Controllers.Chat
{
    public partial class ChatController
    {
        [HttpPost("save")]
        [Authorize]
        public async Task<IActionResult> Save(SaveRequest request, CancellationToken cancellationToken)
        {
            await _chatService.Save(new Save.Request
            {
                AdvertisementId = request.AdvertisementId,
                CustomerId = request.CustomerId,
                Text = request.Text
            }, cancellationToken);

            return Ok();
        }
    }
}
