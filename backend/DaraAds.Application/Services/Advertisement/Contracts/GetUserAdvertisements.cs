using System;
using System.Collections.Generic;

namespace DaraAds.Application.Services.Advertisement.Contracts
{
    public static class GetUserAdvertisements
    {
        public class Request : Paged.Request
        {
            public string Id { get; set; }
            public string SortBy { get; set; }
            public string SortDirection { get; set; }

        }

        public class Response : Paged.Response<Response.Item>
        {
            public sealed class Item
            {
                public int Id { get; set; }
                public string Title { get; set; }
                public string Description { get; set; }
                public string Cover { get; set; }
                public DateTime CreatedDate { get; set; }
                public decimal Price { get; set; }
                public string Status { get; set; }
                public IEnumerable<ImageResponse> Images { get; set; }
                public CategoryResponse Category { get; set; }
                public string Location { get; set; }
            }

           

            public sealed class ImageResponse
            {
                public string Id { get; set; }
                public string ImageUrl { get; set; }
            }
            
            public sealed class CategoryResponse
            {
                public int Id { get; set; }
                public string Name { get; set; }
            }
        }

    }
}
