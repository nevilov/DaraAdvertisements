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
        /// Получить чат
        /// </summary>
        /// <param name="isSeller"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("get/{isSeller}")]
        [Authorize]
        public async Task<IActionResult> GetChats(bool isSeller, CancellationToken cancellationToken)
        {
            var result = await _chatService.GetChats(isSeller, cancellationToken);

            return Ok(result);
        }
    }
}
