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
    public sealed class InMemoryRepository :
        IRepository<Advertisement, int>,
        IRepository<User, int>
    {
        private readonly DaraAdsDbContext _context;

        public InMemoryRepository(DaraAdsDbContext context) 
        {
            _context = context;
        }

        public async Task<int> Count(CancellationToken cancellationToken)
        {
            var cnt = _context.Advertisements.Count();
            return cnt;
        }

        public async Task<Advertisement> FindById(int id, CancellationToken cancellationToken)
        {
            var ad = await _context.Advertisements.FindAsync(id);
            var user = await _context.Users.FindAsync(ad.UserId);
            ad.OwnerUser = user;
            return ad;
        }

        public async Task<Advertisement> FindWhere(Expression<Func<Advertisement, bool>> predicate, CancellationToken cancellationToken)
        {
            var compiled = predicate.Compile();
            return _context.Advertisements.Where(compiled).FirstOrDefault();
        }

        public async Task<User> FindWhere(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken)
        {
            var compiled = predicate.Compile();
            return _context.Users.Where(compiled).FirstOrDefault();
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
                entity.Id = Guid.NewGuid().GetHashCode();
            }

            _context.Advertisements.Add(entity);
            await _context.SaveChangesAsync();

        }

        public async Task Save(User entity, CancellationToken cancellationToken)
        {
            if (entity.Id == 0)
            {
                entity.Id = Guid.NewGuid().GetHashCode();
            }

            _context.Users.Add(entity);
            await _context.SaveChangesAsync();
        }

        async Task<User> IRepository<User, int>.FindById(int id, CancellationToken cancellationToken)
        {
            return await _context.Users.FindAsync(id);
        }

        async Task<IEnumerable<User>> IRepository<User, int>.GetPaged(int offset, int limit, CancellationToken cancellationToken)
        {
            return await _context.Users
                .OrderBy(u => u.Id)
                .Skip(offset)
                .Take(limit)
                .ToListAsync();
        }

        async Task<int> IRepository<User, int>.Count(CancellationToken cancellationToken)
        {
            return _context.Users.Count();
        }
    }
}
