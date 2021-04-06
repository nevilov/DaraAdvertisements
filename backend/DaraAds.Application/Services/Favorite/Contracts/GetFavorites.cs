using DaraAds.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DaraAds.Application.Services.Favorite.Contracts
{
    public static class GetFavorites
    {
        public class Request : Paged.Request
        {

        }

        public class Reponse : Paged.Response<Item>
        {

        }

        public class Item
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Cover { get; set; }
            public decimal Price { get; set; }
            public string Status { get; set; }
            public DateTime CreatedDate { get; set; }
        }
    }
}
