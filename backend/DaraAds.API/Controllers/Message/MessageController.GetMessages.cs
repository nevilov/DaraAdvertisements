using DaraAds.Application.Services.Message.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.API.Controllers.Message
{
    public partial class MessageController
    {
        [HttpGet("get")]
        public async Task<IActionResult> GetMessages(long chatId, CancellationToken cancellationToken)
        {
            var result = await _messageService.GetMessagesByChat(new GetMessagesByChat.Request
            {
                ChatId = chatId
            }, cancellationToken);

            return Ok(result);
        }
    }
}
