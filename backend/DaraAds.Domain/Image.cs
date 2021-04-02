using DaraAds.Domain.Shared;

namespace DaraAds.Domain
{
    public class Image : MutableEntity<string>
    {
        public string Name { get; set; }
        
        public byte[] ImageBlob { get; set; }
        
        public int? AdvertisementId { get; set; }
        
        public virtual Advertisement Advertisement { get; set; }
        
        public string UserId { get; set; }
        
        public virtual User User { get; set; }
    }
}