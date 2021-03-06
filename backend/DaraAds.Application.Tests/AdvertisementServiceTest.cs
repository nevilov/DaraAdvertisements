using DaraAds.Application.Identity.Interfaces;
using DaraAds.Application.Repositories;
using DaraAds.Application.Services.Advertisement.Implementations;
using Moq;

namespace DaraAds.Application.Tests
{
    public class AdvertisementServiceTest
    {
        private Mock<IAdvertisementRepository> _advertisementRepositoryMock;
        private Mock<IIdentityService> _identityServiceMock;

        private AdvertisementService advertisementService;
        public AdvertisementServiceTest()
        {
            _advertisementRepositoryMock = new Mock<IAdvertisementRepository>();
            _identityServiceMock = new Mock<IIdentityService>();

            advertisementService = new AdvertisementService(_advertisementRepositoryMock.Object, _identityServiceMock.Object);
        }

    }
}
