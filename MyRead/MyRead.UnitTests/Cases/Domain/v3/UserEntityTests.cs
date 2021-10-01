using MyRead.Domain.Entities;
using MyRead.Domain.Resources;
using MyRead.UnitTests.Cases.Domain.v3.Entities.User;
using MyRead.UnitTests.Core;
using Shouldly;
using System.Linq;
using Xunit;

namespace MyRead.UnitTests.Cases.Domain.v3
{
    [Trait("Domain", "Entities V3")]
    public class UserEntityTests
    {
        [Theory(DisplayName = "UserEntity: When invalid name"), AutoAssert]
        public void Book_InvalidTitle_ShouldHaveNameValidation(EmptyNameUser user)
        {
            // Act
            var validations = user.Object.Validate();

            // Assert
            validations.ShouldHaveSingleItem();
            validations.Single().Source.ShouldBe(nameof(UserEntity));
            validations.Single().MessageKey.ShouldBe(nameof(DomainStrs.UserNameRequired));
        }

        [Theory(DisplayName = "UserEntity: When valid"), AutoAssert]
        public void Book_ZeroPages_ShouldHavePagesValidation(ValidUser user)
        {
            // Act
            var validations = user.Object.Validate();

            // Assert
            validations.ShouldBeEmpty();
        }
    }
}