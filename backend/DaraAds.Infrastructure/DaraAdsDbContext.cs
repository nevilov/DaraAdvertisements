using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaraAds.Infrastructure
{
    public class DaraAdsDbContext: DbContext
    {
        public DaraAdsDbContext(DbContextOptions<DaraAdsDbContext> options) : base(options) { }

    }
}
