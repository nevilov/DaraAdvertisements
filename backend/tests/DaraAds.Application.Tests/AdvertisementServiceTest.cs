using AutoFixture.Xunit2;
using DaraAds.Application.Identity.Interfaces;
using DaraAds.Application.Repositories;
using DaraAds.Application.Services.Advertisement.Contracts;
using DaraAds.Application.Services.Advertisement.Implementations;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DaraAds.Application.Tests
{
    public partial class AdvertisementServiceTest
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

        private void ConfigureMoqEnvironment(string userId, int adId)
        {
            _identityServiceMock
                .Setup(_ => _.GetCurrentUserId(It.IsAny<CancellationToken>()))
                .ReturnsAsync(userId)
                .Verifiable();

            _advertisementRepositoryMock
                .Setup(_ => _.Save(It.IsAny<Domain.Advertisement>(), It.IsAny<CancellationToken>()))
                .Callback((Domain.Advertisement ad, CancellationToken cancellationToken) => ad.Id = adId);
        }

    }
}
