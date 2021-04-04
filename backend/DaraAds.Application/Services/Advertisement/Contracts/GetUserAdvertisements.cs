using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaraAds.Application.Services.Advertisement.Contracts
{
    public static class GetUserAdvertisements
    {
        public class Request : Paged.Request
        {
        }

        public class Response : Paged.Response<Response.Item>
        {
            public sealed class Item
            {
                public int Id { get; set; }
                public string Title { get; set; }
                public string Description { get; set; }
                public string Cover { get; set; }
                public decimal Price { get; set; }
                public string Status { get; set; }
            }
        }

    }
}
