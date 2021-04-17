using DaraAds.Domain.Shared;

namespace DaraAds.Domain
{
    public class Message : BaseEntity<long>
    {
        public string Text { get; set; }

        public virtual User Sender { get; set; }

        public string SenderId { get; set; }

        public virtual User Recipient { get; set; }

        public string RecipientId { get; set; }

        public virtual Chat Chat { get; set; }

        public long ChatId { get; set; }
    }
}
