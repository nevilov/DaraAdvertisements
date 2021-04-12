using DaraAds.Application.Identity.Interfaces;
using DaraAds.Application.Repositories;
using DaraAds.Application.Services.Chat.Contracts;
using DaraAds.Application.Services.Chat.Interfaces;
using DaraAds.Application.SignalR.Interfaces;
using DaraAds.Domain;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace DaraAds.Application.Services.Chat.Implementations
{
    public class ChatService : IChatService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly IRepository<Domain.User, string> _userRepository;
        private readonly IIdentityService _identityService;
        private readonly ISignalRService _signalRService;

        public ChatService(IMessageRepository messageRepository, IAdvertisementRepository advertisementRepository, IRepository<Domain.User, string> userRepository, IIdentityService identityService, ISignalRService signalRService)
        {
            _messageRepository = messageRepository;
            _advertisementRepository = advertisementRepository;
            _userRepository = userRepository;
            _identityService = identityService;
            _signalRService = signalRService;
        }

        public async Task<Get.Response> GetChats(Get.Request request, CancellationToken cancellationToken)
        {
            var userId = await _identityService.GetCurrentUserId(cancellationToken);
            var advertisement = await _advertisementRepository.FindById(request.AdvertisementId, cancellationToken);
            Message[] messages;

            if(advertisement.OwnerId == userId)
            {
                messages = await _messageRepository.FindByAdvertisementAndUsers(advertisement.Id, advertisement.OwnerId);
            }
            else
            {
                messages = await _messageRepository.FindByAdvertisementAndUsers(advertisement.Id, advertisement.OwnerId, userId);
            }

            var chats = messages.GroupBy(m => m.CustomerId)
                .Select(g => new Get.Response.Chat
                {
                    CustomerId = g.Key,
                    CustomerName = g.First().Customer.Name,
                    Messages = g.OrderByDescending(m => m.CreatedDate)
                        .Select(a => new Get.Response.Chat.Message
                        {
                            SenderName = a.IsSenderCustomer ? a.Customer.Name : a.Advertisement.OwnerUser.Name,
                            CreatedDate = a.CreatedDate,
                            Text = a.Text
                        }).ToArray()
                }).ToArray();

            return new Get.Response
            {
                Chats = chats
            };
        }

        public async Task Save(Save.Request request, CancellationToken cancellationToken)
        {
            var userId = await _identityService.GetCurrentUserId(cancellationToken);
            var advertisement = await _advertisementRepository.FindById(request.AdvertisementId, cancellationToken);

            request.CustomerId ??= userId;

            var message = new Message
            {
                AdvertisementId = advertisement.Id,
                CreatedDate = DateTime.UtcNow,
                CustomerId = request.CustomerId,
                Customer = await _userRepository.FindById(request.CustomerId, cancellationToken),
                Text = request.Text,
                IsSenderCustomer = advertisement.OwnerId != userId
            };

            await _messageRepository.Save(message, cancellationToken);

            await _signalRService.SendMessage(new SignalR.Contracts.Message
            {
                CreatedDate = message.CreatedDate,
                SellerId = advertisement.OwnerId,
                SenderName = message.IsSenderCustomer ? message.Customer.Name : advertisement.OwnerUser.Name,
                Text = message.Text,
                CustomerName = message.Customer.Name,
                CustomerId = message.CustomerId
            }, cancellationToken);
        }
    }
}
