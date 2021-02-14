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
        public async Task<Advertisement> FindById(int id, CancellationToken cancellationToken)
        {
            var ad = await _context.Advertisements.FindAsync(id);
            if (ad == null)
            {
                return null;
            }
            var user = await _context.Users.FindAsync(ad.UserId);

            ad.OwnerUser = user;
            return ad;
        }

        public async Task<Advertisement> FindWhere(Expression<Func<Advertisement, bool>> predicate, CancellationToken cancellationToken)
        {
            var compiled = predicate.Compile();
            return _context.Advertisements.Where(compiled).FirstOrDefault();
        }

        public async Task<IEnumerable<Advertisement>> GetPaged(int offset, int limit, CancellationToken cancellationToken)
        {
            return await _context.Advertisements
                .OrderBy(u => u.Id)
                .Skip(offset)
                .Take(limit)
                .ToListAsync();
        }

        public async Task Save(Advertisement entity, CancellationToken cancellationToken)
        {
            if (entity.Id == 0)
            {
                entity.Id = _context.Advertisements.Count() + 1;
            }

            var entry = _context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                await _context.Advertisements.AddAsync(entity, cancellationToken);
            }

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> Count(CancellationToken cancellationToken)
        {
            var cnt = _context.Advertisements.Count();
            return cnt;
        }


    }
}
