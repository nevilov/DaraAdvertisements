using System.Threading;
using System.Threading.Tasks;
using DaraAds.Application.Services.Image.Contracts;
using Microsoft.AspNetCore.Http;

namespace DaraAds.Application.Services.Image.Interfaces
{
    public interface IImageService
    {
        Task<Upload.Response> Upload(Upload.Request request, CancellationToken cancellationToken);
        Task<Get.Response> GetImage(Get.Request request, CancellationToken cancellationToken);
        Task Delete(Delete.Request request, CancellationToken cancellationToken);
    }
}