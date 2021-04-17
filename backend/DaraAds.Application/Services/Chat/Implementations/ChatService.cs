using DaraAds.Application.Identity.Interfaces;
using DaraAds.Application.Repositories;
using DaraAds.Application.Services.Advertisement.Contracts.Exceptions;
using DaraAds.Application.Services.Chat.Contracts;
using DaraAds.Application.Services.Chat.Interfaces;
using DaraAds.Application.SignalR.Interfaces;
using Microsoft.AspNetCore.Server.IIS.Core;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.Application.Services.Chat.Implementations
{
    public class ChatService : IChatService
    {
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly IChatRepository _chatRepository;
        private readonly IIdentityService _identityService;

        public ChatService(IAdvertisementRepository advertisementRepository,
            IIdentityService identityService,
            IChatRepository chatRepository)
        {
            _advertisementRepository = advertisementRepository;
            _identityService = identityService;
            _chatRepository = chatRepository;
        }

        public async Task<Get.Response> GetChats(bool isSeller, CancellationToken cancellationToken)
        {
            var userId = await _identityService.GetCurrentUserId(cancellationToken);
            
            if (isSeller)
            {
                var chats = await _chatRepository.GetChats(userId, isSeller, cancellationToken);
                return new Get.Response
                {
                    Chats = chats.Select(a => new Get.Response.ChatItem
                    {
                        Name = a.Buyer.Name,
                        Lastname = a.Buyer.LastName,
                        UpdatedDate = a.UpdatedDate,
                    })
                };
            }
            else
            {
                var chats = await _chatRepository.GetChats(userId, isSeller, cancellationToken);
                return new Get.Response
                {
                    Chats = chats.Select(a => new Get.Response.ChatItem
                    {
                        Name = a.Advertisement.OwnerUser.Name,
                        Lastname = a.Advertisement.OwnerUser.LastName,
                        UpdatedDate = a.UpdatedDate,
                    })
                };
            }
        }

        public async Task CreateChat(Save.Request request, CancellationToken cancellationToken)
        {
            var advertisement = await _advertisementRepository.FindById(request.AdvertisementId, cancellationToken);
            if(advertisement == null)
            {
                throw new AdNotFoundException(request.AdvertisementId);
            }

           // var chatDuplicate = await _chatRepository.FindById()

            var userId = await _identityService.GetCurrentUserId(cancellationToken);

            var chat = new Domain.Chat
            {
                Advertisement = advertisement,
                BuyerId = userId,
                CreatedDate = DateTime.UtcNow,
            };

            await _chatRepository.Save(chat, cancellationToken);
        }
    }
}
