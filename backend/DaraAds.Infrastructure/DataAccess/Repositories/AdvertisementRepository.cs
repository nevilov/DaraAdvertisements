using DaraAds.Application.Repositories;
using DaraAds.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.Infrastructure.DataAccess.Repositories
{
    public class AdvertisementRepository : Repository<Domain.Advertisement, int>, IAdvertisementRepository
    {
        public AdvertisementRepository(DaraAdsDbContext context): base(context) { }

        public async Task<Domain.Advertisement> FindByIdWithUser(int id, CancellationToken cancellationToken)
        {
            return await _context.Advertisements
                .Include(a => a.OwnerUser)
                .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        }

        public async Task<Domain.Advertisement> FindByIdWithUserAndCategory(int id, CancellationToken cancellationToken)
        {
            return await _context.Advertisements
                .Include(a => a.OwnerUser)
                .Include(a => a.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);

        }
    }
}
