using System;

namespace DaraAds.Application.Services.Advertisement.Contracts
{
    public static class Update
    {
        public sealed class Request
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public string Cover { get; set; }
        }
    }
}