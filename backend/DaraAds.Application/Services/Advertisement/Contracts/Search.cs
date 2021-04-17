using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaraAds.Application.Common;

namespace DaraAds.Application.Services.Advertisement.Contracts
{
    public static class Search
    {
        public class Request : Paged.Request
        {
            public string KeyWord { get; set; }
            
        }

        public class Response : Paged.Response<Response.Item>
        {
            public sealed class ImageResponse
            {
                public string Id { get; set; }
                public string ImageUrl { get; set; }
                public string ImageBase64 { get; set; }
            }
            public class Item
            {
                public int Id { get; set; }
                public string Title { get; set; }
                public string Description { get; set; }
                public decimal Price { get; set; }
                public string Cover { get; set; }
                public string Status { get; set; }
                public IEnumerable<ImageResponse> Images { get; set; }
            }
        }
    }
}
