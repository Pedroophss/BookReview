using AutoFixture.Xunit2;
using MyRead.Application.Abstractions;
using MyRead.Application.Services;
using MyRead.Domain.Entities;
using MyRead.Domain.Models;
using MyRead.Domain.Repositories;
using MyRead.UnitTests.Cases.Domain.v3.Entities.User;
using MyRead.UnitTests.Core;
using NSubstitute;
using Shouldly;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MyRead.UnitTests.Cases.Application.v3
{
    [Trait("Application", "Services V3")]
    public class UserServiceTests
    {
        [Theory(DisplayName = "When valid user, testing true output"), AutoAssert]
        internal async Task UserService_ValidUser_ShouldReturnTrue
            (ValidUser validUser, UserService svc)
        {
            // Act
            var output = await svc.NewUser(validUser, default);

            // Assert
            output.ShouldBeTrue();
        }

        [Theory(DisplayName = "When invalid user, testing false output"), AutoAssert]
        internal async Task UserService_InvalidNameUser_ShouldReturnFalse
            ([Frozen] INotifier notifier, EmptyNameUser invalidUser, UserService svc)
        {
            // Assert
            notifier.HasError.Returns(true);

            // Act
            var output = await svc.NewUser(invalidUser, default);

            // Assert
            output.ShouldBeFalse();
        }

        [Theory(DisplayName = "When invalid user, testing notifications"), AutoAssert]
        internal async Task UserService_InvalidNameUser_ShouldHaveNotifications
            ([Frozen] INotifier notifier, EmptyNameUser invalidUser, UserService svc)
        {
            // Act
            _ = await svc.NewUser(invalidUser, default);

            // Assert
            notifier.Received(1).Notify(Arg.Any<IEnumerable<ValidationItem>>());
        }

        [Theory(DisplayName = "When valid user, testing notifications"), AutoAssert]
        internal async Task UserService_ValidUser_ShouldHaveNoNotifications
            ([Frozen] INotifier notifier, ValidUser validUser, UserService svc)
        {
            // Act
            _ = await svc.NewUser(validUser, default);

            // Assert
            notifier.DidNotReceive().Notify();
        }

        [Theory(DisplayName = "When valid user, testing repository calls"), AutoAssert]
        internal async Task UserService_ValidUser_ShouldCallRepository
            ([Frozen] IUnitOfWork uow, IUserRepository repo, ValidUser validUser, UserService svc)
        {
            // Assert
            uow.Users.Returns(repo);

            // Act
            _ = await svc.NewUser(validUser, default);

            // Assert
            await repo.Received(1).Save(Arg.Is<UserEntity>(a => a == validUser.Object), Arg.Any<CancellationToken>());
        }
    }
}