using DaraAds.Application.Services.Category.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.Application.Services.Category.Interfaces
{
    public interface ICategoryService
    {
        Task<GetChildCategories.Response> GetChildCategories(GetChildCategories.Request request, CancellationToken cancellationToken);
    }
}
