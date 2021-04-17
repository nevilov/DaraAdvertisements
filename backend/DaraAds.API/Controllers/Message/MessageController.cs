using DaraAds.Application.Services.Message.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DaraAds.API.Controllers.Message
{
    [ApiController]
    [Route("api/[controller]")]
    public partial class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }
    }
}
