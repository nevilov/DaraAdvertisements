using DaraAds.Application.Services.Mail.Interfaces;
using DaraAds.Application.Services.Notification.Contracts;
using MassTransit;
using System.Threading.Tasks;

namespace DaraAds.Infrastructure.Consumers
{
    public class SendNotificationConsumer : IConsumer<SendNotificationMessage>
    {
        private readonly IMailService _mailService;
        public SendNotificationConsumer(IMailService mailService)
        {
            _mailService = mailService;
        }
        public async Task Consume (ConsumeContext<SendNotificationMessage> context)
        {
            var message = context.Message;
            await _mailService.Send(message.RecipientEmail, message.Subject, message.Message, new System.Threading.CancellationToken());
        }
    }
}
