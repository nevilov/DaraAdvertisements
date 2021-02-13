using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaraAds.Domain;

namespace DaraAds.Infrastructure
{
    public class DaraAdsDbContext : DbContext
    {
        public DaraAdsDbContext(DbContextOptions<DaraAdsDbContext> options) : base(options)
        {
        }

        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Abuse> Abuses { get; set; }

    }
}
