using DaraAds.Application.Services.Category.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.Application.Services.Category.Interfaces
{
    public interface ICategoryService
    {
        Task<GetCategoryById.Response> GetCategoryById(GetCategoryById.Request request, CancellationToken cancellationToken);

        Task<GetTopCategories.Response> GetTopCategories(CancellationToken cancellationToken);
    }
}
