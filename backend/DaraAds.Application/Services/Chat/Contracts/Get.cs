using System;

namespace DaraAds.Application.Services.Chat.Contracts
{
    public static class Get
    {
        public class Request
        {
            public int AdvertisementId { get; set; }
        }

        public class Response
        {
            public Chat[] Chats { get; set; }

            public sealed class Chat
            {
                public string CustomerId { get; set; }
                public string CustomerName { get; set; }
                public Message[] Messages { get; set; }

                public sealed class Message
                {
                    public string Text { get; set; }
                    public string SenderName { get; set; }
                    public DateTime CreatedDate { get; set; }
                }
            }
        }
    }
}
