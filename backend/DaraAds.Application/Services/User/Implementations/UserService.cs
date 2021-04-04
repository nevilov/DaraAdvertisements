using DaraAds.Application.Repositories;
using System;
using DaraAds.Application.Services.User.Contracts;
using DaraAds.Application.Services.User.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using DaraAds.Application.Common;
using DaraAds.Application.Identity.Contracts;
using DaraAds.Application.Identity.Interfaces;
using DaraAds.Application.Services.Image.Contracts;
using DaraAds.Application.Services.Image.Interfaces;
using DaraAds.Application.Services.S3.Interfaces;
using DaraAds.Application.Services.User.Contracts.Exceptions;

namespace DaraAds.Application.Services.User.Implementations
{
    public sealed class UserService : IUserService
    {
        private readonly IRepository<Domain.User, string> _repository;
        private readonly IRepository<Domain.Image, string> _imageRepository;
        private readonly IIdentityService _identity;
        private readonly IImageService _imageService;
        private readonly IS3Service _s3Service;

        public UserService(
            IRepository<Domain.User, string> repository,
            IIdentityService identity, 
            IImageService imageService,
            IS3Service s3Service,
            IRepository<Domain.Image, string> imageRepository)
        {
            _repository = repository;
            _identity = identity;
            _imageService = imageService;
            _s3Service = s3Service;
            _imageRepository = imageRepository;
        }

        public async Task<Register.Response> Register(Register.Request request, CancellationToken cancellationToken)
        {
            //TODO Проверка на дубликаты
            var response = await _identity.CreateUser(new CreateUser.Request
            {
                Username = request.Username,
                Email = request.Email,
                Password = request.Password,
                Role = RoleConstants.UserRole
            }, cancellationToken);

            if (response.IsSuccess)
            {
                var domainUser = new Domain.User
                {
                    Id = response.UserId,
                    Username = request.Username,
                    Name = request.Name,
                    LastName = request.LastName,
                    Email = request.Email,
                    CreatedDate = DateTime.UtcNow
                };

                await _repository.Save(domainUser, cancellationToken);

                return new Register.Response
                {
                    UserId = response.UserId
                };
            }

            throw new UserRegisterException(string.Join(",", response.Errors));
        }

        public async Task Update(Update.Request request, CancellationToken cancellationToken)
        {
            var currentUserId = await _identity.GetCurrentUserId(cancellationToken);
            var domainUser = await _repository.FindById(currentUserId, cancellationToken);

            if (domainUser == null)
            {
                throw new NoUserFoundException("Пользователя не существует");
            }
            
            domainUser.Name = request.Name;
            domainUser.LastName = request.LastName;
            domainUser.UpdatedDate = DateTime.UtcNow;
            domainUser.Phone = request.Phone;

            if (request.Avatar != null)
            {
                domainUser.Avatar = request.Avatar;
            }

            await _repository.Save(domainUser, cancellationToken);
        }

        public async Task AddImage(AddImage.Request request, CancellationToken cancellationToken)
        {
            var userId = await _identity.GetCurrentUserId(cancellationToken);

            if (string.IsNullOrEmpty(userId))
            {
                throw new NoUserFoundException($"Пользователь не найден");
            }

            var response = await _imageService.Upload(
                new Upload.Request
                {
                    Image = request.Image
                }, cancellationToken);

            var image = await _imageRepository.FindById(response.Id, cancellationToken);
            
            var user = await _repository.FindById(userId, cancellationToken);
            
            user.Images.Add(image);
            
            await _repository.Save(user, cancellationToken);
        }

        public async Task DeleteImage(DeleteImage.Request request, CancellationToken cancellationToken)
        {
            var userId = await _identity.GetCurrentUserId(cancellationToken);

            if (string.IsNullOrEmpty(userId))
            {
                throw new NoUserFoundException($"Пользователь не найден");
            }

            var user = await _repository.FindById(userId, cancellationToken);

            var image = await _imageRepository.FindById(request.ImageId, cancellationToken);
            
            user.Images.Remove(image);
            // TODO добавить проверки на успешное удаление
            await _s3Service.DeleteFile(image.Name, cancellationToken);
            
            await _repository.Save(user, cancellationToken);
        }
    }
}