using DaraAds.Application.Repositories;
using System;
using DaraAds.Application.Services.User.Contracts;
using DaraAds.Application.Services.User.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using DaraAds.Application.Common;
using DaraAds.Application.Identity.Contracts;
using DaraAds.Application.Identity.Interfaces;
using DaraAds.Application.Services.User.Contracts.Exceptions;

namespace DaraAds.Application.Services.User.Implementations
{
    public sealed class UserService : IUserService
    {
        private readonly IRepository<Domain.User, string> _repository;
        private readonly IIdentityService _identity;

        public UserService(IRepository<Domain.User, string> repository, IIdentityService identity)
        {
            _repository = repository;
            _identity = identity;
        }

        public async Task<Register.Response> Register(Register.Request request, CancellationToken cancellationToken)
        {
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

        public async Task<Get.Response> GetUser(Get.Request request, CancellationToken cancellationToken)
        {
            string userId;

            if (string.IsNullOrEmpty(request.Id))
            {
                var userIdFromClaims = await _identity.GetCurrentUserId(cancellationToken);
                if (string.IsNullOrEmpty(userIdFromClaims))
                {
                    throw new NoUserFoundException("Пользователь не найден");
                }
                userId = userIdFromClaims;
            }
            else
            {
                userId = request.Id;
            }

            var user = await _repository.FindById(userId, cancellationToken);

            if(user == null)
            {
                throw new NoUserFoundException("Пользователь не найден");
            }
            return new Get.Response
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Lastname = user.LastName,
                Avatar = user.Avatar,
                Phone = user.Phone,
                Username = user.Username
            };
        }
    }
}