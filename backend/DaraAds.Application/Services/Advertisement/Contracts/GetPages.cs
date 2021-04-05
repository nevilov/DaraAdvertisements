using System.Collections.Generic;
using System.Linq;

namespace DaraAds.Application.Services.Advertisement.Contracts
{
    public static class GetPages
    {
        public sealed class Request : Paged.Request
        {
        }

        public sealed class Response : Paged.Response<Response.Item>
        {
            public sealed class OwnerResponse
            {
                public string Id { get; set; }
                public string Email { get; set; }
                public string Name { get; set; }
                public string LastName { get; set; }
                public string Username { get; set; }
                public IEnumerable<ImageResponse> Images { get; set; }
            }
            
            public sealed class ImageResponse
            {
                public string Id { get; set; }
                public string ImageUrl { get; set; }
                public string ImageBase64 { get; set; }
            }

            public sealed class Item
            {
                public int Id { get; set; }
                public string Title { get; set; }
                public string Description { get; set; }
                public string Cover { get; set; }
                public decimal Price { get; set; }
                public string Status { get; set; }
                public OwnerResponse Owner { get; set; }
                public IEnumerable<ImageResponse> Images { get; set; }
            }
        }
    }
}