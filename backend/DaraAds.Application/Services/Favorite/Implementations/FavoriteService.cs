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
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly IIdentityService _identityService;

        public FavoriteService(IFavoriteRepository repository,
            IIdentityService identityService,
            IAdvertisementRepository advertisementRepository)
        {
            _repository = repository;
            _identityService = identityService;
            _advertisementRepository = advertisementRepository;
        }

        public async Task<CreateFavorite.Response> AddToFavorite(CreateFavorite.Request request, CancellationToken cancellationToken)
        {
            var userId = await _identityService.GetCurrentUserId(cancellationToken);
            if (string.IsNullOrEmpty(userId))
            {
                throw new UserNotFoundException("Пользователь не найден"); 
            }

            var advertisement = await _advertisementRepository.FindById(request.AdvertisementId, cancellationToken);
            if (advertisement == null)
            {
                throw new AdvertisementNotFoundException($"Объявление с Id = {request.AdvertisementId} не было найдено");
            }

            var advertisementInFavoriteCurrentUser = await _repository.FindWhere(a => a.UserId == userId && a.AdvertisementId == request.AdvertisementId, cancellationToken);
            if (advertisementInFavoriteCurrentUser != null)
            {
                throw new DuplicateFavoriteException($"Объявление с id = {request.AdvertisementId} уже содержится в избранных пользователя");
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

        public async Task<GetFavorites.Response> GetFavorites(GetFavorites.Request request, CancellationToken cancellationToken)
        {
            var userId = await _identityService.GetCurrentUserId(cancellationToken);
            if (string.IsNullOrEmpty(userId))
            {
                throw new UserNotFoundException("Пользователь не найден");
            }

            var total = await _repository.Count(a => a.UserId == userId, cancellationToken);
            if (total == 0)
            {
                return new GetFavorites.Response
                {
                    Total = 0,
                    Offset = request.Offset,
                    Limit = request.Limit
                };
            }

            var items = await _repository.FindFavorites(userId, request.Offset, request.Limit, request.SortBy, request.SortDirection, cancellationToken);

            return new GetFavorites.Response
            {
                Items = items.Select(a => new GetFavorites.Item
                {
                    Id = a.AdvertisementId,
                    Title = a.Advertisement.Title,
                    Description = a.Advertisement.Description,
                    CreatedDate = a.Advertisement.CreatedDate,
                    Price = a.Advertisement.Price,
                    Status = a.Advertisement.Status.ToString(),
                    Cover = a.Advertisement.Cover,
                    Images = a.Advertisement.Images.Select(i => new GetFavorites.ImageResponse
                    {
                        Id = i.Id,
                    }),
                    Category = new GetFavorites.CategoryResponse
                    {
                        Id = a.Advertisement.Category.Id,
                        Name = a.Advertisement.Category.Name
                    },
                    Location = a.Advertisement.Location
                }),
                Total = items.Total,
                Limit = request.Limit,
                Offset = request.Offset
            };
        }

        public async Task RemoveFromFavorite(RemoveFromFavorite.Request request, CancellationToken cancellationToken)
        {
            var userId = await _identityService.GetCurrentUserId(cancellationToken);
            if (string.IsNullOrEmpty(userId))
            {
                throw new UserNotFoundException("Пользователь не найден");
            }

            var advertisement = await _advertisementRepository.FindById(request.AdvertisementId, cancellationToken);
            if (advertisement == null)
            {
                throw new AdvertisementNotFoundException($"Объявление с Id = {request.AdvertisementId} не было найдено");
            }

            var favorite = await _repository.FindWhere(a => a.AdvertisementId == request.AdvertisementId && a.UserId == userId, cancellationToken);
            if(favorite == null)
            {
                throw new FavoriteNotFoundException($"Избранное объявление id = {request.AdvertisementId} у пользователя с id {userId} не было найдено");
            }

            await _repository.Delete(favorite, cancellationToken);
        }
    }
}
