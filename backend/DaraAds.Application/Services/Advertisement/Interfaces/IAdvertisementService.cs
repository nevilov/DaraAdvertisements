using System.Threading;
using System.Threading.Tasks;
using DaraAds.Application.Services.Advertisement.Contracts;

namespace DaraAds.Application.Services.Advertisement.Interfaces
{
    public interface IAdvertisementService
    {
        Task<Create.Response> Create(Create.Request request, CancellationToken cancellationToken);

        Task<Get.Response> Get(Get.Request request, CancellationToken cancellationToken);

        Task Delete(Delete.Request request, CancellationToken cancellationToken);

        Task Update(Update.Request request, CancellationToken cancellationToken);
        
        Task<GetPages.Response> GetPages(GetPages.Request request, CancellationToken cancellationToken);
    }
}