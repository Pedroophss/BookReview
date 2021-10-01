using MyRead.Bind.Models;
using MyRead.Domain.Entities;
using System.Collections.Generic;
using Xunit;

namespace MyRead.UnitTests.Cases.Bind.v1
{
    [Trait("Bind", "Models")]
    public class BookItemTests
    {
        [Fact(DisplayName = "BookItemBuilder: When valid input")]
        public void BookItemBuilder_ValidCsvRow_ShouldGenerateItem()
        {
            // Arrange
            byte rating = 5;
            var review = "legal :)";
            var rowCsv = "1,Harry Potter and the Half-Blood Prince (Harry Potter  #6),J.K. Rowling/Mary GrandPrÃ©,4.57,0439785960,9780439785969,eng,652,2,3,9/16/2006,Scholastic Inc.";

            var user = new UserEntity("nome", 18, true);
            var users = new List<UserEntity> { user };

            var builder = new BookItemBuilder(i => review, () => rating, users);

            // Act
            var item = builder.CreateOne(rowCsv);

            // Assert Book
            Assert.Equal(652, item.Book.Pages);
            Assert.Equal("eng", item.Book.Language);
            Assert.Equal("J.K. Rowling/Mary GrandPrÃ©", item.Book.Authors);
            Assert.Equal("Harry Potter and the Half-Blood Prince (Harry Potter  #6)", item.Book.Title);

            // Assert Rating
            Assert.Equal(2, item.Ratings.Count);

            var rating1 = item.Ratings[0];

            Assert.Equal(rating, rating1.Rating);
            Assert.Equal(user.Id, rating1.UserId);

            var rating2 = item.Ratings[1];

            Assert.Equal(rating, rating2.Rating);
            Assert.Equal(user.Id, rating2.UserId);

            // Assert Review
            Assert.Equal(3, item.Reviews.Count);

            var review1 = item.Reviews[0];

            Assert.Equal(review, review1.Reviwer);
            Assert.Equal(user.Id, review1.UserId);

            var review2 = item.Reviews[1];

            Assert.Equal(review, review2.Reviwer);
            Assert.Equal(user.Id, review2.UserId);

            var review3 = item.Reviews[2];

            Assert.Equal(review, review3.Reviwer);
            Assert.Equal(user.Id, review3.UserId);

            // Assert Counts
            var counts = item.Count;

            Assert.Equal((uint)2, counts.Ratings);
            Assert.Equal((uint)3, counts.Reviwers);
        }
    }
}