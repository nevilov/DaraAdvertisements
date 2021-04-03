using System.Collections.Generic;
using System.Linq;

namespace DaraAds.Application.Services.Advertisement.Contracts
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
                public string Id { get; set; }
                public string Email { get; set; }
                public string Name { get; set; }
                public string LastName { get; set; }
            }
            
            public sealed class CategoryResponse
            {
                public int ParentId { get; set; }
                public string ParentName { get; set; }
                public int Id { get; set; }
                public string Name { get; set; }
            }

            public sealed class ImageResponse
            {
                public string Id { get; set; }
                public string ImageUrl { get; set; }
                public string ImageBase64 { get; set; }
            }
            
            public string Title { get; set; }
            public string Description { get; set; }
            public string Cover { get; set; }
            public IEnumerable<ImageResponse> Images{ get; set; }
            public string Status { get; set; }
            public decimal Price { get; set; }
            public CategoryResponse Category { get; set; }
            public OwnerResponse Owner { get; set; }
        }
    }
}