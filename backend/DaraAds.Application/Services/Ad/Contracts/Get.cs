namespace DaraAds.Application.Services.Ad.Contracts
{
    public static class Get
    {
        public sealed class Request
        {
            public int Id { get; set; }
        }
        public sealed class Response
        {
            public sealed class OwnerResponse
            {
                public int Id { get; set; }
                public string Email { get; set; }
                public string Name { get; set; }
                public string LastName { get; set; }
            }

            public string Title { get; set; }
            public string Description { get; set; }
            public string Cover { get; set; }
            public string Status { get; set; }
            public decimal Price { get; set; }

            public OwnerResponse Owner { get; set; }
        }
    }
}