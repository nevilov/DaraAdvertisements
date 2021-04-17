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
            //TODO Дописать тесты с учетом новых сервисов
            // advertisementService = new AdvertisementService(_advertisementRepositoryMock.Object, _identityServiceMock.Object);
        }



    }
}
