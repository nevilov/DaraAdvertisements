using DaraAds.Domain.Shared;
using System.Collections.Generic;

namespace DaraAds.Domain
{
    public class Chat : MutableEntity<long>
    {
        public Advertisement Advertisement { get; set; }

        public User Buyer { get; set; }

        public string BuyerId { get; set; }

        public ICollection<Message> Messages { get; set; }
    }
}
