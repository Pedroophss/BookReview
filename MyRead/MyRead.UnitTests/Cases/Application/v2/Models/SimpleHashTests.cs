using Bogus;
using MyRead.Application.Models;
using Xunit;
using Xunit.Abstractions;

namespace MyRead.UnitTests.Cases.Application.v2
{
    [Trait("Application", "Models V2")]
    public class SimpleHashTests
    {
        public ITestOutputHelper Output { get; }

        public SimpleHashTests(ITestOutputHelper output) =>
            Output = output;

        [Fact(DisplayName = "SimpleHash: When null value")]
        public void CreateSimpleHash_NullValue_ShouldBeZero()
        {
            // Act
            var sh = new SimpleHash(null);

            // Assert
            Assert.Equal(0, sh.Code);
        }

        [InlineData("ambevtech", 3791)]
        [InlineData("#tamojunto", 5010)]
        [InlineData("dotnet humilha", 9241)]
        [InlineData("C# melhor linguagem :)", 19914)]
        [Theory(DisplayName = "SimpleHash: When valid value")]
        public void CreateSimpleHash_ValidValue_ShouldBeExpected(string value, ushort expected)
        {
            // Act
            var sh = new SimpleHash(value);

            // Assert
            Assert.Equal(expected, sh.Code);
        }

        [Fact(DisplayName = "SimpleHash: With random value")]
        public void CreateSimpleHash_RandomValue_ShouldBeNotZero()
        {
            // Arrange
            var bogus = new Faker();
            var value = bogus.Lorem.Sentence(wordCount: 8);

            Output.WriteLine("Generated Value:");
            Output.WriteLine(value);

            // Act
            var sh = new SimpleHash(value);

            // Assert
            Assert.NotEqual(0, sh.Code);
        }
    }
}