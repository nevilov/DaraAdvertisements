using AutoFixture.Xunit2;
using DaraAds.Application.Services.Advertisement.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DaraAds.Application.Tests
{
    public partial class AdvertisementServiceTest
    {
        [Theory]
        [AutoData]
        public async Task GetById_Response_Success(
            Get.Request request, CancellationToken cancellationToken,
             int userId, int adId)
        {
            ConfigureMoqEnvironment(userId.ToString(), adId);

            // Act
            var response = await advertisementService.Get(request, cancellationToken);

            // Assert
            _identityServiceMock.Verify();
            Assert.NotNull(response);
        }

    }
}
