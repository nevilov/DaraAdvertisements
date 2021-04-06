using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaraAds.Application.Services.Favorite.Contracts
{
    public static class RemoveFromFavorite
    {
        public class Request
        {
            public int AdvertisementId { get; set; }
        }
    }
}
