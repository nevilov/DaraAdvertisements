namespace DaraAds.API.Dto.Chat
{
    public class SaveRequest
    {
        public int AdvertisementId { get; set; }

        public string Text { get; set; }

        public string CustomerId { get; set; }
    }
}
