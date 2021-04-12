namespace DaraAds.Application.Services.Chat.Contracts
{
    public static class Save
    {
        public sealed class Request
        {
            public int AdvertisementId { get; set; }

            public string Text { get; set; }

            public string CustomerId { get; set; }
        }
    }
}
