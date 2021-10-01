using MyRead.Domain.Entities;
using MyRead.Domain.Models;
using MyRead.UnitTests.Cases.Domain.v2.Entities;
using NSubstitute;
using Shouldly;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MyRead.UnitTests.Cases.Application.v2.Services
{
    [Trait("Application", "Services V2")]
    public class UserServiceTests
    {
        [Fact(DisplayName = "When invalid user, testing false output")]
        public async Task UserService_InvalidNameUser_ShouldReturnFalse()
        {
            // Arrange
            var invalidUser = new UserBuilder().WithEmptyName().Build();
            var mock = new UserSvcMockBuilder().SetNotifierHasErros(true);

            // Act
            var output = await mock.Service.NewUser(invalidUser, default);

            // Assert
            output.ShouldBeFalse();
        }

        [Fact(DisplayName = "When invalid user, testing notifications")]
        public async Task UserService_InvalidNameUser_ShouldHaveNotifications()
        {
            // Arrange
            var invalidUser = new UserBuilder().WithEmptyName().Build();
            var mock = new UserSvcMockBuilder().SetNotifierHasErros(true);

            // Act
            _ = await mock.Service.NewUser(invalidUser, default);

            // Assert
            mock.Notifier.Received(1).Notify(Arg.Any<IEnumerable<ValidationItem>>());
        }

        [Fact(DisplayName = "When valid user, testing true output")]
        public async Task UserService_ValidUser_ShouldReturnTrue()
        {
            // Arrange
            var mock = new UserSvcMockBuilder();
            var validUser = new UserBuilder().Build();

            // Act
            var output = await mock.Service.NewUser(validUser, default);

            // Assert
            output.ShouldBeTrue();
        }

        [Fact(DisplayName = "When valid user, testing notifications")]
        public async Task UserService_ValidUser_ShouldHaveNoNotifications()
        {
            // Arrange
            var mock = new UserSvcMockBuilder();
            var validUser = new UserBuilder().Build();

            // Act
            _ = await mock.Service.NewUser(validUser, default);

            // Assert
            mock.Notifier.DidNotReceive().Notify();
        }

        [Fact(DisplayName = "When valid user, testing repository calls")]
        public async Task UserService_ValidUser_ShouldCallRepository()
        {
            // Arrange
            var validUser = new UserBuilder().Build();
            var mock = new UserSvcMockBuilder().SetUowUserRepository();

            // Act
            _ = await mock.Service.NewUser(validUser, default);

            // Assert
            await mock.UserRepo.Received(1).Save(Arg.Is<UserEntity>(a => a == validUser), Arg.Any<CancellationToken>());
        }
    }
}