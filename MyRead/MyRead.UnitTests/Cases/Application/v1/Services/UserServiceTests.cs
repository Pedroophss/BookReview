using MediatR;
using MyRead.Application.Abstractions;
using MyRead.Application.Services;
using MyRead.Domain.Entities;
using MyRead.Domain.Models;
using MyRead.Domain.Repositories;
using NSubstitute;
using Shouldly;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MyRead.UnitTests.Cases.Application.v1.Services
{
    [Trait("Application", "Services")]
    public class UserServiceTests
    {
        [Fact(DisplayName = "When invalid user, testing false output")]
        public async Task UserService_InvalidNameUser_ShouldReturnFalse()
        {
            // Arrange
            var uow = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();
            var notifier = Substitute.For<INotifier>();
            notifier.HasError.Returns(true);

            var invalidUser = new UserEntity(string.Empty, 18, true);
            var service = new UserService(mediator, uow, notifier);

            // Act
            var output = await service.NewUser(invalidUser, default);

            // Assert
            output.ShouldBeFalse();
        }

        [Fact(DisplayName = "When invalid user, testing notifications")]
        public async Task UserService_InvalidNameUser_ShouldHaveNotifications()
        {
            // Arrange
            var uow = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();
            var notifier = Substitute.For<INotifier>();
            notifier.HasError.Returns(true);

            var invalidUser = new UserEntity(string.Empty, 18, true);
            var service = new UserService(mediator, uow, notifier);

            // Act
            _ = await service.NewUser(invalidUser, default);

            // Assert
            notifier.Received(1).Notify(Arg.Any<IEnumerable<ValidationItem>>());
        }

        [Fact(DisplayName = "When valid user, testing true output")]
        public async Task UserService_ValidUser_ShouldReturnTrue()
        {
            // Arrange
            var mediator = Substitute.For<IMediator>();
            var notifier = Substitute.For<INotifier>();
            var userRepo = Substitute.For<IUserRepository>();

            var uow = Substitute.For<IUnitOfWork>();
            uow.Users.Returns(userRepo);

            var validUser = new UserEntity(string.Empty, 18, true);
            var service = new UserService(mediator, uow, notifier);

            // Act
            var output = await service.NewUser(validUser, default);

            // Assert
            output.ShouldBeTrue();
        }

        [Fact(DisplayName = "When valid user, testing notifications")]
        public async Task UserService_ValidUser_ShouldHaveNoNotifications()
        {
            // Arrange
            var uow = Substitute.For<IUnitOfWork>();
            var mediator = Substitute.For<IMediator>();
            var notifier = Substitute.For<INotifier>();

            var invalidUser = new UserEntity(string.Empty, 18, true);
            var service = new UserService(mediator, uow, notifier);

            // Act
            _ = await service.NewUser(invalidUser, default);

            // Assert
            notifier.DidNotReceive().Notify();
        }

        [Fact(DisplayName = "When valid user, testing repository calls")]
        public async Task UserService_ValidUser_ShouldCallRepository()
        {
            // Arrange
            var mediator = Substitute.For<IMediator>();
            var notifier = Substitute.For<INotifier>();
            var userRepo = Substitute.For<IUserRepository>();

            var uow = Substitute.For<IUnitOfWork>();
            uow.Users.Returns(userRepo);

            var validUser = new UserEntity(string.Empty, 18, true);
            var service = new UserService(mediator, uow, notifier);

            // Act
            _ = await service.NewUser(validUser, default);

            // Assert
            await userRepo.Received(1).Save(Arg.Is<UserEntity>(a => a == validUser), Arg.Any<CancellationToken>());
        }
    }
}