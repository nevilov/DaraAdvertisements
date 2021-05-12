namespace DaraAds.Application.Services.Advertisement.Contracts
{
    public static class Create
    {
        public sealed class Request
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public string Cover { get; set; }
            public int CategoryId { get; set; }
            public string Location { get; set; }
            public decimal GeoLat { get; set; }
            public decimal GeoLon { get; set; }
        }

        public sealed class Response
        {
            public int Id { get; set; }
        }
    }
}