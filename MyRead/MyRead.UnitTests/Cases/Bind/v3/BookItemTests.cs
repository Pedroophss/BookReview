using MyRead.Bind.Models;
using MyRead.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using VerifyXunit;
using Xunit;

namespace MyRead.UnitTests.Cases.Bind.v3
{
    [UsesVerify]
    [Trait("Bind", "Models V3")]
    public class BookItemTests
    {
        private const byte Rating = 5;
        private const string Review = "super legal :)";

        private UserEntity User { get; }

        public BookItemTests() =>
            User = new UserEntity(string.Empty, 18, true);

        [Fact(DisplayName = "BookItemBuilder: When valid input")]
        public Task BookItemBuilder_ValidCsvRow_ShouldBeVerified()
        {
            // Arrange
            var builder = new BookItemBuilder(i => Review, () => Rating, new List<UserEntity> { User });
            var rowCsv = "1,Harry Potter and the Half-Blood Prince (Harry Potter  #6),J.K. Rowling/Mary GrandPrÃ©,4.57,0439785960,9780439785969,eng,652,2,4,9/16/2006,Scholastic Inc.";

            // Act
            var item = builder.CreateOne(rowCsv);

            // Assert
            return Verifier.Verify(item);
        }
    }
}