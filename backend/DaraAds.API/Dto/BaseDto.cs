using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaraAds.API.Dto
{
    public class BaseDto<TId>
    {
        public TId Id { get; set; }
    }
}
