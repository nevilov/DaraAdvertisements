using DaraAds.Application.Common;
using DaraAds.Application.Identity.Contracts.Exceptions;
using DaraAds.Application.Identity.Interfaces;
using DaraAds.Application.Repositories;
using DaraAds.Application.Services.Notification.Contracts;
using DaraAds.Application.Services.Notification.Interfaces;
using MassTransit;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.Application.Services.Notification.Implementations
{
    public class NotificationService : INotificationService
    {
        private readonly IIdentityService _identityService;
        private readonly IRepository<Domain.User, string> _userRepository;
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public NotificationService(IIdentityService identityService,
            ISendEndpointProvider sendEndpointProvider,
            IRepository<Domain.User, string> userRepository)
        {
            _identityService = identityService;
            _sendEndpointProvider = sendEndpointProvider;
            _userRepository = userRepository;
        }

        public async Task Send(string subject, string message, CancellationToken cancellationToken)
        {
            var userId = await _identityService.GetCurrentUserId(cancellationToken);
            if(!await _identityService.IsInRole(userId, RoleConstants.AdminRole, cancellationToken))
            {
                throw new HaveNoRightException("Нет прав сделать рассылку");
            }
            var sendNotificationEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:send_notifications"));

            var recipients = await _userRepository.ListFindWhere(a => a.IsSubscribedToNotifications, cancellationToken);

            foreach(var recipient in recipients)
             {
                await sendNotificationEndpoint.Send(new SendNotificationMessage 
                {
                    RecipientEmail = recipient.Email,
                    Subject = subject,
                    Message = message
                }, cancellationToken);
            }
        }
    }
}
