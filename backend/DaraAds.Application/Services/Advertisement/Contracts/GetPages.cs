using System.Collections.Generic;
using System.Linq;
using DaraAds.Application.Common;

namespace DaraAds.Application.Services.Advertisement.Contracts
{
    public static class GetPages
    {
        public sealed class Request
        {
            public int Limit { get; set; } = AdvertisementConstants.PaginationLimit;
            public int Offset { get; set; } = AdvertisementConstants.PaginationOffset;
            public string SortOrder { get; set; } = AdvertisementConstants.SortOrderByIdAsc;
            public string SearchString { get; set; }
            public int CategoryId { get; set; }
        }

        public sealed class Response
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

            public int Total { get; set; }
            public int Offset { get; set; }
            public int Limit { get; set; }

            public IEnumerable<Item> Items { get; set; } = Enumerable.Empty<Item>();
        }
    }
}