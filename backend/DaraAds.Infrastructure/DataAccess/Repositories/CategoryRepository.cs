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


        public async Task<IEnumerable<Category>> FindTopCategories(CancellationToken cancellationToken)
        {
            return await _context.Categories
                .Where(c => c.ParentCategory == null)
                .ToListAsync(cancellationToken);
        }


        public async Task<List<int>> FindCategoryIdsByParent(int id, CancellationToken cancellationToken)
        {
            List<int> Ids = new List<int>();
            var category = await _context.Categories
                                   .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            Ids.Add(category.Id);

            for (var i = 0; i < category.ChildCategories.Count; i++)
            {
                if (category.ChildCategories[i] != null)
                    Ids.AddRange(await FindCategoryIdsByParent(category.ChildCategories[i].Id, cancellationToken));
            }

            return Ids;
        }
    }
}
