using MyRead.Application.Models;
using Xunit;

namespace MyRead.UnitTests.Cases.Application.v1
{
    [Trait("Application", "Models")]
    public class SimpleHashTests
    {
        [Fact(DisplayName = "SimpleHash: When null value")]
        public void CreateSimpleHash_NullValue_ShouldBeZero()
        {
            // Act
            var sh = new SimpleHash(null);

            // Assert
            Assert.Equal(0, sh.Code);
        }

        [Fact(DisplayName = "SimpleHash: When valid value")]
        public void CreateSimpleHash_ValidValue_ShouldBeExpected()
        {
            // Assert
            var expected = 5010;

            // Act
            var sh = new SimpleHash("#tamojunto");

            // Assert
            Assert.Equal(expected, sh.Code);
        }
    }
}