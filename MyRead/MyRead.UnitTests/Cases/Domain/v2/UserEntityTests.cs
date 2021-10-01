using MyRead.Domain.Entities;
using MyRead.Domain.Resources;
using MyRead.UnitTests.Cases.Domain.v2.Entities;
using Shouldly;
using System.Linq;
using Xunit;

namespace MyRead.UnitTests.Cases.Domain.v2
{
    [Trait("Domain", "Entities V2")]
    public class UserEntityTests
    {
        [Fact(DisplayName = "UserEntity: When invalid name")]
        public void Book_InvalidTitle_ShouldHaveNameValidation()
        {
            // Assert
            var user = new UserBuilder().WithEmptyName().Build();

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
            var user = new UserBuilder().Build();

            // Act
            var validations = user.Validate();

            // Assert
            validations.ShouldBeEmpty();
        }
    }
}