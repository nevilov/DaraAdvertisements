using System;

namespace DaraAds.Application.SignalR.Contracts
{
    public class Message
    {
        public string Text { get; set; }
        public string SenderName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string SellerId { get; set; }

    }
}
