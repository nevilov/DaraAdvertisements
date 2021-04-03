using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaraAds.Application.Services.Advertisement.Contracts
{
    public class Paged
    {
        public int Limit { get; set; } = 10;
        public int Offset { get; set; } = 0;
    }
}
