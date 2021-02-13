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
        public async Task<User> FindWhere(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken)
        {
            var compiled = predicate.Compile();
            return _context.Users.Where(compiled).FirstOrDefault();
        }

        public async Task Save(User entity, CancellationToken cancellationToken)
        {
            if (entity.Id == 0)
            {
                entity.Id = Guid.NewGuid().GetHashCode();
            }

            //_context.Users.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Add(User entity, CancellationToken cancellationToken)
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
