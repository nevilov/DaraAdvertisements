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

        public async Task<Upload.Response> Upload(Upload.Request request, CancellationToken cancellationToken)
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
            
            return new Upload.Response
            {
                Id = image.Id
            };
        }
      

        public async Task<Get.Response> GetImageUrl(Get.Request request, CancellationToken cancellationToken)
        {
            var image = await _repository.FindById(request.Id, cancellationToken);
            
            if (image == null)
            {
                throw new ImageNotFoundException();
            }

            var imageUrl = $"https://storage.yandexcloud.net/dara-ads-images/{image.Name}";

            return new Get.Response
            {
                ImageUrl = imageUrl
            };
        }

        public async Task Delete(Delete.Request request, CancellationToken cancellationToken)
        {
            var image = await _repository.FindById(request.Id, cancellationToken);

            var isDeleted = await _s3Service.DeleteFile(image.Name, cancellationToken);

            if (isDeleted)
            {
                await _repository.Delete(image, cancellationToken);
            }
            
            //TODO Добавить обработку ошибки удаления
        }
    }
}