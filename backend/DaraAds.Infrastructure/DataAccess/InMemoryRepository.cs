using DaraAds.Application;
using DaraAds.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.Infrastructure.DataAccess
{
    public sealed partial class InMemoryRepository :
        IRepository<Advertisement, int>,
        IRepository<User, int>,
        IRepository<Abuse, int>
    {
        private readonly DaraAdsDbContext _context;

        public InMemoryRepository(DaraAdsDbContext context) 
        {
            _context = context;
        }

    }
}
