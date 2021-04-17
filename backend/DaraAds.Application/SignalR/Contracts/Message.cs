using System;

namespace DaraAds.Application.SignalR.Contracts
{
    public class Message
    {
        public string Text { get; set; }
        public string SenderId { get; set; }
        public string SenderName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string RecipientId { get; set; }
        public string RecipientName { get; set; }
    }
}
