using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using DaraAds.Application.Repositories;
using DaraAds.Application.Services.Image.Contracts;
using DaraAds.Application.Services.Image.Contracts.Exceptions;
using DaraAds.Application.Services.Image.Interfaces;
using DaraAds.Application.Services.S3.Interfaces;

namespace DaraAds.Application.Services.Image.Implementations
{
    public sealed class ImageService : IImageService
    {

        private readonly IRepository<Domain.Image, string> _repository;
        private readonly IS3Service _s3Service;

        public ImageService(IRepository<Domain.Image, string> repository, IS3Service s3Service)
        {
            _repository = repository;
            _s3Service = s3Service;
        }

        public async Task<UploadImage.Response> Upload(UploadImage.Request request, CancellationToken cancellationToken)
        {

            if (request.Image.Length <= 0)
            {
                throw new ImageInvalidException("Изображение повреждено загрузка невозможна");
            }

            var imageType = request.Image.ContentType;
            
            if (imageType != "image/jpg" 
                && imageType != "image/jpeg"
                && imageType != "image/pjpeg"
                && imageType != "image/gif"
                && imageType != "image/x-png"
                && imageType != "image/png")
            {
                throw new ImageInvalidException("Недопустимый формат изображения");
            }
            
            var imageName = Guid.NewGuid() + request.Image.FileName;
            
            await using var memoryStream = new MemoryStream();
            
            await request.Image.CopyToAsync(memoryStream, cancellationToken);

            var response = await _s3Service.UploadFile(memoryStream, imageName, cancellationToken);

            if (!response) throw new ImageInvalidException("При загрузке произошла ошибка!");
           
            var image = new Domain.Image 
            {
                Id = Guid.NewGuid().ToString(),
                Name = imageName,
                ImageBlob = memoryStream.ToArray(),
                CreatedDate = DateTime.UtcNow
            };
               
            await _repository.Save(image, cancellationToken);
            
            return new UploadImage.Response
            {
                Id = image.Id
            };
        }
      

        public async Task<GetImage.Response> GetImage(GetImage.Request request, CancellationToken cancellationToken)
        {
            if (request.Id == "default")
            {
                var defaultImage = "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";
                return new GetImage.Response
                {
                    ImageUrl = "",
                    ImageBlob = defaultImage
                };
            }

            var image = await _repository.FindById(request.Id, cancellationToken);
            
            if (image == null)
            {
                throw new ImageNotFoundException();
            }

            var imageUrl = $"https://storage.yandexcloud.net/dara-ads-images/{image.Name}";

            return new GetImage.Response
            {
                ImageUrl = imageUrl,
                ImageBlob = Convert.ToBase64String(image.ImageBlob) 
            };
        }

        public async Task Delete(DeleteImage.Request request, CancellationToken cancellationToken)
        {
            var image = await _repository.FindById(request.Id, cancellationToken);

            await _s3Service.DeleteFile(image.Name, cancellationToken);

            await _repository.Delete(image, cancellationToken);
        }
    }
}