using DaraAds.Domain.Shared;
using System.Collections.Generic;

namespace DaraAds.Domain
{
    public class Chat : MutableEntity<long>
    {
        public virtual Advertisement Advertisement { get; set; }

        public virtual User Buyer { get; set; }

        public string BuyerId { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}
