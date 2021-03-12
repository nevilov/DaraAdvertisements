using DaraAds.Application.Repositories;
using DaraAds.Domain;
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

        public async Task<IEnumerable<Domain.Advertisement>> GetPageByFilterSortSearch(int categoryId, string searchString, string sortOrder, int offset, int limit,
            CancellationToken cancellationToken)
        {
            var ads = from advertisement in _context.Advertisements select advertisement;
            
            // Фильтрация
            if (categoryId != 0)
            {
                ads = ads.Where(a => a.CategoryId == categoryId);    
            }
            
            // Поиск
            if (!string.IsNullOrEmpty(searchString))
            {
                var lowerCaseSearchString = searchString.ToLower();
                
                ads = ads.Where(a =>
                    a.Title.ToLower().Contains(lowerCaseSearchString) ||
                    a.Description.ToLower().Contains(lowerCaseSearchString));
            }
            
            // Сортировка
            var descending = false;
            if (sortOrder.EndsWith("_desc"))
            {
                sortOrder = sortOrder.Substring(0, sortOrder.Length - 5);
                descending = true;
            }
            
            ads = @descending
                ? ads.OrderByDescending(a => EF.Property<object>(a, sortOrder))
                : ads.OrderBy(a => EF.Property<object>(a, sortOrder));

            return await ads.Take(limit).Skip(offset).ToListAsync(cancellationToken);
        }
    }
}
