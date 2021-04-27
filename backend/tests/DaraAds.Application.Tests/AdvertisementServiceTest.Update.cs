using AutoFixture.Xunit2;
using DaraAds.Application.Services.Advertisement.Contracts;
using DaraAds.Application.Services.Advertisement.Contracts.Exceptions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DaraAds.Application.Tests
{
    public partial class AdvertisementServiceTest
    {
        [Fact]
        public async Task Update_Response_Success()
        {

            var request = new Update.Request
            {
                Id = 1,
                Title = "UpdTitle",
                Description = "UpdDesc",
                Price = 12345,
                Cover = "TestCover",
                Status = Update.Statuses.Closed,
                UpdateDate = DateTime.UtcNow
            };

            setupMockForUpdate();

            // Act
            var response = await advertisementService.Update(request, new CancellationToken());

            // Assert
            _identityServiceMock.Verify();
            Assert.NotNull(response);
            Assert.NotEqual(default, response.Id);
        }

        [Fact]
        public async Task Update_Response_Failure_No_Ad_Found()
        {

            var request = new Update.Request
            {
                Id = 1,
                Title = "UpdTitle",
                Description = "UpdDesc",
                Price = 12345,
                Cover = "TestCover",
                Status = Update.Statuses.Closed,
                UpdateDate = DateTime.UtcNow
            };

            setupIdentityForUpdate("1");

            Domain.Advertisement adResponse = null;

            _advertisementRepositoryMock
                .Setup(_ => _.FindById(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(adResponse);

            // Act
            await Assert.ThrowsAsync<AdNotFoundException>(async () => await advertisementService.Update(request, new CancellationToken()));

        }

        [Theory]
        [InlineAutoData(null)]
        public async Task Update_Throw_Exception_If_Request_Null(
            Update.Request request, CancellationToken cancellationToken)
        {
            setupMockForUpdate();

            // Assert
            await Assert.ThrowsAsync<NullReferenceException>(async () => await advertisementService.Update(request, cancellationToken));
            _identityServiceMock.Verify();
        }

        private void setupMockForUpdate()
        {
            string userId = "1";

            var adResponse = new Domain.Advertisement
            {
                Title = "Test",
                Description = "TestDesc",
                Status = Domain.Advertisement.Statuses.Created,
                Price = 123,
                Cover = "AdCover",
                OwnerId = userId,
                OwnerUser = new Domain.User
                {
                    Id = userId,
                    Name = "zz",
                    LastName = "zz"
                }
            };

            setupIdentityForUpdate(userId);

            _advertisementRepositoryMock
                 .Setup(_ => _.FindById(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                 .ReturnsAsync(adResponse);
        }

        private void setupIdentityForUpdate(string userId)
        {
            _identityServiceMock
                .Setup(_ => _.GetCurrentUserId(It.IsAny<CancellationToken>()))
                .ReturnsAsync(userId)
                .Verifiable();
        }
    }
}
