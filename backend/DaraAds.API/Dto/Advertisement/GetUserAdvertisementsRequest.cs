using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaraAds.API.Dto.Advertisement
{
    public class GetUserAdvertisementsRequest : PagedDto
    {
        public string Id { get; set; }
    }
}