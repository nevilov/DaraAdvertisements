using DaraAds.Application.Services.Chat.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DaraAds.API.Controllers.Chat
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public partial class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }
    }
}
