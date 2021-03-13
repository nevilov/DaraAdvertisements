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
            var domainUser = await _repository.FindById(request.Id, cancellationToken);
            if (domainUser == null)
            {
                throw new NoUserFoundException("Пользователя не существует");
            }

            var currentUser = await _identity.GetCurrentUserId(cancellationToken);

            if (domainUser.Id != currentUser)
            {
                throw new NoRightsException("Нет прав");
            }

            domainUser.Name = request.Name;
            domainUser.LastName = request.LastName;
            domainUser.Avatar = request.Avatar;
            domainUser.UpdatedDate = DateTime.UtcNow;

            await _repository.Save(domainUser, cancellationToken);
        }
    }
}