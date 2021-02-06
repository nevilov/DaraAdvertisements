using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DaraAds.Application.Services.Ad.Contracts;
using DaraAds.Application.Services.Ad.Contracts.Exeptions;
using DaraAds.Application.Services.Ad.Interfaces;
using DaraAds.Application.Services.User.Interfaces;
using DaraAds.Domain;

namespace DaraAds.Application.Services.Ad.Implementations
{
    public sealed class AdServiceV1 : IAdService
    {
        private readonly IRepository<Advertisement, int> _repository;
        private readonly IUserService _userService;

        public AdServiceV1(IRepository<Advertisement, int> repository, IUserService userService)
        {
            _repository = repository;
            _userService = userService;
        }


        public async Task<Create.Response> Create(Create.Request request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetCurrent(cancellationToken);

            if (user == null)
            {
                throw new NoUserForAdCreationException($"Попытка создания объявления [{request.Title}] без пользователя.");
            }

            var ad = new Advertisement
            {
                Title = request.Title,
                Description = request.Description,
                Price = request.Price,
                Cover = request.Cover,
                UserId = user.Id,
                Status = Advertisement.Statuses.Created,
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
            var ad = await _repository.FindById(request.Id, cancellationToken);
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
            var ad = await _repository.FindById(request.Id, cancellationToken);
            if (ad == null)
            {
                throw new NoAdFoundException(request.Id);
            }

            if (ad.Status != Advertisement.Statuses.Created)
            {
                throw new AdShouldBeInCreatedStateForClosingException(ad.Id);
            }

            ad.Status = Advertisement.Statuses.Closed;
            ad.UpdatedDate = DateTime.UtcNow;

            await _repository.Save(ad, cancellationToken);
        }

        public async Task<GetPaged.Response> GetPaged(GetPaged.Request request, CancellationToken cancellationToken)
        {
            var total = await _repository.Count(cancellationToken);
            if (total == 0)
            {
                return new GetPaged.Response
                {
                    Total = 0,
                    Offset = request.Offset,
                    Limit = request.Limit
                };
            }

            var ads = await _repository.GetPaged(request.Offset, request.Limit, cancellationToken);

            return new GetPaged.Response
            {
                Items = ads.Select(a => new GetPaged.Response.Item
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
            throw new System.NotImplementedException();
        }
    }
}