using AutoFixture;
using AutoFixture.Xunit2;
using DaraAds.Application.Identity.Contracts;
using DaraAds.Application.Identity.Interfaces;
using DaraAds.Application.Repositories;
using DaraAds.Application.Services.User.Contracts;
using DaraAds.Application.Services.User.Contracts.Exceptions;
using DaraAds.Application.Services.User.Implementations;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DaraAds.Application.Tests
{
    public class UserServiceTest
    {
        private Mock<IRepository<Domain.User, string>> _repositoryMock;
        private Mock<IIdentityService> _identityServiceMock;

        private UserService _userService;

        public UserServiceTest()
        {
            _repositoryMock = new Mock<IRepository<Domain.User, string>>();
            _identityServiceMock = new Mock<IIdentityService>();
            //TODO Дописать тесты с учетом новых сервисов
            // _userService = new UserService(_repositoryMock.Object, _identityServiceMock.Object);
        }

        [Theory]
        [AutoData]
        public async Task Register_Return_Response_Id_Sucess(
            Register.Request registerRequest, CancellationToken cancellationToken)
        {
            registerRequest.Email = "test123@gg.cc";
            //Arrange
            var createUserResponse = new Fixture()
                .Build<CreateUser.Response>()
                .Do(_ => _.IsSuccess = true)
                .Create();

            ConfigureIdentity(createUserResponse);

            //Act
            Register.Response userRegisterResponse = await _userService.Register(registerRequest, cancellationToken);

            //Assert
            _identityServiceMock.Verify();
            Assert.NotNull(userRegisterResponse);
            Assert.NotEqual(default, userRegisterResponse.UserId);
        }

        private void ConfigureIdentity(CreateUser.Response createUserResponse)
        {
            _identityServiceMock
                .Setup(_ => _.CreateUser(It.IsAny<CreateUser.Request>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(createUserResponse)
                .Verifiable();
        }

        [Theory]
        [InlineAutoData(null)]
        public async Task Register_Throw_Exception_If_Request_Null(
        Register.Request registerRequest, CancellationToken cancellationToken)
        {
            var createUserResponse = new Fixture()
            .Build<CreateUser.Response>()
            .Do(_ => _.IsSuccess = true)
            .Create();

            ConfigureIdentity(createUserResponse);

            await Assert.ThrowsAsync<NullReferenceException>(
                async () => await _userService.Register(registerRequest, cancellationToken));
        }

    }
}
