using DaraAds.Application.Services.Message.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.API.Controllers.Message
{
    public partial class MessageController
    {
        [Authorize]
        [HttpGet("get/{chatId}")]
        public async Task<IActionResult> GetMessages([FromRoute]long chatId, CancellationToken cancellationToken)
        {
            var result = await _messageService.GetMessagesByChat(new GetMessagesByChat.Request
            {
                ChatId = chatId
            }, cancellationToken);

            return Ok(result);
        }
    }
}
