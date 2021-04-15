using DaraAds.Domain.Shared;

namespace DaraAds.Domain
{
    public class Message : BaseEntity<long>
    {
        public string Text { get; set; }

        public Chat Chat { get; set; }

        public long ChatId { get; set; }
    }
}
