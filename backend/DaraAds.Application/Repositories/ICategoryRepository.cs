using DaraAds.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.Application.Repositories
{
    public interface ICategoryRepository : IRepository<Category, int>
    {
        Task<IEnumerable<Category>> FindTopCategories(CancellationToken cancellationToken);

        Task<List<int>> FindCategoryIdsByParent(int id, CancellationToken cancellationToken);
    }
}
