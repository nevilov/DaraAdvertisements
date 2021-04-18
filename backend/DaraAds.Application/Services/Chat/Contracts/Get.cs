using System;
using System.Collections;
using System.Collections.Generic;

namespace DaraAds.Application.Services.Chat.Contracts
{
    public static class Get
    {
        public class Response
        {
            public class ChatItem
            {
                public long ChatId { get; set; }

                public string UserId { get; set; }

                public string Name { get; set; }

                public string Lastname { get; set; }

                public DateTime? UpdatedDate { get; set; }
            }

            public IEnumerable<ChatItem> Chats { get; set; }
        }
    }
}
