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

        [Theory]
        [AutoData]
        public async Task Create_Response_Success(
            Create.Request request, CancellationToken cancellationToken,
            int userId, int adId)
        {
            ConfigureMoqEnvironment(userId.ToString(), adId);

            // Act
            var response = await advertisementService.Create(request, cancellationToken);

            // Assert
            _identityServiceMock.Verify();
            Assert.NotNull(response);
            Assert.NotEqual(default, response.Id);
        }


        [Theory]
        [InlineAutoData(null)]
        public async Task Throw_Exception_If_Request_Failure(
            Create.Request request, CancellationToken cancellationToken,
            int userId, int adId)
        {
            ConfigureMoqEnvironment(userId.ToString(), adId);

            // Assert
            await Assert.ThrowsAsync<NullReferenceException>(async () => await advertisementService.Create(request, cancellationToken));
            _identityServiceMock.Verify();
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
