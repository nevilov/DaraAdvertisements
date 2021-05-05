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
        public async Task GetById_Response_Success()
        {
            var getRequest = new Get.Request
            {
                Id = 1
            };

            var adResponse = new Domain.Advertisement
            {
                Title = "Test",
                Description = "TestDesc",
                Status = Domain.Advertisement.Statuses.Created,
                Price = 123,
                Cover = "AdCover",
                OwnerUser = new Domain.User
                {
                    Id = "zz",
                    Name = "zz",
                    LastName = "zz"
                }
            };

            advertisementServiceGetConfigure(adResponse);

            // Act
            var response = await advertisementService.Get(getRequest, new CancellationToken());

            // Assert
            Assert.NotNull(response);
        }

        [Fact]
        public async Task GetById_Response_Failure_No_Ad_Found()
        {
            var getRequest = new Get.Request
            {
                Id = 1
            };

            Domain.Advertisement adResponse = null;

            advertisementServiceGetConfigure(adResponse);

            // Act
            await Assert.ThrowsAsync<AdNotFoundException>(async () => await advertisementService.Get(getRequest, new CancellationToken()));

        }

        private void advertisementServiceGetConfigure(Domain.Advertisement adResponse)
        {
            _advertisementRepositoryMock
                .Setup(_ => _.FindById(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(adResponse);
        }
    }
}
