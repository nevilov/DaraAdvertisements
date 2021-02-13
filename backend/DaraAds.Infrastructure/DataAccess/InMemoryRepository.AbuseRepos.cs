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
        //ABUSE REPOSITORY BEGIN
        Task<Abuse> IRepository<Abuse, int>.FindById(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task Save(Abuse entity, CancellationToken cancellationToken)
        {
            if (entity.Id == 0)
            {
                entity.Id = Guid.NewGuid().GetHashCode();
            }
            await _context.SaveChangesAsync();
        }

        public async Task Add(Abuse entity, CancellationToken cancellationToken)
        {
            if (entity.Id == 0)
            {
                entity.Id = Guid.NewGuid().GetHashCode();
            }

            _context.Abuses.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Abuse> FindWhere(Expression<Func<Abuse, bool>> predicate, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        //ABUSE GET PAGE & COUNT BEGIN

        async Task<IEnumerable<Abuse>> IRepository<Abuse, int>.GetPaged(int offset, int limit, CancellationToken cancellationToken)
        {
            return await _context.Abuses
           .OrderBy(u => u.Id)
           .Skip(offset)
           .Take(limit)
           .ToListAsync();
        }

        //ABUSE REPOSITORY END
    }

}
