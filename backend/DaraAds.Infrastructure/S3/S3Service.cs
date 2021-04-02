using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using DaraAds.Application.Services.S3.Contracts.Exceptions;
using DaraAds.Application.Services.S3.Interfaces;

namespace DaraAds.Infrastructure.S3
{
    public class S3Service : IS3Service
    {
        
        private readonly IAmazonS3 _s3Client;  
        private readonly S3BucketSettings _settings;

        public S3Service(IAmazonS3 s3Client, S3BucketSettings settings)
        {
            _s3Client = s3Client;
            _settings = settings;
        }

        public async Task<bool> UploadFile(Stream uploadFile, string fileName, CancellationToken cancellationToken = default)
        {
            try
            {
                var fileTransferUtility = new TransferUtility(_s3Client);
                var fileUploadRequest = new TransferUtilityUploadRequest()
                {
                    BucketName = _settings.BucketName,
                    Key = fileName,
                    InputStream = uploadFile
                };
                await fileTransferUtility.UploadAsync(fileUploadRequest, cancellationToken);

                return true;
            }
            catch (Exception e)
            {
                throw new SendingImageException("При загрузке произошла ошибка!" + e.Message);
            }
        }

        public async Task<bool> DeleteFile(string fileName, CancellationToken cancellationToken = default)
        {
            try
            {
                var fileTransferUtility = new TransferUtility(_s3Client);
                var fileDeleteRequest = new DeleteObjectRequest()
                {
                    BucketName = _settings.BucketName,
                    Key = fileName,
                };
                await fileTransferUtility.S3Client.DeleteObjectAsync(fileDeleteRequest, cancellationToken);
                
                return true;
            }
            catch (Exception e)
            {
                throw new DeletingImageException("При загрузке произошла ошибка!" + e.Message);
            }
        }
    }
}