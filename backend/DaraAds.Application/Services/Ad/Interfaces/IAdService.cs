using System.Threading;
using System.Threading.Tasks;
using DaraAds.Application.Services.Ad.Contracts;

namespace DaraAds.Application.Services.Ad.Interfaces
{
    public interface IAdService
    {
        Task<Create.Response> Create(Create.Request request, CancellationToken cancellationToken);

        Task<Get.Response> Get(Get.Request request, CancellationToken cancellationToken);

        Task Delete(Delete.Request request, CancellationToken cancellationToken);

        Task<Update.Response> Update(Update.Request request, CancellationToken cancellationToken);
        
        Task<GetPage.Response> GetPaged(GetPage.Request request, CancellationToken cancellationToken);
    }
}