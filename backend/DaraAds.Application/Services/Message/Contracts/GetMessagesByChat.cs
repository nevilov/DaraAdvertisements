using System;
using System.Collections.Generic;

namespace DaraAds.Application.Services.Message.Contracts
{
    public static class GetMessagesByChat
    {
        public class Request
        {
            public long ChatId { get; set; }
        }

        public class Response
        {
            public sealed class UserReponse
            {
                public string Id { get; set; }
                public string Name { get; set; }
                public string Lastname { get; set; }
            }
            public sealed class Message
            {
                public string Text { get; set; }
                public UserReponse Sender { get; set; }
                public UserReponse Recipient { get; set; }
                public DateTime CreatedDate { get; set; }
            }

            public IEnumerable<Message> Messages { get; set; }
        }
    }
}
