using DaraAds.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.Infrastructure.DataAccess.Repositories
{
    public class AdvertisementRepository : Repository<Domain.Advertisement, int>, IAdvertisementRepository
    {
        public AdvertisementRepository(DaraAdsDbContext context): base(context) { }

        public async Task<IEnumerable<Domain.Advertisement>> FindByCategory(int id, int limit, int offset, CancellationToken cancellationToken)
        {
            return await _context.Advertisements.Where(e => e.CategoryId == id).Take(limit).Skip(offset).ToListAsync(cancellationToken);
        }

        public async Task<Domain.Advertisement> FindByIdWithUser(int id, CancellationToken cancellationToken)
        {
            return await _context.Advertisements
                .Include(a => a.OwnerUser)
                .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Domain.Advertisement>> Search(Expression<Func<Domain.Advertisement, bool>> predicate, int limit, int offset, CancellationToken cancellationToken)
        {
            return await _context.Advertisements
                .Where(predicate)
                .OrderBy(x => x.Id)
                .Take(limit)
                .Skip(offset)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Domain.Advertisement>> FindUserAdvertisements(string userId, int limit, int offset, CancellationToken cancellationToken)
        {
            return await _context.Advertisements
                .Where(x => x.OwnerId == userId)
                .OrderBy(x => x.CreatedDate)
                .Take(limit)
                .Skip(offset)
                .ToListAsync(cancellationToken);
        }
    }
}
