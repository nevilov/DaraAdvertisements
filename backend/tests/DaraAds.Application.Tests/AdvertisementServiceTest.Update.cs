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
        public async Task Update_Response_Success(
            Update.Request request, CancellationToken cancellationToken,
            int userId, int adId)
        {
            ConfigureMoqEnvironment(userId.ToString(), adId);

            // Act
            var response = await advertisementService.Update(request, cancellationToken);

            // Assert
            _identityServiceMock.Verify();
            Assert.NotNull(response);
            Assert.NotEqual(default, response.Id);
        }


        [Theory]
        [InlineAutoData(null)]
        public async Task Update_Throw_Exception_If_Request_Null(
            Update.Request request, CancellationToken cancellationToken,
            int userId, int adId)
        {
            ConfigureMoqEnvironment(userId.ToString(), adId);

            // Assert
            await Assert.ThrowsAsync<NullReferenceException>(async () => await advertisementService.Update(request, cancellationToken));
            _identityServiceMock.Verify();
        }
    }
}
