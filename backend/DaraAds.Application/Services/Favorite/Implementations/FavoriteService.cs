using DaraAds.Application.Identity.Interfaces;
using DaraAds.Application.Repositories;
using DaraAds.Application.Services.Favorite.Contracts;
using DaraAds.Application.Services.Favorite.Contracts.Exceptions;
using DaraAds.Application.Services.Favorite.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.Application.Services.Favorite.Implementations
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IRepository<Domain.Favorite, int> _repository;
        private readonly IIdentityService _identityService;
        private readonly IAdvertisementRepository _advertisementService;

        public FavoriteService(IRepository<Domain.Favorite, int> repository,
            IIdentityService identityService,
            IAdvertisementRepository advertisementService)
        {
            _repository = repository;
            _identityService = identityService;
            _advertisementService = advertisementService;
        }

        public async Task<CreateFavorite.Response> CreateFavorite(CreateFavorite.Request request, CancellationToken cancellationToken)
        {
            var userId = await _identityService.GetCurrentUserId(cancellationToken);
            if(string.IsNullOrEmpty(userId))
            {
                throw new UserNotFoundException("Пользователь не найден"); 
            }

            var advertisement = await _advertisementService.FindById(request.AdvertisementId, cancellationToken);
            if(advertisement == null)
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
    }
}
