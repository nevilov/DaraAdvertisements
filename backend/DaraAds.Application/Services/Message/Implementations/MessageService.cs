using DaraAds.Application.Identity.Interfaces;
using DaraAds.Application.Repositories;
using DaraAds.Application.Services.Message.Contracts;
using DaraAds.Application.Services.Message.Contracts.Exceptions;
using DaraAds.Application.Services.Message.Interfaces;
using DaraAds.Application.Services.User.Interfaces;
using DaraAds.Application.SignalR.Interfaces;
using DaraAds.Domain;
using Microsoft.Extensions.Configuration.UserSecrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.Application.Services.Message.Implementations
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IChatRepository _chatRepository;
        private readonly IIdentityService _identityService;
        private readonly ISignalRService _signalRService;
        private readonly IRepository<Domain.User, string> _userRepository;

        public MessageService(IMessageRepository messageRepository,
            IChatRepository chatRepository,
            IIdentityService identityService,
            ISignalRService signalRService, IRepository<Domain.User, string> userRepository)
        {
            _messageRepository = messageRepository;
            _chatRepository = chatRepository;
            _identityService = identityService;
            _signalRService = signalRService;
            _userRepository = userRepository;
        }

        public async Task<GetMessagesByChat.Response> GetMessagesByChat(GetMessagesByChat.Request request, CancellationToken cancellationToken)
        {
            var chat = await _chatRepository.FindById(request.ChatId, cancellationToken);
            if(chat == null)
            {
                throw new ChatNotFoundExceptions($"Чат с id {request.ChatId} не был найден");
            }

            var userId = await _identityService.GetCurrentUserId(cancellationToken);
            if(userId != chat.BuyerId && userId != chat.Advertisement.OwnerId)
            {
                throw new HaveNoRigthToGetMessageByChatException($"Нет прав просматривать сообщения чата id {request.ChatId}");
            }

            var messages = await _messageRepository.FindMessagesByChat(request.ChatId, cancellationToken);

            return new GetMessagesByChat.Response
            {
                Messages = messages.Select(m => new GetMessagesByChat.Response.Message
                {
                    Text = m.Text,
                    CreatedDate = m.CreatedDate,
                    Recipient = new GetMessagesByChat.Response.UserReponse
                    {
                        Id = m.Recipient.Id,
                        Name = m.Recipient.Name,
                        Lastname = m.Recipient.LastName
                    },
                    Sender = new GetMessagesByChat.Response.UserReponse
                    {
                        Id = m.Sender.Id,
                        Name = m.Sender.Name,
                        Lastname = m.Sender.LastName
                    }
                })
            };
        }

        public async Task SendMessage(long chatId, string recipientId, string text, CancellationToken cancellationToken)
        {
            var chat = await _chatRepository.FindById(chatId, cancellationToken);
            if (chat == null)
            {
                throw new ChatNotFoundExceptions($"Чат с id {chatId} не был найден");
            }

            var userId = await _identityService.GetCurrentUserId(cancellationToken);

            if(chat.BuyerId != userId && chat.Advertisement.OwnerId != userId)
            {
                throw new HaveNoRigthToSendMessageChat($"Чат с id {chatId} не пренадлежит вам");
            }

            //Kostil, users is null;
            var sender = await _userRepository.FindById(userId, cancellationToken);
            var recipient = await _userRepository.FindById(recipientId, cancellationToken);
            if (recipient == null)
            {
                throw new UserNotFoundException($"Пользователь с id{recipientId}, которому отправляется сообщение не был найден");
            }

            var message = new Domain.Message
            {
                Text = text,
                SenderId = userId,
                Sender = sender,
                RecipientId = recipientId,
                Recipient = recipient,
                CreatedDate = DateTime.UtcNow,
                ChatId = chatId
            };

            await _messageRepository.Save(message, cancellationToken);

            await _signalRService.SendMessage(new SignalR.Contracts.Message
            {
                SenderId = message.SenderId,
                SenderName = message.Sender.Name,
                CreatedDate = message.CreatedDate,
                Text = message.Text,
                RecipientId = message.RecipientId,
                RecipientName = message.Recipient.Name
            }, cancellationToken);
        }
    }
}
