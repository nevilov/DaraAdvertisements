namespace DaraAds.Application.Services.Ad.Contracts
{
    public static class Create
    {
        public sealed class Request
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public string Cover { get; set; }
            
            //Решить судьбу этих полей
            //public string Category { get; set; }
            //public string SubCategory { get; set; }
        }

        public sealed class Response
        {
            public int Id { get; set; }
        }
    }
}