using DaraAds.Application.Identity.Interfaces;
using DaraAds.Application.Repositories;
using DaraAds.Application.Services.Favorite.Contracts;
using DaraAds.Application.Services.Favorite.Contracts.Exceptions;
using DaraAds.Application.Services.Favorite.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.Application.Services.Favorite.Implementations
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IFavoriteRepository _repository;
        private readonly IAdvertisementRepository _advertisementService;
        private readonly IIdentityService _identityService;

        public FavoriteService(IFavoriteRepository repository,
            IIdentityService identityService,
            IAdvertisementRepository advertisementService)
        {
            _repository = repository;
            _identityService = identityService;
            _advertisementService = advertisementService;
        }

        public async Task<CreateFavorite.Response> AddToFavorite(CreateFavorite.Request request, CancellationToken cancellationToken)
        {
            var userId = await _identityService.GetCurrentUserId(cancellationToken);
            if (string.IsNullOrEmpty(userId))
            {
                throw new UserNotFoundException("Пользователь не найден"); 
            }

            var advertisementInFavoriteCurrentUser = await _repository.FindWhere(a => a.UserId == userId && a.AdvertisementId == request.AdvertisementId, cancellationToken);
            if (advertisementInFavoriteCurrentUser != null)
            {
                throw new DuplicateFavoriteException($"Объявление с id = {request.AdvertisementId} уже содержится в избранных пользователя");
            }

            var advertisement = await _advertisementService.FindById(request.AdvertisementId, cancellationToken);
            if (advertisement == null)
            {
                throw new AdvertisementNotFoundException($"Объявление с Id = {request.AdvertisementId} не было найдено");
            }

            var favorite = new Domain.Favorite
            {
                AdvertisementId = advertisement.Id,
                UserId = userId,
                CreatedDate = DateTime.UtcNow
            };

            await _repository.Save(favorite, cancellationToken);

            return new CreateFavorite.Response
            {
                Id = favorite.Id
            };
        }

        public async Task<GetFavorites.Reponse> GetFavorites(GetFavorites.Request request, CancellationToken cancellationToken)
        {
            var userId = await _identityService.GetCurrentUserId(cancellationToken);

            var total = await _repository.Count(a => a.UserId == userId, cancellationToken);
            if (total == 0)
            {
                return new GetFavorites.Reponse
                {
                    Total = 0,
                    Offset = request.Offset,
                    Limit = request.Limit
                };
            }

            var items = await _repository.FindFavorites(userId, 0, 100, cancellationToken);

            return new GetFavorites.Reponse
            {
                Items = items.Select(a => new GetFavorites.Item
                {
                    Title = a.Advertisement.Title,
                    Description = a.Advertisement.Description,
                    CreatedDate = a.Advertisement.CreatedDate,
                    Price = a.Advertisement.Price,
                    Status = a.Advertisement.Status.ToString(),
                    Cover = a.Advertisement.Cover         
                }),
                Total = total,
                Limit = request.Limit,
                Offset = request.Offset
            };
        }
    }
}
