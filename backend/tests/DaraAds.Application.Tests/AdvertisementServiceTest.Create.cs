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
        public async Task Create_Throw_Exception_If_Request_Null(
            Create.Request request, CancellationToken cancellationToken,
            int userId, int adId)
        {
            ConfigureMoqEnvironment(userId.ToString(), adId);

            // Assert
            await Assert.ThrowsAsync<NullReferenceException>(async () => await advertisementService.Create(request, cancellationToken));
            _identityServiceMock.Verify();
        }
    }
}
