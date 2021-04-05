using System;
using System.Collections.Generic;
using System.Linq;

namespace DaraAds.Application.Services.Favorite.Contracts
{
    public static class GetFavorites
    {

        public class Reponse
        {
            public class Item
            {
                public string Title { get; set; }
                public string Description { get; set; }
                public string Cover { get; set; }
                public decimal Price { get; set; }
                public string Status { get; set; }
                public DateTime CreatedDate { get; set; } 
            }

            public IEnumerable<Item> Items { get; set;} = Enumerable.Empty<Item>();
        }
    }
}
