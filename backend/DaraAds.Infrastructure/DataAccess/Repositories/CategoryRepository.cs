using DaraAds.Application.Repositories;
using DaraAds.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.Infrastructure.DataAccess.Repositories
{
    public class CategoryRepository : Repository<Category, int>, ICategoryRepository
    {
        public CategoryRepository(DaraAdsDbContext context) : base(context)
        {
        }

        public async Task<Category> FindCategoryById(int id, CancellationToken cancellationToken)
        {
            return await _context.Categories.FindAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<Category>> FindTopCategories(CancellationToken cancellationToken)
        {
            return await _context.Categories
                .Where(c => c.ParentCategory == null)
                .ToListAsync(cancellationToken);
        }
    }
}
