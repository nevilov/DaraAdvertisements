using System.IO;
using System.Threading;
using System.Threading.Tasks;
using DaraAds.Application.Repositories;
using DaraAds.Application.Services.Advertisement.Contracts;

namespace DaraAds.Application.Services.Advertisement.Interfaces
{
    public interface IAdvertisementService
    {
        Task<Create.Response> Create(Create.Request request, CancellationToken cancellationToken);

        Task<Get.Response> Get(Get.Request request, CancellationToken cancellationToken);

        Task Delete(Delete.Request request, CancellationToken cancellationToken);

        Task<Update.Response> Update(Update.Request request, CancellationToken cancellationToken);
        
        Task<GetPages.Response> GetPages(GetPages.Request request, CancellationToken cancellationToken);

        Task<GetPagedByCategory.Response> GetPagedByCategory(GetPagedByCategory.Request request, CancellationToken cancellationToken);

        Task<Search.Response> Search(Search.Request request, CancellationToken cancellationToken);

        Task<GetUserAdvertisements.Response> GetUserAdvertisements(GetUserAdvertisements.Request request,CancellationToken cancellationToken);
        
        Task AddImage(AddImage.Request request, CancellationToken cancellationToken);

        Task DeleteImage(DeleteImage.Request request, CancellationToken cancellationToken);

        Task ImportExcelProducer(Stream excelFileStream, CancellationToken cancellationToken);

        Task CreateByExcelConsumer(ImportExcelMessage message, CancellationToken cancellationToken);
    }
}