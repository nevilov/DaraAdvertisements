using DaraAds.API.Dto.Message;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.API.Controllers.Message
{
    public partial class MessageController
    {
        [Authorize]
        [HttpPost("send")]
        public async Task<IActionResult> SendMessage(SendMessageRequest request, CancellationToken cancellationToken)
        {
            await _messageService.SendMessage(request.ChatId, request.RecipientId, request.Text, cancellationToken);
            return NoContent();
        }
    }
}
