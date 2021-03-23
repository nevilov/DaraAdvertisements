using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaraAds.Application.Services.Advertisement.Contracts
{
    public static class Search
    {
        public class Request
        {
            public string KeyWord { get; set; }

            public int Offset { get; set; } = 0;

            public int Limit { get; set; } = 10;
        }

        public class Response
        {
            public class Item
            {
                public int Id { get; set; }
                public string Title { get; set; }
                public string Description { get; set; }
                public decimal Price { get; set; }
                public string Cover { get; set; }
                public string Status { get; set; }
            }

            public int Total { get; set; }

            public int Limit { get; set; }

            public int Offset { get; set; }

            public IEnumerable<Item> Items { get; set; } = Enumerable.Empty<Item>();
        }
    }
}
