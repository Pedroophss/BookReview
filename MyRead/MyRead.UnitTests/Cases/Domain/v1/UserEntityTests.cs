using MyRead.Domain.Entities;
using MyRead.Domain.Resources;
using Shouldly;
using System.Linq;
using Xunit;

namespace MyRead.UnitTests.Cases.Domain.v1
{
    [Trait("Domain", "Entities")]
    public class UserEntityTests
    {
        [Fact(DisplayName = "UserEntity: When invalid name")]
        public void Book_InvalidTitle_ShouldHaveNameValidation()
        {
            // Assert
            var user = new UserEntity(string.Empty, 18, false);

            // Act
            var validations = user.Validate();

            // Assert
            validations.ShouldHaveSingleItem();
            validations.Single().Source.ShouldBe(nameof(UserEntity));
            validations.Single().MessageKey.ShouldBe(nameof(DomainStrs.UserNameRequired));
        }

        [Fact(DisplayName = "UserEntity: When valid")]
        public void Book_ZeroPages_ShouldHavePagesValidation()
        {
            // Assert
            var user = new UserEntity("Jose", 70, true);

            // Act
            var validations = user.Validate();

            // Assert
            validations.ShouldBeEmpty();
        }
    }
}