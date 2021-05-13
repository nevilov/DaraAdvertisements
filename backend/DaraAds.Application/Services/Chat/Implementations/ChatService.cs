using DaraAds.Application.Identity.Interfaces;
using DaraAds.Application.Repositories;
using DaraAds.Application.Services.Advertisement.Contracts.Exceptions;
using DaraAds.Application.Services.Chat.Contracts;
using DaraAds.Application.Services.Chat.Contracts.Exceptions;
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
                        ChatId = a.Id,
                        UserId = a.BuyerId,
                        Name = a.Buyer.Name,
                        Lastname = a.Buyer.LastName,
                        Avatar = a.Buyer.Avatar,
                        UpdatedDate = a.UpdatedDate,
                        Advertisement = new Get.Response.AdvertisementItem
                        {
                            Id = a.Advertisement.Id,
                            Title = a.Advertisement.Title
                        }
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
                        ChatId = a.Id,
                        UserId = a.Advertisement.OwnerId,
                        Name = a.Advertisement.OwnerUser.Name,
                        Lastname = a.Advertisement.OwnerUser.LastName,
                        Avatar = a.Advertisement.OwnerUser.Avatar,
                        UpdatedDate = a.UpdatedDate,
                        Advertisement = new Get.Response.AdvertisementItem
                        {
                            Id = a.Advertisement.Id,
                            Title = a.Advertisement.Title
                        }
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

            var userId = await _identityService.GetCurrentUserId(cancellationToken);

            var chatDuplicate = await _chatRepository.FindWhere(c => c.Advertisement.Id == advertisement.Id && c.BuyerId == userId, cancellationToken);
            if(chatDuplicate != null)
            {
                throw new ChatDuplicateException($"Чат с id {chatDuplicate.Id} уже создан");
            }

            if(userId == advertisement.OwnerId)
            {
                throw new ChatException($"Попытка создать чат с самим собой, пользователь с id {userId}, является владельцем объявления");
            }

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
