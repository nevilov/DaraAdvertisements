using System.Threading;
using System.Threading.Tasks;
using DaraAds.Application.Services.Image.Contracts;
using Microsoft.AspNetCore.Http;

namespace DaraAds.Application.Services.Image.Interfaces
{
    public interface IImageService
    {
        Task<UploadImage.Response> Upload(UploadImage.Request request, CancellationToken cancellationToken);
        Task<GetImage.Response> GetImage(GetImage.Request request, CancellationToken cancellationToken);
        Task Delete(DeleteImage.Request request, CancellationToken cancellationToken);
    }
}