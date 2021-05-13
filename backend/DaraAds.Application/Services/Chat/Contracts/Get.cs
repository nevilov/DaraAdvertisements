using System;
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

                public string Avatar { get; set; }

                public AdvertisementItem Advertisement { get; set; }

                public DateTime? UpdatedDate { get; set; }
            }

            public class AdvertisementItem
            {
                public int Id { get; set; }

                public string Title { get; set; }

            }


            public IEnumerable<ChatItem> Chats { get; set; }
        }
    }
}
