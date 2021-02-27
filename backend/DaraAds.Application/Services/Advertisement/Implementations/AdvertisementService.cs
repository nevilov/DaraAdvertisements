using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DaraAds.Application.Common;
using DaraAds.Application.Identity.Interfaces;
using DaraAds.Application.Repositories;
using DaraAds.Application.Services.Advertisement.Contracts;
using DaraAds.Application.Services.Advertisement.Contracts.Exeptions;
using DaraAds.Application.Services.Advertisement.Interfaces;
using DaraAds.Application.Services.User.Contracts.Extantions;

namespace DaraAds.Application.Services.Advertisement.Implementations
{
    public sealed class AdvertisementService : IAdvertisementService
    {
        private readonly IAdvertisementRepository _repository;
        private readonly IIdentityService _identityService;

        public AdvertisementService(IAdvertisementRepository repository, IIdentityService identityService)
        {
            _repository = repository;
            _identityService = identityService;
        }


        public async Task<Create.Response> Create(Create.Request request, CancellationToken cancellationToken)
        {
            var userId = await _identityService.GetCurrentUserId(cancellationToken);

            if (string.IsNullOrEmpty(userId))
            {
                throw new NoUserForAdCreationException($"Попытка создания объявления [{request.Title}] без пользователя.");
            }

            var ad = new Domain.Advertisement
            {
                Title = request.Title,
                Description = request.Description,
                Price = request.Price,
                Cover = request.Cover,
                OwnerId = userId,
                Status = Domain.Advertisement.Statuses.Created,
                CreatedDate = DateTime.UtcNow
            };

            await _repository.Save(ad, cancellationToken);
            return new Create.Response
            {
                Id = ad.Id
            };
        }

        public async Task<Get.Response> Get(Get.Request request, CancellationToken cancellationToken)
        {
            var ad = await _repository.FindByIdWithUser(request.Id, cancellationToken);
            if (ad == null)
            {
                throw new NoAdFoundException(request.Id);
            }

            return new Get.Response
            {
                Title = ad.Title,
                Description = ad.Description,
                Status = ad.Status.ToString(),
                Price = ad.Price,
                Cover = ad.Cover,
                
                Owner = new Get.Response.OwnerResponse
                {
                    Id = ad.OwnerUser.Id,
                    Name  = ad.OwnerUser.Name,
                    LastName = ad.OwnerUser.LastName
                }
            };
        }

        public async Task Delete(Delete.Request request, CancellationToken cancellationToken)
        {
            var userId = await _identityService.GetCurrentUserId(cancellationToken);

            if (string.IsNullOrEmpty(userId))
            {
                throw new NoUserFoundException($"Пользователь не найден");
            }

            var ad = await _repository.FindById(request.Id, cancellationToken);

            if (ad == null)
            {
                throw new NoAdFoundException(request.Id);
            }
            
            if (ad.Status != Domain.Advertisement.Statuses.Created)
            {
                throw new AdShouldBeInCreatedStateForClosingException(ad.Id);
            }
            
            var isAdmin = await _identityService.IsInRole(userId, RoleConstants.AdminRole, cancellationToken);

            if (!isAdmin && ad.OwnerId != userId)
            {
                throw new NoRightsException("Нет прав для выполнения операции.");
            }
            
            ad.Status = Domain.Advertisement.Statuses.Closed;
            ad.UpdatedDate = DateTime.UtcNow;
            
            await _repository.Save(ad, cancellationToken);
            
        }

        public async Task<GetPages.Response> GetPages(GetPages.Request request, CancellationToken cancellationToken)
        {
            var total = await _repository.Count(cancellationToken);
            if (total == 0)
            {
                return new GetPages.Response
                {
                    Total = 0,
                    Offset = request.Offset,
                    Limit = request.Limit
                };
            }

            var ads = await _repository.GetPaged(request.Offset, request.Limit, cancellationToken);

            return new GetPages.Response
            {
                Items = ads.Select(a => new GetPages.Response.Item
                {
                    Id = a.Id,
                    Title = a.Title,
                    Description = a.Description,
                    Cover = a.Cover,
                    Price = a.Price,
                    Status = a.Status.ToString()
                }),
                Total = total,
                Offset = request.Offset,
                Limit = request.Limit
            };
        }

        public async Task<Update.Response> Update(Update.Request request, CancellationToken cancellationToken)
        {
            var userId = await _identityService.GetCurrentUserId(cancellationToken);

            if (string.IsNullOrEmpty(userId))
            {
                throw new NoUserFoundException($"Пользователь не найден");
            }

            var advertisement = await _repository.FindByIdWithUser(request.Id, cancellationToken);
            
            if (advertisement == null)
            {
                throw new NoAdFoundException(request.Id);
            }

            var isAdmin = await _identityService.IsInRole(userId, RoleConstants.AdminRole, cancellationToken);

            if (!isAdmin && advertisement.OwnerId != userId)
            {
                throw new NoRightsException("Нет прав для выполнения операции.");
            }
            
            advertisement.Title = request.Title;
            advertisement.Description = request.Description;
            advertisement.Price = request.Price;
            advertisement.Cover = request.Cover;
            advertisement.UpdatedDate = DateTime.UtcNow;
           //Status = (Enum.Parse<Domain.Advertisement.Statuses>(request.Status)).ToString;

            await _repository.Save(advertisement, cancellationToken);

            return new Update.Response
            {
                Id = request.Id
            };    
        }
    }
}