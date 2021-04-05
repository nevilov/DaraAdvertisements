using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.Application.Services.S3.Interfaces
{
    public interface IS3Service
    {
        Task<bool> UploadFile(Stream uploadFile, string fileName, CancellationToken cancellationToken = default);
        Task<bool> DeleteFile(string fileName, CancellationToken cancellationToken = default);  
    }
}