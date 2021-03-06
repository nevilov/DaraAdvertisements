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
        [InlineAutoData(null)]
        public async Task Delete_Throw_Exception_If_Request_Null(
            Delete.Request request, CancellationToken cancellationToken,
            int userId, int adId)
        {
            ConfigureMoqEnvironment(userId.ToString(), adId);
            // Assert
            await Assert.ThrowsAsync<NullReferenceException>(async () => await advertisementService.Delete(request, cancellationToken));
            _identityServiceMock.Verify();
        }
    }
}
