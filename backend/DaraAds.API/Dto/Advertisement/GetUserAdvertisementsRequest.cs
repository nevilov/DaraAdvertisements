using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaraAds.API.Dto.Advertisement
{
    public class GetUserAdvertisementsRequest
    {
        public int Limit { get; set; } = 10;

        public int Offset { get; set; } = 0;
    }
}
