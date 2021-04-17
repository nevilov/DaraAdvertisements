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
                public string Name { get; set; }

                public string Lastname { get; set; }

                public string Cover { get; set; }

                public DateTime? UpdatedDate { get; set; }
            }

            public IEnumerable<ChatItem> Chats { get; set; }
        }
    }
}
