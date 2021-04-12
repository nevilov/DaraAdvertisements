using DaraAds.Domain.Shared;

namespace DaraAds.Domain
{
    public class Message : BaseEntity<long>
    {
        public string Text { get; set; }

        public int AdvertisementId { get; set; }

        public virtual Advertisement Advertisement { get; set; }

        public string CustomerId { get; set; }

        public virtual User Customer { get; set; }

        public bool IsSenderCustomer { get; set; }
    }
}
